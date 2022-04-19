using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public GameObject Entrance;
    public Transform Exit;
    public Transform Player;
    public Transform Camera;

    private void onTriggerEnter2D()
    {
            Debug.Log("In");
            /*if (Input.GetKeyDown(KeyCode.E))
            {
                Player.position = Exit.position;
            }*/
    }

}
