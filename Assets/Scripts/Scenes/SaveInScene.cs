using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveInScene : MonoBehaviour
{
    public string targetObjectName = "Target"; // Name of the GameObject you want to remove

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        
        GameObject targetObject = GameObject.Find(gameObject.name);

        if (targetObject != null && targetObject != gameObject)
        {
            Destroy(gameObject); // Removes the GameObject with the specified name
        }
        else if (targetObject == gameObject)
        {
            Debug.LogWarning("Script is attached to the target GameObject. Detach the script to prevent removing itself.");
        }
        else
        {
            Debug.LogWarning("No other GameObject found with the name: " + targetObjectName);
        }
        
    }
}
