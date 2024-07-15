using UnityEngine;
using System.Collections;

public class NoteClick : MonoBehaviour
{
    public GameObject parent; // Reference to the parent note GameObject
    public GameObject text;
    public int pointsToAdd = 1; // Points to add when the button is clicked
    private bool isClicked = false; // Flag to check if the note has already been clicked

    private GameplayManager gameplayManager;

    void Start()
    {
        // Find the GameplayManager in the scene
        gameplayManager = FindObjectOfType<GameplayManager>();
    }

    public void AddPoints()
    {
        if (isClicked) return; // If already clicked, do nothing

        isClicked = true; // Mark as clicked

        Debug.Log("Parent GameObject found: " + parent.name);
        if (parent.TryGetComponent<AudioSource>(out var audioSource))
        {
            audioSource.Play();
            Debug.Log("Audio played.");
        }
        else
        {
            Debug.LogWarning("AudioSource component not found on parent GameObject.");
        }

        text.SetActive(true);
        // Add points to the GameManager
        GameManager.instance.AddPoints(pointsToAdd);

        StartCoroutine(DestroyAfterDelay(0.1f));
    }

    public void Initialize(float noteDuration)
    {
        StartCoroutine(DestroyAfterDelay(noteDuration));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        if (!isClicked) // Check if the note was not clicked
        {
            gameplayManager.DecreaseHealth(5); // Decrease health if note was not clicked
        }

        // Destroy the parent GameObject
        Destroy(parent);
    }
}
