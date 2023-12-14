using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int OpeningDirection;
    //1 --> need BOTTOM door
    //2 --> need TOP door
    //3 --> need LEFT door
    //4 --> need RIGHT door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;
    bool firstTime = true;


    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.05f);
    }
    void Spawn()
    {
        if (spawned == false)
        {
            if (OpeningDirection == 1)
            {
                //need to spawn room with bottom door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (OpeningDirection == 2)
            {
                //need to spawn room with top door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (OpeningDirection == 3)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (OpeningDirection == 4)
            {
                //need to spawn room with right door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            else if (OpeningDirection == 5)
            {
                //need to spawn room with just for spawn
                rand = Random.Range(0, templates.spawnRooms.Length);
                Instantiate(templates.spawnRooms[rand], transform.position, templates.spawnRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            Destroy(gameObject);
        }
    }
}
