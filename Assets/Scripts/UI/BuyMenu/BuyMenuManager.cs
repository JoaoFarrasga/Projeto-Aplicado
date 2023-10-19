using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyMenuManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public int numberOfItems = 5;
    public CellVariation[] cells;

    public Inventory inventory;
    Inventory.Slot slot;

    void Start()
    {
        AddItemsToContainer(cells.Length);
        inventory = GameManager.instance._player.GetComponent<Inventory>();
    }


    void AddItemsToContainer(int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            CellVariation selectedVariation = cells[i];

            GameObject cell = Instantiate(selectedVariation.cellPrefab, transform);
            Debug.Log("Hello" + selectedVariation.itemName + selectedVariation.ironValue);

            Transform textTransform = cell.transform.GetChild(2);
            TextMeshProUGUI textComponent = textTransform.GetComponent<TextMeshProUGUI>();
            if (textComponent != null) 
            {
                textComponent.text = selectedVariation.ironValue.ToString();
            }

            Transform textTransform1 = cell.transform.GetChild(3);
            TextMeshProUGUI textComponent1 = textTransform1.GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent1.text = selectedVariation.woodValue.ToString();
                /*
                if ((inventory.CheckQuantity(ItemType.WOOD) < selectedVariation.woodValue) || (inventory.CheckQuantity(ItemType.IRON) < selectedVariation.ironValue))
                {
                    cells[i].isAffordable = false;
                }
                else
                {
                    cells[i].isAffordable = true;
                }
                */
            }

            Transform textTransform2 = cell.transform.GetChild(5);
            TextMeshProUGUI textComponent2 = textTransform2.GetComponent<TextMeshProUGUI>();
            if (textComponent2 != null) textComponent2.text = selectedVariation.itemName.ToString();

            cell.name = cells[i].name;
            

        }
    }
}
