using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] Transform ninja = null;
    [SerializeField] Renderer rdr = null;

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

    private void Update()
    {
        SetGlobalScale(transform, new Vector3(1, ninja.position.y - ninja.GetComponent<Ninja>().currentColumn.position.y, 1));
    }

    public static void SetGlobalScale(Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }

    private void OnJump(bool move)
    {
        rdr.enabled = false;
    }

    private void OnLand()
    {
        rdr.enabled = true;
    }
}
