using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour, InteractableInterface
{
    [SerializeField] public string[] startDialogueLines;
    [SerializeField] public GameObject canvas;
    [SerializeField] private GameObject dialogueTemplatePrefab;
    [SerializeField] public string pressKeyToInteractText = "Press F to Open";
    [SerializeField] private GameObject pressKeyToTalkPrefab;
    [SerializeField] private bool isLocked = false;
    [SerializeField] private CharacterController2D player;
    [SerializeField] private string sceneName;

    private GameObject textPrefab;
    private bool isInteracting = false;
    private bool playerDetection = false;
    private int startIndex = 0;
    private GameObject dialoguePrefab;
    private TMP_Text dialogueText;
    private bool isNearObject;
    private bool isWritingDialogue;
    private int keyAmount;
    public void Interact()
    {
        isInteracting = true;
    }

    private void Update()
    {
        if (playerDetection)
        {
            if (!isNearObject)
            {
                textPrefab = Instantiate(pressKeyToTalkPrefab, canvas.transform);
                textPrefab.GetComponent<TMP_Text>().text = pressKeyToInteractText;
                isNearObject = true;
            }
            if (isInteracting)
            {
                isLocked = SearchPlayerKey(isLocked);
                if (!isLocked)
                {
                    SceneManager.LoadScene(sceneName);
                    Debug.Log("Door unlocked");
                }
                else 
                {
                    DoorLockedDialogue(startDialogueLines);
                }
            }

        }
        else if(isNearObject)
        {
            DialogueVariablesReset();
        }
        isInteracting = false;
    }


    private void DoorLockedDialogue(string[] startDialogue) 
    {
        if (startIndex != startDialogue.Length)
        {
            if (isLocked)
            {
                WriteDialogue(startDialogue, ref startIndex);
            }
            else
            {
                Debug.Log("Unlocked/Change scene to room");
            }
        }
        else if(isWritingDialogue)
        {
            DialogueVariablesReset();
        }
    }

    private void WriteDialogue(string[] dialogue, ref int index)
    {
        if (!isWritingDialogue)
        {
            dialoguePrefab = Instantiate(dialogueTemplatePrefab, canvas.transform);
            dialogueText = dialoguePrefab.transform.GetComponentInChildren<TMP_Text>();
            isWritingDialogue = true;
        }
        dialogueText.text = dialogue[index];

        index++;
    }

    private void DialogueVariablesReset()
    {
        Destroy(textPrefab);
        //dialogueTemplate.SetActive(false);
        Destroy(dialoguePrefab);
        isNearObject = false;
        isWritingDialogue = false;
        startIndex = 0;
    }

    private bool SearchPlayerKey(bool lockedState) 
    {
        foreach (Inventory.Slot slot in player.inventory.slots)
        {
            if (slot.name == "Door Key")
            {
                player.inventory.RemoveItem(slot.name, 1);
                lockedState = false;
                return false;
            }
        }
        return true;
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
