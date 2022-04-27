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
    public GameObject arrow;

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
            arrow.gameObject.SetActive(true);
            currentTeleporter = collision.gameObject;
            arrow.transform.position = currentTeleporter.transform.position + new Vector3 (0,3,0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            arrow.gameObject.SetActive(false);
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