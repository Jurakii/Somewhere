using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float obstacleSpawnInterval = 2f;
    public GameObject obstaclePrefab;
    public Transform obstacleSpawnPoint;
    public Text scoreText;

    private int score = 0;
    private bool isGameOver = false;

    private void Start()
    {
        InvokeRepeating("SpawnObstacle", obstacleSpawnInterval, obstacleSpawnInterval);
    }

    private void SpawnObstacle()
    {
        if (!isGameOver)
        {
            Instantiate(obstaclePrefab, obstacleSpawnPoint.position, Quaternion.identity);
        }
    }

    public void IncreaseScore(int amount)
    {
        if (!isGameOver)
        {
            score += amount;
            scoreText.text = score.ToString();
        }
    }
    public void DecreaseScore(int amount)
    {
        if (!isGameOver && score > 0)
        {
            score -= amount;
            scoreText.text = score.ToString();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public void RestartGame()
    {
        // Reload the scene or reset necessary variables to restart the game
    }
}
