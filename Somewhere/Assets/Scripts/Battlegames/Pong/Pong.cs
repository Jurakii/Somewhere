using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pong : MonoBehaviour
{
    public bool isBall;
    public bool isPlayer;
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;

    private float movement;

    public bool isGoal;
    public bool isPlayer1Goal;

    void Start()
    {
        if (isBall)
        {
            startPosition = transform.position;
            Launch();
        } else if (isPlayer)
        {
            startPosition = transform.position;
        }
    }

    void Update()
    {
        if (isPlayer)
        {
            if (isPlayer1)
            {
                movement = Input.GetAxisRaw("Vertical");
            }
            else
            {
                //movement = Input.GetAxisRaw("Vertical2");
            }

            rb.velocity = new Vector2(rb.velocity.x, movement * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGoal)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                if (!isPlayer1Goal)
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().Player1Scored();
                } else
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().Player2Scored();
                }
            }
        }
    }

    public void Reset()
    {
        if (isPlayer)
        {
            rb.velocity = Vector2.zero;
            transform.position = startPosition;
        } else if (isBall)
        {
            rb.velocity = Vector2.zero;
            transform.position = startPosition;
            Launch();
        }
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }
}
