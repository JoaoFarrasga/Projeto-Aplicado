using UnityEngine;

public class EnemyMelee : EnemyController
{
    [Header("Enemy")]
    public GameObject checkGround;
    public GameObject checkWall;
    public GameObject checkEnemy;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    private bool _isGround;
    private bool _facingRight;
    private bool _isFacingWall;
    private bool _isFacingEnemy;

    //Patrol Logic, this Enemy does not chase the player, making him change directions when it doesn't have ground
    public override void Patrol()
    {
        _isGround = Physics2D.OverlapCircle(checkGround.transform.position, 0.1f, groundLayer);
        _isFacingWall = Physics2D.OverlapCircle(checkWall.transform.position, 0.1f, groundLayer);
        _isFacingEnemy = Physics2D.OverlapCircle(checkEnemy.transform.position, 0.1f, enemyLayer);
        
        float checkRotation = _facingRight ? 1f : -1f;

        Vector3 moveDirection = new Vector3(checkRotation, 0f, 0f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (!_isGround || _isFacingWall || _isFacingEnemy)
        {
            Debug.Log("is facing wall: " + _isFacingWall);
            Debug.Log("is ground: " + _isGround);
            Flip();
        }
    }

    //Flips the Enemy, used in Patrol whenever needed
    void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(new Vector3(0, 180, 0));

        moveSpeed = -moveSpeed;
    }
}
