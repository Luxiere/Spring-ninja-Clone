using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] float speed = 10, jumpSpeed = 15;
    [SerializeField] float accelerationTime = 0.5f;

    float currentAccelerationTime = 0;
    bool isAccelerating = false;

    private void Update()
    {
        if (isAccelerating)
        {
            currentAccelerationTime += Time.deltaTime;
            currentAccelerationTime = Mathf.Min(currentAccelerationTime, accelerationTime);
        }
        else
        {
            currentAccelerationTime -= Time.deltaTime;
            currentAccelerationTime = Mathf.Max(currentAccelerationTime, 0);
        }
        GetComponent<Renderer>().material.SetFloat("_Speed", Mathf.Lerp(speed, jumpSpeed, currentAccelerationTime / accelerationTime));
    }

    private void OnEnable()
    {
        Ninja.onJump += OnJump;
        Ninja.onLand += OnLand;
    }

    private void OnDisable()
    {
        Ninja.onJump -= OnJump;
        Ninja.onLand -= OnLand;
    }

    private void OnJump(bool move)
    {
        isAccelerating = move;
        currentAccelerationTime = move ? currentAccelerationTime : 0;
    }

    private void OnLand()
    {
        isAccelerating = false;
    }
}
