using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBoss : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private GameObject bossChild;

    private void Awake()
    {
        gameObject.GetComponent<TimeManager>().OnDeathAction += SpawnLittleBosses;
    }

    private void SpawnLittleBosses()
    {
        for (int i = 0; i < 2; i++)
        {
            Instantiate(bossChild, transform.position, transform.rotation);
        }
    }
}
