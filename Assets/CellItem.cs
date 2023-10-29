using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellItem : MonoBehaviour
{
    [SerializeField] public Image itemImage;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public Transform materials;

    public void SetUp(CellVariation cellVariation) 
    {
        itemName.text = cellVariation.name;  
    }
}
