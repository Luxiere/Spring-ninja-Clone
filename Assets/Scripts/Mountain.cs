using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 2;

    float jumpTime = 0;
    bool isMoving = false;

    Renderer rdr;

    private void OnEnable()
    {
        Ninja.onJump += OnJump;
    }

    private void OnDisable()
    {
        Ninja.onJump -= OnJump;
    }

    void Start()
    {
        rdr = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isMoving)
        {
            jumpTime += Time.deltaTime;
            rdr.material.SetTextureOffset("_MainTex", new Vector2(jumpTime * jumpSpeed, 0));
        }
    }

    private void OnJump(bool move)
    {
        isMoving = move;
    }
}
