using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CTRL + K + D (Cleans code)
public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 10f;
    public Rigidbody rigid;

    private bool isGrounded = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if W key is pressed
        if(Input.GetKey(KeyCode.W))
        {
            // Move forward
            rigid.AddForce(Vector3.forward * speed);
        }

        // Check if S key is pressed
        if(Input.GetKey(KeyCode.S))
        {
            // Move back
            rigid.AddForce(Vector3.back * speed);
        }

        // Check if A key is pressed
        if(Input.GetKey(KeyCode.A))
        {
            // Move left
            rigid.AddForce(Vector3.left * speed);
        }

        // Check if D key is pressed
        if(Input.GetKey(KeyCode.D))
        {
            // Move right
            rigid.AddForce(Vector3.right * speed);
        }

        /* 
            Logical Operator:
            && - AND
            || - OR
             > - Is Greater Than
             < - Is Less Than
            >= - Is Greater Than OR equal to
            <= - Is Less Than OR equal to
            != - Is NOT equal to
            == - Is Equal to

            Arithmetic Operator:
            + - Addition
            - - Subtraction
            / - Division
            * - Multiply
        */

        // If space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Jump up!
            rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        // I have hit something!
        if(collision.collider.name == "Ground")
        {
            // I have hit the ground!
            isGrounded = true;
        }
    }
}
