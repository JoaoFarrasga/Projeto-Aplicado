using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCSystem : MonoBehaviour, InteractableInterface
{
    [SerializeField] private bool isBarter = false;
    [SerializeField] private string[] startDialogueLines;
    [SerializeField] private string[] endDialogueLines;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject dialogueTemplatePrefab;
    [SerializeField] private GameObject buyMenu;
    [SerializeField] private string pressKeyToTalkText = "Press F to talk";
    [SerializeField] private GameObject pressKeyToTalkPrefab;

    private bool hasBarted = false;
    //private string dialogueText;
    private bool playerDetection = false;
    private int startIndex = 0;
    private int endIndex = 0;
    private int indexCheckpoint = 0;
    private bool isNearObject;
    private GameObject textPrefab;
    private GameObject dialoguePrefab;
    private TMP_Text dialogueText;
    //private Canvas canvas;
    //private GameObject buyMenu;
    [SerializeField] private bool isInteracting = false;

    private void Start()
    {
        // Find the Canvas component in the scene and assign it to the canvas variable
        //canvas = FindObjectOfType<Canvas>();

        // Find the BuyMenu script in the scene and assign it to the buyMenu variable
        //buyMenu = FindObjectOfType<GameObject>();

    }
    // Update is called once per frame
    void Update()
    {
        if (playerDetection)
        {
            
            if (!isNearObject)
            {
                textPrefab = Instantiate(pressKeyToTalkPrefab, canvas.transform);
                textPrefab.GetComponent<TMP_Text>().text = pressKeyToTalkText;
                isNearObject = true;
            }
            
            if (isInteracting)
            {    
                NPCActions(startDialogueLines, endDialogueLines);               
            }
        }
        else if(isNearObject)
        {
            DialogueVariablesReset();
        }
        isInteracting = false;
    }

    public void Interact()
    {
        isInteracting = true;
    }

    private void NPCActions(string[] startDialogue, string[] endDialogue)
    {

        if (startIndex != startDialogue.Length )
        {
            WriteDialogue(startDialogue, ref startIndex);
        }
        else if (startIndex == startDialogue.Length && endIndex != endDialogue.Length)
        {
            if (isBarter && !hasBarted)
            {
                BuyMenuActions();
            }
            else
            {
                Debug.Log(endDialogue.Length + " lenght");
                buyMenu.SetActive(false);
                WriteDialogue(endDialogue, ref endIndex);
            }
        }
        else
        {
            DialogueVariablesReset();
        }

    }

    private void WriteDialogue(string[] dialogue, ref int index)
    {
        if (dialoguePrefab == null) // Instantiate the dialoguePrefab if it doesn't exist
        {
            dialoguePrefab = Instantiate(dialogueTemplatePrefab, canvas.transform);
            dialogueText = dialoguePrefab.GetComponentInChildren<TMP_Text>();
        }

        // Check if index is within the bounds of the dialogue array
        
        if (index < dialogue.Length)
        {
            dialogueText.text = dialogue[index];
            Debug.Log(dialogueText.text);
            index++;
            Debug.Log(index);
        }
        else
        {

            // All dialogue lines have been displayed, reset dialogue variables and destroy UI elements
            DialogueVariablesReset();
            Destroy(dialoguePrefab); // Destroy the dialoguePrefab after the last line
        }
        
    }

    private void DialogueVariablesReset()
    {
        Debug.Log("Dialogue variables reset");
        Destroy(textPrefab);
        Destroy(dialoguePrefab);
        buyMenu.SetActive(false);
        startIndex = 0;
        endIndex = 0;
        hasBarted = false;
        isNearObject = false;
    }

    private void BuyMenuActions()
    {
        Destroy(textPrefab);
        Destroy(dialoguePrefab);

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

