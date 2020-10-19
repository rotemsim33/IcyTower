using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraScript : MonoBehaviour
{
    [SerializeField] private GameControllerScript gameController;
    [SerializeField] private Transform player;
    [SerializeField] private Text scoreText;

    [SerializeField] private float m_speedUp = 1.8f;
    [SerializeField] private float m_maxDistanceCameraPlayer = 8f;

    private Vector3 targetPosition;
    private float speedUpCalc;
    private float distanceCameraPlayer;
    private float timeDelta;

    void Start()
    {
        timeDelta = 0;
    }

    //is called every frame
    void LateUpdate()
    {
        //maincamera.y-player.y 
        distanceCameraPlayer = transform.position.y - player.position.y;

        //for the start- give the player another option to go up - even if he a bit under the screen
        if (player.position.y < -0.5) return;

        //once the player.y >0 then we need to check if the distance between the player and the camera is more then 8  so the player was fall
        if (distanceCameraPlayer > m_maxDistanceCameraPlayer)
        {
            gameController.GameOver();
        }

        //if the player is up fast, then set the camera to be on the player
        else if (distanceCameraPlayer < -1)
        {
            targetPosition = new Vector3(0, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, (-1 * distanceCameraPlayer) * Time.deltaTime);
        }
        //make sure that the camera is up all the time in Fixed speed
         else
        {
            targetPosition = new Vector3(0, transform.position.y + speedUpCalc, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        }

        timeDelta += Time.deltaTime;
        speedUpCalc = (1 + (timeDelta) / 60) * m_speedUp;
    }

     public void UpdateScore(int score)
    {
        int scoreCals = (score / 3) * 10;
        scoreText.text = scoreCals.ToString();
    }
}
