using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : Item
{
    [Header("Health Item Info")]
    public int health;

    public override void OnPickUp(Collider2D collision)
    {
        collision.GetComponent<IHealable>().Heal(health);
    }
}
