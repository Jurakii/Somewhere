using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float wrapPositionX = -10f; // X-coordinate where the object wraps to the opposite side

    private void Update()
    {
        // Move the object to the left
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Check if the object has reached or gone beyond the wrap position
        if (transform.position.x <= wrapPositionX)
        {
            // Calculate the new x-position on the opposite side of the wrap position
            float newPositionX = -wrapPositionX;
            transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        }
    }
}
