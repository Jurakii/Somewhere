using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : MonoBehaviour
{
    private GameObject currentTeleporter;
    public GameObject cam;
    public Animator transition;
    public float transitionTime = 1f;
    private bool warping = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && warping == false)
        {
            if (currentTeleporter != null)
            {
                warping = true;
                WarpPlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
    public void WarpPlayer()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        PlayerMovement moveScript = GetComponent<PlayerMovement>();
        moveScript.canMove = false;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        transform.position = currentTeleporter.GetComponent<Warp>().GetDestination().position;
        cam.transform.position = transform.position;

        transition.SetTrigger("End");
        warping = false;
        moveScript.canMove = true;
    }
}