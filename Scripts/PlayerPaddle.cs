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
    private void FixedUpdate() {
        //find mouse position, change velocity
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        velocity = new Vector3(rb.velocity.x, (mousePos.y - transform.position.y) * speed, 0);
        rb.velocity = velocity;
        //bound y
        if (transform.position.y > boundY) {
            transform.position = new Vector3(transform.position.x, boundY, 0);
        } else if (transform.position.y < -boundY) {
            transform.position = new Vector3(transform.position.x, -boundY, 0);
        }
    }

    private void MovePaddle() {
        
    }
}
