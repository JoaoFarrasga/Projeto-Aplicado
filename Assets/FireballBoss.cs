using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBoss : MonoBehaviour
{
    [SerializeField]
    private TimeManager timeManager;

    private float bossHealth;

    private void Update()
    {
        timeManager = GetComponent<TimeManager>();
        
    }
}
