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
    //[SerializeField] private GameObject canvas;
    [SerializeField] private GameObject dialogueTemplatePrefab;
    //[SerializeField] private GameObject buyMenu;
    [SerializeField] private string pressKeyToTalkText = "Press F to talk";
    [SerializeField] private GameObject pressKeyToTalkPrefab;

    private bool hasBarted = false;
    //private string dialogueText;
    private bool playerDetection = false;
    private int startIndex = 0;
    private int endIndex = 0;
    private int indexCheckpoint = 0;
    private bool isNearObject;
    private bool isWritingDialogue;
    private GameObject textPrefab;
    private GameObject dialoguePrefab;
    private TMP_Text dialogueText;
    private Canvas canvas;
    private GameObject buyMenu;
    [SerializeField] private bool isInteracting = false;

    private void Start()
    {
        // Find the Canvas component in the scene and assign it to the canvas variable
        canvas = FindObjectOfType<Canvas>();

        // Find the BuyMenu script in the scene and assign it to the buyMenu variable
        buyMenu = FindObjectOfType<GameObject>();

        // Now you can use canvas and buyMenu references in your script
        if (canvas != null)
        {
            // Do something with the canvas
        }

        if (buyMenu != null)
        {
            // Do something with the buyMenu
        }
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
        else if(isWritingDialogue)
        {
            DialogueVariablesReset();
        }

    }

    private void WriteDialogue(string[] dialogue, ref int index)
    {
        //dialogueTemplate.SetActive(true);
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
        buyMenu.SetActive(false);
        startIndex = 0;
        endIndex = 0;
        hasBarted = false;
        isNearObject = false;
        isWritingDialogue = false;
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

