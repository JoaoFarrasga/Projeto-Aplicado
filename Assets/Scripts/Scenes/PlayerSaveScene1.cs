using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveScene1 : MonoBehaviour
{
    private static bool playerExists;

    void Awake()
    {
        // Check if an instance of the player already exists
        if (playerExists)
        {
            // If an instance already exists, destroy this object
            Destroy(gameObject);
        }
        else
        {
            // If this is the first instance, set playerExists to true
            playerExists = true;

            // Mark this GameObject to not be destroyed when loading new scenes
            DontDestroyOnLoad(gameObject);
        }
    }
}
