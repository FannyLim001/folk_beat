using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;

public class GameplayManager : MonoBehaviour
{
    public TMP_Text songNameText;
    public TMP_Text songDurationText;
    public TMP_Text songScore;
    public AudioSource audioSource;

    private float songPositionSeconds; // Current position in the song in seconds
    private bool isPlaying = false;

    public GameObject notePrefab;
    public Transform notesContainer;

    private IEnumerable<Note> midiNotes;
    private TempoMap tempoMap;

    private Coroutine spawnNotesCoroutine; // Coroutine reference

    private List<Vector3> predefinedPositions;

    public int maxHealth = 50;
    public int currentHealth;

    public HealthBar healthBar;

    public GameObject failedPanel;

    void Start()
    {
        failedPanel.SetActive(false);

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is null. Ensure GameManager is initialized correctly.");
            return;
        }

        AudioManager.instance.stopSound();

        string songName = GameManager.instance.selectedSongName;
        AudioClip audioClip = GameManager.instance.selectedAudioClip;

        GameManager.instance.ResetScore();

        songNameText.text = songName;

        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            songPositionSeconds = 0f;
            songDurationText.text = FormatTime(audioClip.length);

            audioSource.Play();
            isPlaying = true;
        }

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        StartCoroutine(InitializeMidiData());
    }

    IEnumerator InitializeMidiData()
    {
        GameManager.instance.ReadFromFile();

        while (GameManager.instance.GetMidiNotes() == null || GameManager.instance.GetTempoMap() == null)
        {
            yield return null;
        }

        midiNotes = GameManager.instance.GetMidiNotes();
        tempoMap = GameManager.instance.GetTempoMap();

        InitializePredefinedPositions();

        spawnNotesCoroutine = StartCoroutine(SpawnNotesCoroutine());
    }

    private IEnumerator SpawnNotesCoroutine()
    {
        foreach (var note in midiNotes)
        {
            double beatTime = note.TimeAs<MetricTimeSpan>(tempoMap).TotalSeconds;

            double waitTime = beatTime - songPositionSeconds;
            if (waitTime > 0)
            {
                yield return new WaitForSeconds((float)waitTime);
            }

            Vector3 position = FindNonOverlappingPosition();

            GameObject noteObject = Instantiate(notePrefab, position, Quaternion.identity, notesContainer);

            double noteDuration = note.LengthAs<MetricTimeSpan>(tempoMap).TotalSeconds + 1f;

            NoteClick noteClick = noteObject.GetComponentInChildren<NoteClick>();
            if (noteClick != null)
            {
                noteClick.parent = noteObject;
                noteClick.Initialize((float)noteDuration);
            }

            songPositionSeconds = (float)beatTime; // Update song position
        }
    }

    private void InitializePredefinedPositions()
    {
        predefinedPositions = new List<Vector3>();
        RectTransform rectTransform = notesContainer.GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("RectTransform component is missing on notesContainer.");
            return;
        }

        float containerWidth = rectTransform.rect.width;
        float containerHeight = rectTransform.rect.height;

        int rows = 3;
        int columns = 3;

        // Calculate padding between positions
        float paddingX = containerWidth / (columns + 1);
        float paddingY = containerHeight / (rows + 1);

        // Calculate starting position based on top-left corner
        float startX = -containerWidth / 2f + paddingX;
        float startY = containerHeight / 2f - paddingY;

        // Iterate over rows and columns
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                // Calculate position for each column and row
                float posX = startX + i * paddingX;
                float posY = startY - j * paddingY;

                // Convert local position to world position
                Vector3 localPosition = new Vector3(posX, posY, 0f);

                predefinedPositions.Add(localPosition);
            }
        }
    }

    private Vector3 FindNonOverlappingPosition()
    {
        if (predefinedPositions == null || predefinedPositions.Count == 0)
        {
            Debug.LogError("Predefined positions are not initialized.");
            return notesContainer.position;
        }

        foreach (Vector3 position in predefinedPositions)
        {
            Vector3 worldPosition = notesContainer.TransformPoint(position);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPosition, 1f);
            if (colliders.Length == 0)
            {
                return worldPosition;
            }
        }

        Debug.LogWarning("Could not find a non-overlapping position.");
        return notesContainer.position;
    }

    void Update()
    {
        if (isPlaying)
        {
            songPositionSeconds = audioSource.time;

            songDurationText.text = FormatTime(songPositionSeconds);

            if (!audioSource.isPlaying)
            {
                isPlaying = false;
                SceneManager.LoadScene("Result");
            }

            songScore.text = GameManager.instance.score.ToString();
        }
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            // Handle player death
            isPlaying = false;
            Time.timeScale = 0;
            audioSource.Stop();
            failedPanel.SetActive(true);
            Debug.Log("Player is dead!");
        }
    }

    public void Retry()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("MainGameplay");
    }

    public void Back()
    {
        Time.timeScale = 1;

        AudioClip musicClip = Resources.Load<AudioClip>("Music/StockTune-Heartbeats In Neon Light_1720417489");
        AudioManager.instance.changeSound(musicClip);
        AudioManager.instance.playSound();
        SceneManager.LoadScene("ChooseSong");
    }
}
