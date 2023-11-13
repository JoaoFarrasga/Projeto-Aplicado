using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnFlies : MonoBehaviour
{
    [SerializeField] private int spawnInterval = 3;
    [SerializeField] private GameObject smallFly;
    [SerializeField] private Transform spawnLocation;

    private float time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnInterval)
        {
            Instantiate(smallFly, spawnLocation.position, spawnLocation.rotation);
            Debug.Log("Spawn fly");
            time = 0;
        }
    }
}
