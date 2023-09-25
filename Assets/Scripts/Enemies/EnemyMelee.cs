using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMelee : EnemyController
{
    [Header("Enemy")]
    public GameObject checkGround;
    public LayerMask groundLayer;

    //States
    private bool _isGround;
    private bool _facingRight;

    public override void Patrol()
    {
        _isGround = Physics2D.OverlapCircle(checkGround.transform.position, 0.1f, groundLayer);

        float checkRotation = _facingRight ? 1f : -1f;

        Vector3 moveDirection = new Vector3(checkRotation, 0f, 0f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (!_isGround)
        {
            Flip();
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(new Vector3(0, 180, 0));

        moveSpeed = -moveSpeed;
    }
}
