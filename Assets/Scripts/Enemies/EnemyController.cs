using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    [Header("Enemy Starter")]
    public int enemyDamage;
    public float moveSpeed;

    [Header("Weight")]
    public bool _isHeavy;

    //Check if Collision with the Player Happend and Gives Damage to It
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<IDamageable>().Damage(enemyDamage);
        }
    }
}
