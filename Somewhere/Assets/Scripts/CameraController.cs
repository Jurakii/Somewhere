using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    private DialogueUI dialogueUI;

    private void Start()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
        cam3.SetActive(false);
        cam1.SendMessage("Color");
        dialogueUI = FindObjectOfType<DialogueUI>(); // Find the DialogueUI script in the scene
        dialogueUI.UpdateTextWithColor();
    }

    private void Update()
    {
        if (Input.GetButtonDown("1Key"))
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
            cam3.SetActive(false);
            cam1.SendMessage("Color");
            dialogueUI.UpdateTextWithColor(); // Call UpdateTextWithColor method in DialogueUI
        }
        if (Input.GetButtonDown("2Key"))
        {
            cam2.SetActive(true);
            cam1.SetActive(false);
            cam3.SetActive(false);
            cam2.SendMessage("Color");
            dialogueUI.UpdateTextWithColor(); // Call UpdateTextWithColor method in DialogueUI
        }
        if (Input.GetButtonDown("3Key"))
        {
            cam3.SetActive(true);
            cam2.SetActive(false);
            cam1.SetActive(false);
            cam3.SendMessage("Color");
            dialogueUI.UpdateTextWithColor(); // Call UpdateTextWithColor method in DialogueUI
        }
    }
}
