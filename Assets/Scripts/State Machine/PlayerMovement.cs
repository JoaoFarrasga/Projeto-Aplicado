using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Inputs input;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool dash = false;
    public bool isAttacking;
    public bool isGrounded;
    public bool canDoubleJump;

    void Update()
    {
        canDoubleJump = GetComponent<CharacterController2D>().canDoubleJump;

        isGrounded = GetComponent<CharacterController2D>().m_Grounded;

        isAttacking = GetComponent<Attack>().isAttacking;

        horizontalMove = input.move.x * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (input.jump)
            jump = true;

    }

    public void OnFall()
    {
        animator.SetBool("IsJumping", true);
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            // Move our character
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
            jump = false;
            input.jump = false;
            dash = false;
            animator.SetBool("CanWalk", true);
        }
        else if (isAttacking && !isGrounded)
        {
            dash = false;
            jump = false;
            input.jump = false;
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
            canDoubleJump = false;
            animator.SetBool("CanWalk", false);
        }
        else if (isAttacking && isGrounded)
        {
            dash = false;
            jump = false;
            input.jump = false;
            controller.Move(0, jump, dash);
            animator.SetBool("CanWalk", false);
        }

    }
}
