using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    [SerializeField] float jumpSpeed = 3;

    float currentSpeed;

    void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        Jump();
    }

    public void Jump()
    {
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        Ninja.onJump += OnJump;
    }

    private void OnDisable()
    {
        Ninja.onJump -= OnJump;
    }

    private void OnJump(bool move)
    {
        currentSpeed = move ? jumpSpeed : speed;
    }
}
