using UnityEngine;

public class EnemyHidden : EnemyController
{
    [Header("Enemy")]
    public GameObject checkGround;
    public LayerMask groundLayer;

    private bool _isGround;
    private bool _facingRight;

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

        float checkRotation = _facingRight ? 1f : -1f;

        Vector3 moveDirection = new Vector3(checkRotation, 0f, 0f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (!_isGround)
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
