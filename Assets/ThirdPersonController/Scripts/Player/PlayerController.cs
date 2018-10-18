﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 1). Weapon Cycle
 * 2). Inderaction System
 */

// CTRL + K + D (Cleans code)
namespace ThirdPersonController
{
    public class PlayerController : MonoBehaviour
    {
        public bool rotateToMainCamera = false;
        public bool rotateWeapon = false;
        public float moveSpeed = 5f;
        public float jumpHeight = 10f;
        public Rigidbody rigid;
        public float rayDistance = 1f; // How many units the ray is drawn below the player.
        public LayerMask ignoreLayers;
        public Weapon[] weapons;

        private Weapon currentWeapon;
        private bool isGrounded = false;
        private Vector3 moveDir;
        private bool isJumping;
        private Interactable interactObject;

        // Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected
        private void OnDrawGizmos()
        {
            Ray groundRay = new Ray(transform.position, Vector3.down);
            Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * rayDistance);
        }
        private void OnTriggerEnter(Collider other)
        {
            interactObject = other.GetComponent<Interactable>();
        }
        private void OnTriggerExit(Collider other)
        {
            interactObject = null;
        }
        private bool IsGrounded()
        {
            Ray groundRay = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            // Casts a line beneath the player
            if (Physics.Raycast(groundRay, out hit, rayDistance, ~ignoreLayers))
            {
                // is grounded
                return true;
            }
            // is NOT grounded
            return false;
        }

        // Update is called once per frame
        private void Update()
        {
            // Get the euler angles of Camera
            Vector3 camEuler = Camera.main.transform.eulerAngles;

            // Is the contorller rotating to the camera?
            if (rotateToMainCamera)
            {
                // Calculate the new move direction by only taking into account the Y Axis
                moveDir = Quaternion.AngleAxis(camEuler.y, Vector3.up) * moveDir;
            }

            Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);

            if (isJumping && IsGrounded())
            {
                force.y = jumpHeight;
                isJumping = false;
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
        private void DisableAllWeapons()
        {
            // Loop through every weapon
            foreach (Weapon weapon in weapons)
            {
                // Deactivate weapon's GameObject
                weapon.gameObject.SetActive(false);
            }
        }

        // Selects and switches out the current weapon
        public void SelectWeapon(int index)
        {
            // Check index is within range of weapons array
            // is within range i >= 0 && i < length
            // is not within range i < 0 && i >= length
            if (index < 0 || index >= weapons.Length)
                return;

            // DisableAllWeapons
            DisableAllWeapons();

            // Enable weapon at index
            weapons[index].gameObject.SetActive(true);

            // Set the currentWeapon
            currentWeapon = weapons[index];
        }
        public void Move(float inputH, float inputV)
        {
            moveDir = new Vector3(inputH, 0f, inputV);
            moveDir *= moveSpeed;
        }
        public void Jump()
        {
            isJumping = true;
        }
        public void Attack()
        {
            currentWeapon.Attack();
        }
        public void Interact()
        {
            // If interactable is found
            if (interactObject)
            {
                // Run interact
                interactObject.Interact();
            }
        }
    } 
}