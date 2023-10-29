using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCSystem : MonoBehaviour
{
    public bool isBarter = false;
    public string[] startDialogueLines;
    public string[] endDialogueLines;
    public GameObject canvas;
    public GameObject dialogueTemplate;
    public GameObject buyMenu;
    public TextMeshProUGUI pressKeyToTalk;
    public string pressKeyToTalkText = "Press F to talk";
    public GameObject buyMenuContainer;

    private bool hasBarted = false;
    private string dialogueText;
    private bool playerDetection = false;
    private int startIndex = 0;
    private int endIndex = 0;
    private int indexCheckpoint = 0;

    // Update is called once per frame
    void Update()
    {
        NPCActions(startDialogueLines, endDialogueLines);
    }

    private void NPCActions(string[] startDialogue, string[] endDialogue) 
    {
        if (playerDetection)
        {
            pressKeyToTalk.text = pressKeyToTalkText;

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (startIndex == startDialogue.Length && endIndex != endDialogue.Length)
                {
                    if (isBarter && !hasBarted)
                    {
                        BuyMenuActions();
                    }
                    else
                    {
                        buyMenu.SetActive(false);

                        WriteDialogue(endDialogue, ref endIndex);
                    }
                }
                else if (startIndex != startDialogue.Length)
                {

                    WriteDialogue(startDialogue, ref startIndex);
                }
                else
                {
                    DialogueVariablesReset();
                }
            }
        }
        else
        {
            DialogueVariablesReset();
        }
    }

    private void WriteDialogue(string[] dialogue, ref int index) 
    {
        dialogueTemplate.SetActive(true);

        dialogueTemplate.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dialogue[index];

        index++;
    }

    private void DialogueVariablesReset() 
    {
        pressKeyToTalk.text = "";
        dialogueTemplate.SetActive(false);
        buyMenu.SetActive(false);
        startIndex = 0;
        endIndex = 0;
        hasBarted = false;
    }

    private void BuyMenuActions() 
    {
        pressKeyToTalk.text = "";
        dialogueTemplate.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        buyMenu.SetActive(true);       

        hasBarted = true;
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
