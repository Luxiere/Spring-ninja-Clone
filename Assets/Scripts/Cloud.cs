using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Move
{
    [SerializeField] float minHeight = 0, maxHeight = 10;
    [SerializeField] float minSpawnOffset = -2, maxSpawnOffset = 3;
    [SerializeField] float minSize = 0.5f, maxSize = 2;
    [SerializeField] float reusePosition = -4.5f;
    [SerializeField] bool randomStart = true;


    readonly float spawnPoint = 9f; // Camera size is 8

    private void Start()
    {
        if(randomStart) Spawn(0);
    }

    void Update()
    {
        Jump();
        if(transform.position.x < reusePosition)
        {
            Spawn(spawnPoint);
        }
    }

    private void Spawn(float spawnPoint)
    {
        transform.position = new Vector3(Random.Range(spawnPoint + minSpawnOffset, spawnPoint + maxSpawnOffset), Random.Range(minHeight, maxHeight), 0);
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, size);
    }
}
