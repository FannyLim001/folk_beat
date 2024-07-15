using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongSelector : MonoBehaviour
{
    public GameObject Song;
    public Transform SongsContainer;
    
    public AudioSource mainAudioSource;

    public GameObject songDetailPanel;
    public TMP_Text songNameInDetail;
    public TMP_Text songDetailText;

    void Start()
    {
        SongOnIsland songManager = SongOnIsland.instance;
        songDetailPanel.SetActive(false);
        foreach(var song in songManager.songs)
        {
            string songName = song.name;
            string songDetail = song.details;

            string path1 = song.midiFilePaths.Count > 0 ? song.midiFilePaths[0] : "";
            string path2 = song.midiFilePaths.Count > 1 ? song.midiFilePaths[1] : "";
            string path3 = song.midiFilePaths.Count > 2 ? song.midiFilePaths[2] : "";

            AudioClip clip = Resources.Load<AudioClip>("Music/Songs/" + songName);

            GameObject button = Instantiate(Song, SongsContainer);
            button.GetComponentInChildren<TMP_Text>().text = songName;

            button.GetComponent<Button>().onClick.AddListener(() => OnSongButtonClicked(clip, songName, songDetail, path1, path2, path3));
        }
    }

    void OnSongButtonClicked(AudioClip clip, string songName, string songDetail, string path1, string path2, string path3)
    {
        if (mainAudioSource.isPlaying)
        {
            mainAudioSource.Stop();
        }

        AudioManager.instance.stopSound();

        mainAudioSource.clip = clip;
        mainAudioSource.Play();

        songDetailPanel.SetActive(true);
        songNameInDetail.text = songName;
        songDetailText.text = songDetail;

        GameManager.instance.selectedSongName = songName;
        GameManager.instance.selectedAudioClip = clip;

        GameManager.instance.midiFile1 = path1;
        GameManager.instance.midiFile2 = path2;
        GameManager.instance.midiFile3 = path3;
    }

    public void PlaySongButton(int level)
    {
        if (level == 1)
        {
            GameManager.instance.currentMidiFile = GameManager.instance.midiFile1;
        }
        else if (level == 2)
        {
            GameManager.instance.currentMidiFile = GameManager.instance.midiFile2;
        }
        else if (level == 3)
        {
            GameManager.instance.currentMidiFile = GameManager.instance.midiFile3;
        }

        SceneManager.LoadScene("MainGameplay");
    }

    public void DetailBackButton()
    {
        songDetailPanel.SetActive(false);
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back button clicked");
        AudioManager.instance.playSound();
        SceneManager.LoadScene("ChooseIsland");
    }
}
