using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
   [SerializeField] private GameObject howToPlayPopUp;

    public void ActiveDisActiveHowToPlay(bool is_active)
    {
        howToPlayPopUp.SetActive(is_active); 

    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
