using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Options.BACKGROUND_COLOR;
    }
}
