using UnityEngine;
using System.Collections;

public class EnemyFlyIdle : EnemyController
{
    [SerializeField] private AudioClip flyingAudio;
    [SerializeField] private float timeToFreeze;
    [SerializeField] private float freezeTime;

    //Player
    private Transform _playerTransform;
    private float timer = 0;
    //private float timer2 = 0;
    private float currentMoveSpeed;
    private int currentEnemyDamage;
    private SpriteRenderer sprite;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        _playerTransform = GameManager.instance._player.transform;
        currentMoveSpeed = moveSpeed;
        currentEnemyDamage = enemyDamage;
    }

    //If the Enemey sees the Player it starts chasing him
    public override void Chase()
    {
        if (timer < timeToFreeze) 
        {
            if (flyingAudio != null)
            {
                AudioSource.PlayClipAtPoint(flyingAudio, transform.position);
            }
            Vector3 playerDirection = _playerTransform.position - transform.position;
            playerDirection.Normalize();
            transform.Translate(playerDirection * moveSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            
        }
        else
        {
            FreezeFly();
            StartCoroutine(UnfreezeFly(freezeTime));
        }
    }

    private IEnumerator UnfreezeFly(float delay) 
    {
        yield return new WaitForSeconds(delay);
        timer = 0;
        moveSpeed = currentMoveSpeed;
        enemyDamage = currentEnemyDamage;
        sprite.color = Color.white;
    }

    private void FreezeFly() 
    {
        sprite.color = Color.blue;
        moveSpeed = 0;
        enemyDamage = 0;
    }
}