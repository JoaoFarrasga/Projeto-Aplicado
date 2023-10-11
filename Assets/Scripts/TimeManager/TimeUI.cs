using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField]
    private TimeManager timeManager;
    private TMP_Text text;

    private void Awake()
    {
        //timeManager = GetComponent<TimeManager>();
        text = GetComponent<TMP_Text>();
        timeManager.OnChange += UpdateInfo;

    }

    void UpdateInfo()
    {
        text.text = Mathf.RoundToInt(timeManager.Value).ToString();
    }



}
