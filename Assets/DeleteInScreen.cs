using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteInScreen : MonoBehaviour
{
    [SerializeField] private string firstSceneName = "FirstLevel";

    void Update()
    {
        // Check for scene transition
        if (SceneManager.GetActiveScene().name == firstSceneName)
        {
            Destroy(gameObject); // or add your custom destruction logic
            // Update the current scene name
            //currentSceneName = SceneManager.GetActiveScene().name;
        }
    }
}
