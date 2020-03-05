using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] Ninja ninja = null;

    float holdTime = 0;

    void Update()
    {
        if (Ninja.isRecovering) return;
        if (Ninja.isJumping) return;
        if (Input.GetMouseButton(0))
        {
            holdTime += Time.deltaTime;
            holdTime = Mathf.Min(holdTime, 1);
            ninja.ChargeJump(holdTime);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ninja.Jump(holdTime);
            holdTime = 0;
        }
    }
}
