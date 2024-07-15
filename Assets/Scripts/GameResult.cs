using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    public Image rankImage;
    public TMP_Text accuracyScore;
    // Start is called before the first frame update
    void Start()
    {
        AudioClip clip = Resources.Load<AudioClip>("Music/StockTune-City Nights Serenade_1720417406");
        AudioManager.instance.changeSound(clip);
        AudioManager.instance.playSound();

        int accuracy = GameManager.instance.CountPercentage();
        accuracyScore.text = accuracy.ToString() + " %";

        var rank = "";

        if(accuracy >= 95)
        {
            rank = "s";
        } else if(accuracy >= 90)
        {
            rank = "a";
        } else if(accuracy >= 80)
        {
            rank = "b";
        } else if (accuracy >= 70)
        {
            rank = "c";
        } else
        {
            rank = "f";
        }

        var image = Resources.Load<Sprite>("2D/UI/result-rank-"+rank);

        rankImage.sprite = image;
    }

    public void backBtn()
    {
        AudioClip musicClip = Resources.Load<AudioClip>("Music/StockTune-Heartbeats In Neon Light_1720417489");
        AudioManager.instance.changeSound(musicClip);
        AudioManager.instance.playSound();
        SceneManager.LoadScene("ChooseSong");
    }
}
