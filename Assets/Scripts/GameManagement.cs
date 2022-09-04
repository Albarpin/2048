using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winningPanel;
    [SerializeField] GameObject fillPrefab; 
    [SerializeField] Cell[] allCells; 
    [SerializeField] Text scoreDisplay;
    [SerializeField] int winningScore;


    public int gameScore;
    public Color[] colorsOfFill;
    public static Action<string> slide;
    public static GameManagement instance;
    public static int cellsTicker; 

    private int gameOver;
    private bool wonGame;


    void Start()
    {
        StartSpawnFill();
        StartSpawnFill();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFill();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ResetTickerAndGameOver();
            slide("up");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ResetTickerAndGameOver();
            slide("right");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ResetTickerAndGameOver();
            slide("down");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ResetTickerAndGameOver();
            slide("left");
        }
    }

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    
    public void StartSpawnFill()
    {
        int spawnedCell = UnityEngine.Random.Range(0, allCells.Length);
        if (allCells[spawnedCell].transform.childCount != 0) 
        {
            SpawnFill();
            return;
        }
        Fill2Or4(spawnedCell, 2);
    }

    public void SpawnFill()
    {
        bool isFull = true;
        
        for (int i = 0; i < allCells.Length; i++)
        {
            if (allCells[i].fill == null)
            {
                isFull = false;
            }
        }
        if (isFull == true)
        {
            return;
        }

        int spawnedCell = UnityEngine.Random.Range(0, allCells.Length); 

        if (allCells[spawnedCell].transform.childCount != 0) 
        {
            SpawnFill();
            return;
        }

        float intsantiateChance = UnityEngine.Random.Range(0f, 1f);

        if (intsantiateChance < 0.2f) 
        {
            return;
        }
        else if (intsantiateChance < 0.8f) 
        {
            Fill2Or4(spawnedCell, 2);
        }
        else 
        {
            Fill2Or4(spawnedCell, 4);
        }
    }

    private void Fill2Or4(int spawnedCell, int cellValue)
    {
        GameObject tmpFillPrefab = Instantiate(fillPrefab, allCells[spawnedCell].transform);
        Fill tmpFillComponent = tmpFillPrefab.GetComponent<Fill>();
        allCells[spawnedCell].GetComponent<Cell>().fill = tmpFillComponent; 
        tmpFillComponent.UpdateFillValue(cellValue);
    }

    public void UpdateGameScore(int cellValue)
    {
        gameScore += cellValue;
        scoreDisplay.text = gameScore.ToString();
    }

    public void IsGameOver()
    {
        gameOver++;
        if (gameOver >= 16)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void IsGameWon(int highestFill)
    {
        if (wonGame)
        {
            return;
        }
        if (highestFill == winningScore)
        {
            winningPanel.SetActive(true);
            wonGame = true;
        }
    }
    public void ContinueGame()
    {
        winningPanel.SetActive(false);
    }
    private void ResetTickerAndGameOver()
    {
        cellsTicker = 0;
        gameOver = 0;
    }
}