using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour
{
    void Start()
    {
        // Set color
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Options.METER_COLOR;
        else
            GetComponent<Image>().color = Options.METER_COLOR;
    }
}
