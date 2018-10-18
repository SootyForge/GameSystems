using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Player : MonoBehaviour
    {
        public float upForce = 5f;
        public bool isDead = false;
        public Rigidbody2D rigid;
        
        // Update is called once per frame
        void Update()
        {
            // If mouse button is down
            if (Input.GetMouseButtonDown(0))
            {
                // Make bird flap
                Flap();
            }
        }

        // Makes the bird flap when called
        void Flap()
        {
            // Is the bird not dead?
            if (!isDead)
            {
                // Cancel velocity
                rigid.velocity = Vector2.zero;
                // Give the bird an upward force using impulse
                // new Vector2(0, upForce) == Vector2.up * upForce
                rigid.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
            }
        }
    } 
}