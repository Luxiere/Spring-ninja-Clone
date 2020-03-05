using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Columns : MonoBehaviour
{
    [SerializeField] float minHeight = -5, maxHeight = 0;
    [SerializeField] float minSpawnOffset = 2, maxSpawnOffset = 6;
    [SerializeField] float reusePosition = -5.5f;
    
    public List<Transform> columns = null; 

    private void Start()
    {
        for(int i = 1; i < columns.Count; i++)
        {
            columns[i].position = new Vector3(columns[i - 1].position.x + Random.Range(minSpawnOffset, maxSpawnOffset), Random.Range(minHeight, maxHeight), 0);
        }
    }

    void Update()
    {
        for(int i = 0; i < columns.Count; i++)
        {
            if (columns[i].position.x < reusePosition)
            {
                Spawn(i);
            }
        }
    }

    public void Spawn(int columnIndex)
    {        
        columns[columnIndex].position = new Vector3(columns[columnIndex - 1 >= 0 ? columnIndex - 1 : columns.Count - 1].position.x + Random.Range(minSpawnOffset, maxSpawnOffset), Random.Range(minHeight, maxHeight), 0);
    }
}
