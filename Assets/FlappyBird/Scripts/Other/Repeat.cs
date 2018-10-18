using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Repeat : MonoBehaviour
    {
        public float moveSpeed = 1f;
        public SpriteRenderer rend;
        private float width;

        // Use this for initialization
        void Start()
        {
            // Get with from collider and scale by transform
            width = rend.bounds.size.x;

        }

        // Update is called once per frame
        void Update()
        {
            // Get position
            Vector3 pos = transform.position;
            // Move position
            pos += Vector3.left * moveSpeed * Time.deltaTime;
            // IF on leaving left side if screen
            if (pos.x < -width)
            {
                // Repeat = Move to opposite side of screen
                float offset = width * 2;
                Vector3 newPosition = new Vector3(offset, 0, 0);
                pos += newPosition;
            }

            transform.position = pos;
        }
    } 
}
