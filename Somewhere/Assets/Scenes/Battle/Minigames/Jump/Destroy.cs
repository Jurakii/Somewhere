using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Destroy the enemy object
            Destroy(collision.gameObject);
            // Find the GameManager in the scene
            GameManager gameManager = FindObjectOfType<GameManager>();

            // Increment the score in the GameManager
            gameManager.IncreaseScore(1);


        }
    }
}
