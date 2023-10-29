using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CellVariation", menuName = "Cell Variation")]
public class CellVariation : ScriptableObject
{
    public Sprite image;
    public List<Material> materials = new List<Material>();

    public bool isAffordable;

    [Header("Attack Modifiers")]
    public float attackRadius;

    [Header("Primary Attack modifiers")]
    public float primaryAttackDamage;
    public float primaryAttackTimeout;
    public float primaryAttackSpeed;

    [Header("Secondary Attack Modifiers")]
    public float secondaryAttackDamage;
    public float secondaryAttackTimeout;
    public float secondaryAttackSpeed;
}

[System.Serializable]
public class Material 
{
    public string name;
    public int quantity;
}