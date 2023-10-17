using System;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D m_Rigidbody2D;

    [Header("Checks")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform attackCheck;
    public bool grounded;
    public bool walled;
    const float checkRadius = .2f;


    [Header("Movement")]
    public float speed = 5;
    [SerializeField] private float smoothing = 0.1f;
    public float deadZone = 0.1f;
    private Vector3 zero = Vector3.zero;


    [Header("Player")]
    public float hurtTimeout = 1f;
    public float deathTimeout = 3f;

    [Header("Attacks")]
    public float attackRadius = .3f;
    public float attackGravityCancel = 5f;

    [Header("PrimaryAttack")]
    public float primaryAttackDamage = 5f;
    public float primaryAttackTimeout = 0.3f;
    public float primaryAttackSpeed = 2f;

    [Header("SecondaryAttack")]
    public float secondaryAttackDamage = 10f;
    public float secondaryAttackTimeout = 0.5f;
    public float secondaryAttackSpeed = 1f;

    [Header("Jump")]
    public bool doubleJump;
    [SerializeField] private float jumpForce = 800f;
    [SerializeField] private float doubleJumpMultiplier = 0.8f;
    public float jumpTimeout = 0.35f;
    public float maxFallSpeed = 25f;

    [Header("Dash")]
    [SerializeField] private float dashForce = 50f;
    public float dashTimeout = 0.25f;
    public bool canDash;

    [Header("Inventory")]
    public Inventory inventory;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        inventory = new Inventory(24);
    }

    private void FixedUpdate()
    {
        grounded = false;
        walled = false;

        CheckGrounded();
        CheckWall();
    }


    private void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, checkRadius, groundLayer);

        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                grounded = true;
                doubleJump = true;
                break;
            }
        }
    }

    private void CheckWall()
    {
        if (!grounded)
        {
            Collider2D[] collidersWall = Physics2D.OverlapCircleAll(wallCheck.position, checkRadius, groundLayer);

            foreach (var collider in collidersWall)
            {
                if (collider.gameObject != null)
                {
                    walled = true;
                    break;
                }
            }
        }
    }

    public void Move(float move)
    {
        move *= speed * 100 * Time.fixedDeltaTime;
        Vector3 targetVelocity = new Vector2(move, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref zero, smoothing);
        Flip(move);
    }

    public void Dash()
    {
        canDash = false;
        m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * dashForce, 0);
    }

    public void Jump(bool isDoubleJump = false, bool isWallJump = false)
    {
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f); // Reset vertical velocity

        float horizontalForce = (isWallJump) ? -transform.localScale.x * jumpForce * 1.2f : 0f;
        float verticalForce = jumpForce * (isDoubleJump ? doubleJumpMultiplier : 1f);

        m_Rigidbody2D.AddForce(new Vector2(horizontalForce, verticalForce));
    }

    public void Flip(float move)
    {
        if (move == 0) return;
        if (Mathf.Sign(transform.localScale.x) != Mathf.Sign(move))
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
    }

    public void Attack(float damageAmount)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                IDamageable damageable = colliders[i].GetComponent<IDamageable>();
                damageable?.Damage(damageAmount);
            }
        }
    }

    public void Grapple()
    {

    }
}
