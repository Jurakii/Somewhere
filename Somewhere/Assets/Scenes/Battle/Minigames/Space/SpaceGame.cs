using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceGame : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform enemyContainer;
    public Transform player;
    public Transform bulletPrefab;
    public Transform bulletContainer;
    public Text scoreText;
    public Text gameOverText;


    public float playerSpeed = 1f;
    public float shootSpeed = 1f;

    private int score;
    private bool isGameOver;

    private void Start()
    {
        score = 0;
        isGameOver = false;
        gameOverText.gameObject.SetActive(false);

        // Spawn enemies
        SpawnEnemies();
    }

    private void Update()
    {
        if (isGameOver)
            return;

        // Move the player
        float moveHorizontal = Input.GetAxis("Horizontal");
        player.Translate(Vector3.right * moveHorizontal * Time.deltaTime * playerSpeed);

        // Shoot bullets
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }

        // Check if all enemies are destroyed
        if (enemyContainer.childCount == 0)
        {
            isGameOver = true;
            gameOverText.gameObject.SetActive(true);
        }
    }

private void SpawnEnemies()
{
    float initialX = -2.5f;
    float initialY = 2f;
    float spacingX = 1.5f;
    float spacingY = 1.2f;

    for (int row = 0; row < 3; row++)
    {
        for (int col = 0; col < 6; col++)
        {
            Vector3 position = enemyContainer.position + new Vector3(initialX + spacingX * col, initialY - spacingY * row, 0f);
            Instantiate(enemyPrefab, position, Quaternion.identity, enemyContainer);
        }
    }
}


    private void ShootBullet()
    {
        Vector3 bulletPosition = player.position + Vector3.up;
        Transform bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity, bulletContainer);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * shootSpeed;
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = "" + score;
    }
}
