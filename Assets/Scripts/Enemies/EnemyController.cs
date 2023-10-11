using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    private TimeManager timeManager;

    [Header("Enemy Starter")]
    public int enemyDamage;
    public float moveSpeed;

    [Header("Weight")]
    public bool _isHeavy;
    private void Awake()
    {
        timeManager = GetComponent<TimeManager>();
        timeManager.OnDeathAction += OnDeath;
        timeManager.MaxValue = Random.Range(15, 61);
        timeManager.Value = timeManager.MaxValue;
    }

    //Check if Collision with the Player Happend and Gives Damage to It
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<IDamageable>()?.Damage(enemyDamage);
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
