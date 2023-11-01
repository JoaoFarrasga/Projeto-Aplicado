using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorManager : MonoBehaviour, InteractableInterface
{
    [SerializeField] public string[] startDialogueLines;
    [SerializeField] public GameObject canvas;
    [SerializeField] public GameObject dialogueTemplate;
    public TextMeshProUGUI pressKeyToInteract;
    [SerializeField] public string pressKeyToInteractText = "Press F to Open";

    private bool isInteracting = false;
    private bool playerDetection = false;
    private int startIndex = 0;
    public void Interact()
    {
        isInteracting = true;
    }

    private void Update()
    {
        if (playerDetection)
        {
            pressKeyToInteract.text = pressKeyToInteractText;
            Debug.Log(pressKeyToInteract.text);
            if (isInteracting)
            {
                DoorLockedDialogue(startDialogueLines);
            }
        }
        else
        {
            DialogueVariablesReset();
        }
        isInteracting = false;
    }

    private void DoorLockedDialogue(string[] startDialogue) 
    {
        if (startIndex != startDialogue.Length)
        {
            WriteDialogue(startDialogue, ref startIndex);
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
        pressKeyToInteract.text = "";
        dialogueTemplate.SetActive(false);
        //startIndex = 0;
        //endIndex = 0;
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
