using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    private TimeManager timeManager;

    [Header("Enemy Starter")]
    public int enemyDamage;
    public float moveSpeed;

    [Header("Enemy TimeOuts")]
    public float attackTimeOut;
    public float deathTimeOut;

    [Header("Player")]
    public LayerMask playerLayer;
    public int viewRange;

    [Header("Weight")]
    public bool _isHeavy;

    [HideInInspector] public bool seePlayer;

    private void Awake()
    {
        timeManager = GetComponent<TimeManager>();
        timeManager.MaxValue = Random.Range(15, 61);
        timeManager.Value = timeManager.MaxValue;
    }

    public virtual void Patrol()
    {

    }

    public virtual void Chase()
    {

    }

    public void Update()
    {
        CheckDetection();
    }

    public void CheckDetection()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, viewRange, playerLayer);

        seePlayer = (playerCollider != null);
    }

    //Check if Collision with the Player Happend and Gives Damage to It
    public virtual void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 1.0f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Player")
            {
                IDamageable damageable = colliders[i].GetComponent<IDamageable>();
                damageable?.Damage(enemyDamage);
            }
        }
    }
}
