using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private Animator panelGameOverAnim;
    [SerializeField] private Text actualGameScore;
    [SerializeField] private Text gameScoreText;

    [SerializeField] private Text menuScoreText;
    [SerializeField] private Text actualHighestScoreText;
    
    private MusicController musicController;
    private GameObject player;

    private void Awake()
    {
        musicController = FindObjectOfType<MusicController>();
        player = GameObject.FindGameObjectWithTag("Player");
        actualHighestScoreText.text = PlayerPrefs.GetInt("highScore", 0).ToString();
    }
    public void GameOver()
    {
        musicController.DeathMusic();
        player.GetComponent<PlayerScript>().enabled = false;
        panelGameOverAnim.SetBool("On",true);
        menuScoreText.text = "Score:" + actualGameScore.text;

        //handl highscore
        int currScore= int.Parse(actualGameScore.text);

        if (currScore > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", currScore);
            actualHighestScoreText.text = actualGameScore.text;

        }

        actualGameScore.gameObject.SetActive(false);
        gameScoreText.gameObject.SetActive(false);

    }

    public void PlayAgain()
    {
        musicController.LevelMusic();
        panelGameOverAnim.SetBool("On", false);
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
