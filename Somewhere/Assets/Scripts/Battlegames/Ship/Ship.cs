using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public float movementSpeed=10f;
    public int offset=10;
    public GameObject laser;
    public float cooldown=1f;
    private float time=0f;

    void Start() {
        Debug.Log("Hello");
    }

    void Update() {
        Debug.Log("Hello1");
        /*if (time > 0f)
        {
            time -= Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(laser, transform.TransformPoint(Vector3.forward * 2), transform.rotation);
            time = cooldown;
        }*/

        //Movement
        var targetPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z=transform.position.z;
        float verticalInput=Input.GetAxis("Vertical");
        verticalInput=Mathf.Abs(verticalInput);
        transform.position=Vector3.MoveTowards(transform.position, targetPos, verticalInput * movementSpeed * Time.deltaTime);
        Vector3 difference=Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z=Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation=Quaternion.Euler(0f, 0f, rotation_z + offset);
    }
}