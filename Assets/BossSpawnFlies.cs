using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnFlies : MonoBehaviour
{
    [SerializeField] private int spawnInterval = 3;
    [SerializeField] private GameObject smallFly;
    [SerializeField] private Transform spawnLocation;

    private float time;
    private EnemyPlayerRange range;

    private void Awake()
    {
        range = GetComponent<EnemyPlayerRange>();
    }

    // Update is called once per frame
    void Update()
    {
        if (range.playerIsInRange)
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
}
