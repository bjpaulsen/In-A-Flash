using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Vector3 pivot;

    public void Point(int team, float value)
    {
        float newXScale = team * value + transform.localScale.x;
        ScaleAround(new Vector3(newXScale, transform.localScale.y, transform.localScale.z));
        if (newXScale > 449)
            Debug.Log("LEFT WINS");
        else if (newXScale < -449)
            Debug.Log("RIGHT WINS");
    }

    // credit: KyleBlumreisinger on Unity Forums
    // https://forum.unity.com/threads/scale-around-point-similar-to-rotate-around.232768/
    private void ScaleAround(Vector3 newScale)
    {
        Vector3 A = transform.localPosition;
        Vector3 B = pivot;
    
        Vector3 C = A - B; // diff from object pivot to desired pivot/origin
    
        float RS;
        if (transform.localScale.x == 0)
            RS = newScale.x;
        else
            RS = newScale.x / transform.localScale.x; // relative scale factor

        // calc final position post-scale
        Vector3 FP = B + C * RS;
    
        // finally, actually perform the scale/translation
        transform.localScale = newScale;
        transform.localPosition = FP;
    }
}
