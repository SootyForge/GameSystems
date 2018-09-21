using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : Projectile
{
    public float explosionRadius = 5f;
    public GameObject explosion;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public override void OnCollisionEnter(Collision col)
    {
        string tag = col.collider.tag;
        if (tag != "Player" && tag != "Weapon")
        {
            Effects(); // Spawns a particle
            Explode(); // Deals damage to surrounding enemies
            Destroy(gameObject);
        }
    }

    void Effects()
    {
        // Spawn a new explosion GameObject
        Instantiate(explosion, transform.position, transform.rotation);
    }

    void Explode()
    {
        // Detect collision with objects using radius
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        // Loop through everything we hit
        foreach (var hit in hits)
        {
            // Try getting the enemy component
            Enemy e = hit.GetComponent<Enemy>();
            // If we hit an enemy
            if(e)
            {
                // Note(Manny) : You can calculate a falloff damage here

                // Deal damage to the enemy
                e.DealDamage(damage);
            }
        }
    }
}
