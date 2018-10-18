using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Task 1). Create a Draw.io URL diagram for the Projectile System.
 * Details: Projectile system needs the following structure
 *                  Projectile
 *                 /    |    \
 *            Normal   Fire  Explosive
 * Variables for each class
 * Functions for each class
 * 
 * Task 2). Player cannot shoot until the Weapon is ready to fire.
 * Details: Refer to discord channel "GameSystems" for any resources on this task
 */

namespace ThirdPersonController
{
    public abstract class Weapon : MonoBehaviour
    {
        public int damage = 100;
        public int ammo = 30;
        public float accuracy = 1f;
        public float range = 10f;
        public float rateOfFire = 5f;
        public GameObject projectile;
        public Transform spawnPoint;
        protected int currentAmmo = 0;

        public abstract void Attack();

        public void Reload()
        {
            // Reset currentAmmo
            currentAmmo = ammo;
        }
    } 
}
