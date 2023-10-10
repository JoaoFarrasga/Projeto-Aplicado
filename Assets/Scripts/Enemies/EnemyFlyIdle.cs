using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyFlyIdle : EnemyController
{
    [Header("Enemy Fly Idle")]
    public Transform starterPosition;

    [Header("Detection")]
    public float viewRange;
    public LayerMask playerLayer;

    //Enemy States
    private bool _isChase;

    //Player
    private Transform _playerTransform;

    //Override Start to make the _isChase False
    public override void Start()
    {
        _isChase = false;
    }

    //Override Update to make the Enemy check if the player is close if yes chase, if not stay idle.
    public override void Update()
    {
        CheckDetection();

        if (_isChase)
        {
            Chase();
        }
        else
        {
            Idle();
        }
    }

    //Checks if the Player is in View Range of the Enemy, changing the Boolean _isChase in case of Yes
    private void CheckDetection()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, viewRange, playerLayer);

        if (playerCollider != null)
        {
            _isChase = true;
            _playerTransform = playerCollider.transform;
        }
        else if (_isChase)
        {
            _isChase = false;
        }
    }

    //If the Enemy doesn't see the Player it stays Idle
    private void Idle()
    {
        transform.position = Vector2.MoveTowards(transform.position, starterPosition.position, moveSpeed * Time.deltaTime);
    }

    //If the Enemey sees the Player it starts chasing him
    public override void Chase()
    {
        Vector3 playerDirection = _playerTransform.position - transform.position;
        playerDirection.Normalize();
        transform.Translate(playerDirection * moveSpeed * Time.deltaTime);
    }
}