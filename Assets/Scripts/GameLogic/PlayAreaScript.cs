using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayAreaScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] prefabLevelsArray;
    [SerializeField] private GameObject prefabWalls;

    //first level is in y=0
    [SerializeField] private float m_currYLevel = 0f;
    [SerializeField] private float m_currYWall = 5;

    [SerializeField] private float m_distanceBeforeContinuesLevel = 3f;
    [SerializeField] private float m_distanceBeforeContinuesWall = 10f;

    [SerializeField] private int m_amountInitialLevels = 12;
    [SerializeField] private int m_amountInitialWalls = 5;

    [SerializeField] private float m_distanceBetweenLevels = 2.8f;
    [SerializeField] private float m_wallHight = 8f;

    [SerializeField] private List<GameObject> levelsList;
    [SerializeField] private List<GameObject> wallsList;

    private void Awake()
    {
        InitialLevels();
        InitialWalls();
    }

    private void Update()
    {
        //levels
        if (m_currYLevel - player.position.y < m_distanceBeforeContinuesLevel) ContinusLevelsCreation();

        //walls
        if (m_currYWall - player.position.y < m_distanceBeforeContinuesWall) ContinusWallsCreation();
 
        
    }
    //create first m_amountInitialLevels of levels
    private void InitialLevels()
    {
        for (int i = 0; i < m_amountInitialLevels; i++)
        {
            //3 types of levels- pick randomlly one of them
            int randomLevelindex = (int)Random.Range(0, 2.9f);
            Vector2 vecPos = new Vector2(Random.Range(-4.4f, 3.8f), m_currYLevel);

            GameObject level = Instantiate(prefabLevelsArray[randomLevelindex], vecPos, Quaternion.identity, transform);
            m_currYLevel += m_distanceBetweenLevels;
            levelsList.Add(level);

        }
    }

    //making a list of Levels- one goes in and the last goes out
    private void ContinusLevelsCreation()
    {
        GameObject temp = levelsList[0];
        temp.transform.position = new Vector2(Random.Range(-4.4f, 3.8f), m_currYLevel);
        m_currYLevel += m_distanceBetweenLevels;

        levelsList.RemoveAt(0);
        levelsList.Add(temp);
    }

    //create first m_amountInitialWalls of walls
    private void InitialWalls()
    {
        for (int i = 0; i < m_amountInitialWalls; ++i)
        {
            Vector2 vecPos = new Vector2(0, m_currYWall);
            GameObject wall = Instantiate(prefabWalls, vecPos, Quaternion.identity, transform);
            m_currYWall += m_wallHight;
            wallsList.Add(wall);
        }
    }

    //making a list of walls- one goes in and the last goes out
    private void ContinusWallsCreation()
    {
        GameObject temp = wallsList[0];
        temp.transform.position = new Vector2(0, m_currYWall);
        m_currYWall += m_wallHight;

        wallsList.RemoveAt(0);
        wallsList.Add(temp);
    }
}
