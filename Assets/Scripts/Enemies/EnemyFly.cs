using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyFly : EnemyController
{
    [Header("Flying Enemy")]
    //This need's to Change to a Collider bigger than the Enemy
    public GameObject checkGround;
    public GameObject checkRoof;
    public GameObject checkFront;
    public LayerMask groundLayer;

    private Rigidbody2D enemyRB;

    //Move Direction
    private Vector2 moveDirection;

    //Checks
    private bool _isGround;
    private bool _isRoof;
    private bool _isFront;

    public void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        moveDirection = new Vector2(1f, 0.25f);
    }

    public void Update()
    {
        Patrol();
    }

    private void FixedUpdate()
    {
        enemyRB.velocity = moveDirection * moveSpeed;
    }

    public void Patrol()
    {
        _isGround = Physics2D.OverlapCircle(checkGround.transform.position, 0.1f, groundLayer);
        _isRoof = Physics2D.OverlapCircle(checkRoof.transform.position, 0.1f, groundLayer);
        _isFront = Physics2D.OverlapCircle(checkFront.transform.position, 0.1f, groundLayer);

        if (_isFront) FlipX();
        if (_isGround || _isRoof) FlipY();
    }

    void FlipX()
    {
        transform.Rotate(new Vector2(0, 180));
        moveDirection.x *= -1f;
    }

    void FlipY()
    {
        moveDirection.y *= -1f;
    }
}