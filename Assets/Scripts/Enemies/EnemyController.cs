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

    //Start to Initialize Variables
    public virtual void Start()
    {

    }

    //Update to Update the Logic being used
    public virtual void Update()
    {

    }

    //Logic that the Enemy Uses to Chase the Enemy
    public virtual void Chase()
    {
        
    }

    //Logic that makes the Enemy Patrol
    public virtual void Patrol()
    {

    }

    //Check if Collision with the Player Happend and Gives Damage to It
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UnityEngine.Debug.Log("You Recieved: " + enemyDamage);
        }
    }
}
