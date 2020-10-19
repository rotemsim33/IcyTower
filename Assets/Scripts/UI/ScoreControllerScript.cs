using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    void Start()
    {
        scoreText.text = "0";
    }

    public void UpdateScore(int score)
    {
        int prevScore = int.Parse(scoreText.text);
        int scoreCals = (score / 3) * 10;
        if(scoreCals > prevScore) scoreText.text = scoreCals.ToString();
    }
}
