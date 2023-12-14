using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void OnEnable()
    {
        //GameManager.instance.CursorToggle(true);
    }

    public void ResumeGame()
    {
        //GameManager.instance.CursorToggle(false);
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Prototipe");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}