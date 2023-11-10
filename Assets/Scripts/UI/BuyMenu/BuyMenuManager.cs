using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyMenuManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject materialPrefab;
    public CellVariation[] cells;
    public CharacterController2D player;


    
    private void Start()
    {
        GameObject findPlayer = GameObject.Find("Player");
        if (findPlayer != null)
        {
            player = findPlayer.GetComponent<CharacterController2D>();
            if (player != null && player.inventory != null)
            {
                // Player and player.inventory are properly initialized
                InitializeShop();
            }
            else
            {
                //Debug.LogError("Player's CharacterController2D component or inventory is null.");
            }
        }
        else
        {
            //Debug.LogError("Player not found in the scene.");
        }
    }
    
    //private GameObject[] cellStore;
    private List<GameObject> cellStore = new List<GameObject>();
    private int affordableChecker = 0;
    private void OnEnable()
    {
        InitializeShop();
    }

    
    private void OnDisable()
    {      
        foreach (GameObject cell in cellStore) 
        {
            Destroy(cell);
        }
    }
    
    void InitializeShop()
    {
        AddItemsToContainer();
    }
    void AddItemsToContainer()
    {
        foreach (CellVariation cellVariation in cells)
        {
            GameObject cell = Instantiate(cellPrefab, transform);
            
            CellItem cellItem = cell.GetComponent<CellItem>();

            cellStore.Add(cell);
            cellItem.itemName.text = cellVariation.name;
            cellItem.itemImage.GetComponent<Image>().sprite = cellVariation.image;

            //Debug.Log("Total Material Quantity for " + cellVariation.name + ": " + totalMaterialQuantity);

            foreach (Material material in cellVariation.materials)
            {
                string materialName = material.name;
                int materialQuantity = material.quantity;

                int playerMaterialAmount = player.inventory.CheckQuantity(material);


                if (playerMaterialAmount < materialQuantity)
                {
                    affordableChecker++;
                    Debug.Log(materialQuantity + " " + materialName + " > " + playerMaterialAmount);
                }

                cellVariation.isAffordable = true;
                if (affordableChecker > 0)
                {
                    cellVariation.isAffordable = false;
                }

                GameObject mat = Instantiate(materialPrefab, cellItem.materials);
                mat.GetComponent<MaterialItem>().SetUp(material);

                affordableChecker = 0;

                Transform buttonPosition = cell.transform.GetChild(1);
                Button button = buttonPosition.GetComponent<Button>();

                button.onClick.AddListener(() => ForgeButton(cellVariation));
            }

        }   
    }

    public void ForgeButton(CellVariation cellClick) 
    {
        if (cellClick.hasBeenForged)
        {
            player.primaryAttackDamage = cellClick.primaryAttackDamage;
            player.primaryAttackTimeout = cellClick.primaryAttackTimeout;
            player.primaryAttackSpeed = cellClick.primaryAttackSpeed;

            player.secondaryAttackDamage = cellClick.secondaryAttackDamage;
            player.secondaryAttackTimeout = cellClick.secondaryAttackTimeout;
            player.secondaryAttackSpeed = cellClick.secondaryAttackSpeed;
        }
        else 
        {
            if (cellClick.isAffordable == true)
            {
                foreach (Material material in cellClick.materials)
                {
                    string materialName = material.name;
                    int materialQuantity = material.quantity;

                    player.primaryAttackDamage = cellClick.primaryAttackDamage;
                    player.primaryAttackTimeout = cellClick.primaryAttackTimeout;
                    player.primaryAttackSpeed = cellClick.primaryAttackSpeed;

                    player.secondaryAttackDamage = cellClick.secondaryAttackDamage;
                    player.secondaryAttackTimeout = cellClick.secondaryAttackTimeout;
                    player.secondaryAttackSpeed = cellClick.secondaryAttackSpeed;

                    player.inventory.RemoveItem(materialName, materialQuantity);
                    cellClick.hasBeenForged = true;
                }
            }
            else
            {
                Debug.Log("Item not affordable");
            }

        }
    }
}
