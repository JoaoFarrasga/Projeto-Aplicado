using UnityEngine;

public class EnemyHidden : EnemyController
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

    private bool _isHidden;
    private SpriteRenderer _spriteRenderer;

    [Tooltip("This is a False Chase")]

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Hide();
    }

    public override void Chase()
    {
        Appear();

        _isGround = Physics2D.OverlapCircle(checkGround.transform.position, 0.1f, groundLayer);
        _isFacingWall = Physics2D.OverlapCircle(checkWall.transform.position, 0.1f, groundLayer);
        _isFacingEnemy = Physics2D.OverlapCircle(checkEnemy.transform.position, 0.1f, enemyLayer);

        float checkRotation = _facingRight ? 1f : -1f;

        Vector3 moveDirection = new Vector3(checkRotation, 0f, 0f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (!_isGround || _isFacingWall || _isFacingEnemy)
        {
            Flip();
        }
    }

    public override void Patrol()
    {
        Hide();
    }

    void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(new Vector3(0, 180, 0));

        moveSpeed = -moveSpeed;
    }

    void Appear()
    {
        _isHidden = false;
        _spriteRenderer.enabled = true;
    }

    void Hide()
    {
        _isHidden = true;
        _spriteRenderer.enabled = false;
    }
}
