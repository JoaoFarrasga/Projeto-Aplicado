using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCSystem : MonoBehaviour
{
    public string[] dialogueLines;
    public GameObject canvas;
    public GameObject dialogueTemplate;
    public TextMeshProUGUI pressKeyToTalk;
    public string pressKeyToTalkText = "Press F to talk";

    private string dialogueText;
    private bool playerDetection = false;
    private int index = 0;

    // Update is called once per frame
    void Update()
    {
        if (playerDetection)
        {
            pressKeyToTalk.text = pressKeyToTalkText;
            Debug.Log("Press F to talk");

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (index == dialogueLines.Length)
                {
                    pressKeyToTalk.text = "";
                    dialogueTemplate.SetActive(false);
                    index = 0;
                }
                else
                {
                    dialogueTemplate.SetActive(true);

                    dialogueTemplate.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dialogueLines[index];

                    index++;
                }
            }
        }
        else
        {
            pressKeyToTalk.text = "";
            dialogueTemplate.SetActive(false);
            index = 0;
        }
    }

    private void NewDialogue(string text) 
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDetection = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDetection = false;
        }
    }
}
