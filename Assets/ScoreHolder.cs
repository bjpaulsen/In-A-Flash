using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolder : MonoBehaviour
{
    void Start()
    {
        // Set night mode
        if (Options.NightMode)
            GetComponent<SpriteRenderer>().color = Options.DARK_SCOREHOLDER;
    }
}
