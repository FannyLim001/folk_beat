using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using System;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string selectedSongName;
    public AudioClip selectedAudioClip;
    public string currentMidiFile;
    public string midiFile1;
    public string midiFile2;
    public string midiFile3;

    public MidiFile midiFile;
    public TempoMap tempoMap;
    public IEnumerable<Note> notes;

    public int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReadFromFile()
    {
        StartCoroutine(ReadMidiFileCoroutine());
    }

    private IEnumerator ReadMidiFileCoroutine()
    {
        string fullPath = Path.Combine(Application.streamingAssetsPath, currentMidiFile);
        string filePath;

        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest www = UnityWebRequest.Get(fullPath);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error reading MIDI file: " + www.error);
                yield break;
            }

            byte[] fileData = www.downloadHandler.data;
            filePath = Path.Combine(Application.temporaryCachePath, "temp.mid");
            File.WriteAllBytes(filePath, fileData);
        }
        else
        {
            filePath = fullPath;
        }

        try
        {
            midiFile = MidiFile.Read(filePath);
            GetDataFromMidi();
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to read MIDI file: " + ex.Message);
        }
    }

    public void GetDataFromMidi()
    {
        // Get notes from the MIDI file
        notes = midiFile.GetNotes();
        tempoMap = midiFile.GetTempoMap();

        // Print information about each note
        foreach (var note in notes)
        {
            Debug.Log($"Note: Pitch={note.NoteNumber}, Start Time={note.Time}, Duration={note.Length}");
        }

        Debug.Log("Total notes: " + notes.Count());
    }

    public IEnumerable<Note> GetMidiNotes()
    {
        return notes;
    }

    public TempoMap GetTempoMap()
    {
        return tempoMap;
    }

    public void AddPoints(int points)
    {
        score += points;
        Debug.Log($"Points added: {points}. Total score: {score}");
        // You can add more logic here as needed, such as updating UI elements
    }

    public int CountPercentage()
    {
        // Ensure at least one of the operands is a floating point number
        float scoreFloat = (float)score;
        float notesCount = (float)notes.Count();

        // Calculate percentage with floating point division
        float percentage = (scoreFloat / notesCount) * 100;

        // Ensure percentage does not exceed 100
        if (percentage > 100) percentage = 100;

        return (int)percentage;
    }

    public void ResetScore()
    {
        score = 0;
    }

}
