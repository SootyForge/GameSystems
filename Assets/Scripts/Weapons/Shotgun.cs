using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public float pellets = 6;

    public override void Attack()
    {
        // Store forward direction of player
        Vector3 direction = transform.forward;
        // Calculate spread by using range
        Vector3 spread = Vector3.zero;
        //Offset on Local Y
        spread += transform.up * Random.Range(-accuracy, accuracy);
        //Offset on Local X
        spread += transform.right * Random.Range(-accuracy, accuracy);
        //Instantiate a new bullet from prefab "bullet"
        GameObject clone = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        // Get the component from the new bullet
        Bullet newBullet = clone.GetComponent<Bullet>();
        // Tell the bullet to Fire()
        newBullet.Fire(direction + spread);
    }
}
