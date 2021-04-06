using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : Unit
{

    // Performs class-specific functions before unit death
    public override void ReadyDeath()
    {
        // Play sound effect (octave randomly varies)
        if (Random.Range(0, 2) == 1)
            audioManager.Play("A-Hit High");
        else
            audioManager.Play("A-Hit Low");

        Done();
    }
}
