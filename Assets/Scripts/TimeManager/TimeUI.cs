using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField]
    private TimeManager timeManager;
    private TMP_Text text;

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>(); // Find TimeManager in the scene
        text = GetComponent<TMP_Text>();

        if (timeManager != null)
        {
            timeManager.OnChange += UpdateInfo;
        }
        else
        {
            Debug.LogError("TimeManager not found in the scene.");
        }

    }

    void UpdateInfo()
    {
        text.text = Mathf.RoundToInt(timeManager.Value).ToString();
    }
}
