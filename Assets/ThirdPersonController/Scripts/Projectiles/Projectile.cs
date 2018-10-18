﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController
{
    public abstract class Projectile : MonoBehaviour
    {
        public int damage = 100;
        public float speed = 5f;
        public float range = 50f;
        public GameObject impact;
        public Rigidbody rigid;

        public virtual void Fire(Vector3 direction)
        {
            // Shoot the bullet off in given direction × speed
            rigid.AddForce(direction * speed, ForceMode.Impulse);
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
            Instantiate(impact, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    } 
}
