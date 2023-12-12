using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KnifeGuyThrow : EnemyController
{
    [SerializeField] private Transform knifeOrigin;
    [SerializeField] private GameObject knife;
    [SerializeField] private float knifeCooldownMax;
    [SerializeField] private float knifeCooldownMin;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwForceUp;
    [SerializeField] private float knifeLifeSpan;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip knifeThrowSound;

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

    private bool knifeDespawn;

    private float timer;
    private float knifeCooldown;
    private Vector2 knifeThrowDirection;
    private EnemyPlayerRange range;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        range = GetComponent<EnemyPlayerRange>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Debug.Log(range.playerIsInRange);
            if (range.playerIsInRange)
            {
                knifeCooldown = Random.Range(knifeCooldownMin, knifeCooldownMax);
                knifeThrowDirection = new Vector2((player.transform.position.x - transform.position.x), throwForceUp);
                timer += Time.deltaTime;
                if (timer >= knifeCooldown)
                {
                    ThrowKnife();
                    timer = 0;
                }
            }
        }   
    }

    void ThrowKnife()
    {
        AudioSource.PlayClipAtPoint(knifeThrowSound, transform.position);
        GameObject newObject = Instantiate(knife, knifeOrigin.position, Quaternion.identity);
        Rigidbody2D knifeRb = newObject.GetComponent<Rigidbody2D>();

        if (knifeRb != null)
        {
            // Apply an upward force to make the knife go up
            Debug.Log("Knife force");
            knifeRb.AddForce(knifeThrowDirection * throwForce, ForceMode2D.Impulse);
            float i = 0;
            StartCoroutine(DestroyKnifeAfterDelay(newObject, knifeLifeSpan));
        }
    }

    IEnumerator DestroyKnifeAfterDelay(GameObject knifeObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(knifeObject);
    }

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
            Debug.Log("is facing ground: " + _isGround);
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
