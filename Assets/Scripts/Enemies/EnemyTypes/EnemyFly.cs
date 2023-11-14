using UnityEngine;

public class EnemyFly : EnemyController
{
    [Header("Flying Enemy")]
    //This need's to Change to a Collider bigger than the Enemy
    public GameObject checkGround;
    public GameObject checkRoof;
    public GameObject checkFront;
    public GameObject checkBack;
    public LayerMask groundLayer;
    

    private Rigidbody2D enemyRB;

    //Move Direction
    [SerializeField] private Vector2 moveDirection = new Vector2(0f, 0f);

    //Checks
    private bool _isGround;
    private bool _isRoof;
    private bool _isFront;
    private bool _isBack;

    public override void Awake()
    {
        
    }

    public void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        if (moveDirection != Vector2.zero)
            return;

        do
        {
            moveDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
        } while (moveDirection.x == 0 && moveDirection.y == 0);
    }

    private void FixedUpdate()
    {
        enemyRB.velocity = moveDirection * moveSpeed;
    }

    public override void Patrol()
    {
        _isGround = Physics2D.OverlapCircle(checkGround.transform.position, 0.1f, groundLayer);
        _isRoof = Physics2D.OverlapCircle(checkRoof.transform.position, 0.1f, groundLayer);
        _isFront = Physics2D.OverlapCircle(checkFront.transform.position, 0.1f, groundLayer);
        _isBack = Physics2D.OverlapCircle(checkBack.transform.position, 0.1f, groundLayer);

        if (_isFront || _isBack) FlipX();
        if (_isGround || _isRoof) FlipY();
    }

    void FlipX()
    {
        //transform.Rotate(new Vector2(0, 180));
        moveDirection.x *= -1f;
    }

    void FlipY()
    {
        moveDirection.y *= -1f;
    }
}