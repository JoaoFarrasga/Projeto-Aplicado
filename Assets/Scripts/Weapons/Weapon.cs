using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/Create New weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;

    public float primaryDamage;
    public float primaryAttackSpeed;
    public float primaryCooldown;

    public float secondaryDamage;
    public float secondaryAttackSpeed;
    public float secondaryCooldown;

    public float range;
}
