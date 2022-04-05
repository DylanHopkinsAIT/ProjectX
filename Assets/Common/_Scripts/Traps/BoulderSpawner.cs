using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boulder;
    [SerializeField] private int repeatRate;
    private Vector3 spawnPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        //Set boulder spawn position to spawner location
        spawnPosition = transform.position;
        
        //Spawn boulder every 5 seconds
        InvokeRepeating(nameof(SpawnBoulder), 0,repeatRate);
    }
    
    void SpawnBoulder()
    {
        Instantiate(boulder, spawnPosition, Quaternion.identity);
    }
}
