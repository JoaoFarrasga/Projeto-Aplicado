using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/Create New weapon")]
public class Weapon : ScriptableObject
{
    public string name;

    public float lightDamage;
    public float lightCooldown;

    public float heavyDamage;
    public float heavyCooldown;

    public float range;

}
