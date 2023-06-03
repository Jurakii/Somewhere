using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        // Move the object to the left
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Find the GameManager in the scene
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.DecreaseScore(1);
        }
    }
}
