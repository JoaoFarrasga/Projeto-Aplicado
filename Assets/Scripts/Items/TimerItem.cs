using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerItem : Item
{
    [Header("Timer Item Info")]
    public float extraTime;

    public override void OnPickUp(Collider2D collision)
    {
        collision.GetComponent<TimeManager>().MaxValue += extraTime;
    }
}
