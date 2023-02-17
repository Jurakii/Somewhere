using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    float nGScale = 1;
    float oGScale = 0;
    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;


    private Rigidbody2D m_Rigidbody2D;


    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
        }

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            oGScale = m_Rigidbody2D.gravityScale;
            m_Rigidbody2D.gravityScale = m_Rigidbody2D.gravityScale * -1;
            nGScale = m_Rigidbody2D.gravityScale;
            Debug.Log(oGScale + " " + nGScale);
            

        }

        if ((nGScale > oGScale) && (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            Debug.Log("Jump");
            m_Rigidbody2D.AddForce(new Vector2(0f, 1000));
        }
        else if ((nGScale < oGScale) && (Input.GetKeyDown(KeyCode.DownArrow)))
        {
            Debug.Log("Jump");

            m_Rigidbody2D.AddForce(new Vector2(0f, -1000));
        }
    }

    void FixedUpdate()
    {
        
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
}
