
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    public float speed = 10f;
    public float boundY = 2.25f;
    public Rigidbody rb;
    private Vector3 velocity;

    // Update is called once per frame
    private void Update() {
        if (transform.position.y > boundY) {
            transform.position = new Vector3(transform.position.x, boundY, 0);
        } else if (transform.position.y < -boundY) {
            transform.position = new Vector3(transform.position.x, -boundY, 0);
        }
    }

    private void FixedUpdate(){
        //move paddle
        MovePaddle();
    }

    private void MovePaddle() {
        //find mouse position, change velocity
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 normalizedMousePos = new Vector3(0, mousePos.y - transform.position.y, 0);
        //normalize normalizedMousePos
        normalizedMousePos.Normalize();
        //normalize mousey - transform.position.y
        
        velocity = new Vector3(rb.velocity.x, (normalizedMousePos.y) * speed, 0);
        //if close enough to mouse, just tp y pos to mouse and set velocity to 0
        if (Mathf.Abs(mousePos.y - transform.position.y) < speed * Time.deltaTime) {
            transform.position = new Vector3(transform.position.x, mousePos.y, 0);
            velocity = new Vector3(rb.velocity.x, 0, 0);
        }
        //bound y'
        rb.velocity = velocity;
    }
}
