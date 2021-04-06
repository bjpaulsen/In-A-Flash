using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Unit
{
    [Header("Explosion Settings")]
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;

    // Performs class-specific functions before unit death
    public override void ReadyDeath()
    {
        // Explode and push nearby units
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

        foreach (GameObject unit in units)
        {
            unit.GetComponent<Rigidbody2D>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        // Play sound effect (octave randomly varies)
        if (Random.Range(0, 2) == 1)
            audioManager.Play("G-Hit High");
        else
            audioManager.Play("G-Hit Low");

        // Destroy self
        Done();
    }
}
