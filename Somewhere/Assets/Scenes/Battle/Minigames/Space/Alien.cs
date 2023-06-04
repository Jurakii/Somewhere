using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public float moveSpeed = 5f;          // Speed of object movement
    public float maxXPosition = 10f;      // Maximum x position before flipping direction
    private bool moveRight = true;        // Flag to track the direction of movement

    private void Update()
    {
        // Calculate the direction of movement
        float direction = moveRight ? 1f : -1f;

        // Move the object horizontally
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        // Check if the object has reached the maximum x position
        if (moveRight && transform.position.x >= maxXPosition)
        {
            // Change direction to move left
            moveRight = false;
            transform.Translate(Vector3.down * 1f);
        }
        else if (!moveRight && transform.position.x <= -maxXPosition)
        {
            // Change direction to move right
            moveRight = true;
            transform.Translate(Vector3.down * 1f);
        }
    }
}
