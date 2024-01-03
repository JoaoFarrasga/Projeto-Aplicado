using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnPortalOnDeath : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private GameObject portal;

    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.GetComponent<TimeManager>().OnDeathAction += SpawnPortal;
    }

    private void SpawnPortal()
    {
        for (int i = 0; i < 2; i++)
        {
            Instantiate(portal, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
