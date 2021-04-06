using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator camAnimator;

    public void ShakeCamera()
    {
        camAnimator.SetTrigger("shaking");
    }
}
