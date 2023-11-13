using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour, InteractableInterface
{
    [SerializeField] public string[] startDialogueLines;
    [SerializeField] private GameObject dialogueTemplatePrefab;
    [SerializeField] public string pressKeyToInteractText = "Press F to Open";
    [SerializeField] private GameObject pressKeyToTalkPrefab;
    [SerializeField] public bool isLocked = false;
    [SerializeField] private string sceneName;

    private GameObject textPrefab;
    private bool isInteracting = false;
    private bool playerDetection = false;
    private int startIndex = 0;
    private GameObject dialoguePrefab;
    private TMP_Text dialogueText;
    private Canvas canvas;
    private bool isNearObject;
    private bool isWritingDialogue;
    private int keyAmount;
    private bool hasKey = false;

    public CharacterController2D player;

    public void Interact()
    {
        isInteracting = true;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        player = FindObjectOfType<CharacterController2D>();

        if (player == null)
        {
            Debug.LogError("CharacterController2D object not found in the scene.");
        }

        GameObject canvasPlayer = GameObject.Find("CanvasPlayer");
        if (canvasPlayer != null)
        {
            canvas = canvasPlayer.GetComponent<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("Canvas component not found on CanvasPlayer GameObject.");
            }
        }
        else
        {
            Debug.LogError("CanvasPlayer not found in the scene.");
        }

        // Find the BuyMenu GameObject by name and assign it to the buyMenu variable
        //buyMenu = GameObject.Find("YourBuyMenuObjectName");







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
                textPrefab.GetComponent<TMP_Text>().text = "";
                //isLocked = SearchPlayerKey(isLocked);
                if (!isLocked)
                {
                    DontDestroyOnLoad(player);
                    SceneManager.LoadScene(sceneName);
                    Debug.Log("Door unlocked");
                }
                else 
                {
                    hasKey = SearchPlayerKey(hasKey);
                    if (hasKey == false)
                    {
                        isLocked = false;
                        DontDestroyOnLoad(player);
                        SceneManager.LoadScene(sceneName);
                        Debug.Log("Door unlocked");
                    }
                    else
                    {
                        DoorLockedDialogue(startDialogueLines);
                    }
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
