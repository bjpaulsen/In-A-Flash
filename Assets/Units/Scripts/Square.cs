using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Unit
{

    // Performs class-specific functions before unit death
    public override void ReadyDeath()
    {
        // Play sound effect (octave randomly varies)
        if (Random.Range(0, 2) == 1)
            audioManager.Play("C-Hit High");
        else
            audioManager.Play("C-Hit Low");

        Done();
    }
}
