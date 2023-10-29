using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialItem : MonoBehaviour
{
    [SerializeField] public TMP_Text materialName;
    [SerializeField] public TMP_Text materialQuantity;

    public void SetUp(Material material)
    {
        materialName.text = material.name;
        materialQuantity.text = material.quantity.ToString();
    }
}
