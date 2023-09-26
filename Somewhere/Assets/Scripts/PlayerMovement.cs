using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private DialogueUI dialogueUI;

	public DialogueUI DialogueUI => dialogueUI;

	public IInteractable Interactable { get; set; }


	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	public bool canMove = false;

	// Update is called once per frame
	void Update()
	{
		if (dialogueUI.IsOpen)
		{
			canMove = false;
			return;
		}

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		if (canMove)
        {
			animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        else
        {
			animator.SetFloat("Speed", 0f);
			
        }

		
			

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

		if (Input.GetKeyDown(KeyCode.E))
        {
			Interactable?.Interact(player:this);
        }

	}

	void FixedUpdate()
	{
        if (canMove)
        {
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
        } else
        {
			//animator.Play("Idle");
			controller.Move(0, crouch, jump);
		}

	}
}