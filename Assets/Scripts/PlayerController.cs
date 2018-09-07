using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 1. Ability to rotate the player 
 * - Check if the player is grounded
 * - Wherever the player moves, they will rotate in that direction
 */

// CTRL + K + D (Cleans code)
public class PlayerController : MonoBehaviour
{
    public bool rotateToMainCamera = false;
    public bool rotateWeapon = false;
    public Weapon currentWeapon;

    public float moveSpeed = 5f;
    public float jumpHeight = 10f;
    public Rigidbody rigid;
    public float rayDistance = 1f; // How many units the ray is drawn below the player.
    public LayerMask ignoreLayers;
    
    private bool isGrounded = false;

// Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected
    void OnDrawGizmos()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * rayDistance);
    }

    bool IsGrounded()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        // Casts a line beneath the player
        if(Physics.Raycast(groundRay, out hit, rayDistance, ~ignoreLayers))
        {
            // is grounded
            return true;
        }
        // is NOT grounded
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        // If fire button is pressed AND weapon is allowed to fire
        if (Input.GetButton("Fire1"))
        {
            // Fire the weapon
            currentWeapon.Attack();
        }

        float inputH = Input.GetAxis("Horizontal") * moveSpeed;
        float inputV = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 moveDir = new Vector3(inputH, 0f, inputV);

        // Get the euler angles of Camera
        Vector3 camEuler = Camera.main.transform.eulerAngles;

        // Is the contorller rotating to the camera?
        if (rotateToMainCamera)
        {
            // Calculate the new move direction by only taking into account the Y Axis
            moveDir = Quaternion.AngleAxis(camEuler.y, Vector3.up) * moveDir;
        }

        Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            force.y = jumpHeight;
        }

        rigid.velocity = force;

        // If the user pressed a key (moveDir has values in it other than 0)
        /*if(moveDir.magnitude > 0)
        {
            // Rotate the player to that moveDir
            transform.rotation = Quaternion.LookRotation(moveDir);
        }*/

        // TESTING!

        Quaternion playerRotation = Quaternion.AngleAxis(camEuler.y, Vector3.up);
        transform.rotation = playerRotation;

        if (rotateWeapon)
        {
            Quaternion weaponRotation = Quaternion.AngleAxis(camEuler.x, Vector3.right);
            currentWeapon.transform.localRotation = weaponRotation;
        }
    }
}