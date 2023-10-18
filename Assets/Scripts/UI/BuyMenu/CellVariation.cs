using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CellVariation", menuName = "Cell Variation")]
public class CellVariation : ScriptableObject
{
    public GameObject cellPrefab;

    public string itemName;
    public Image image;
    public int ironValue;
    public int woodValue;

    public bool isAffordable;
}
