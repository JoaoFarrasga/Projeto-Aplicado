using UnityEngine;
using System.Collections;

public class EnemyFlyIdle : EnemyController
{
    [SerializeField] private AudioClip flyingAudio;
    [SerializeField] private float freezeTime;

    //Player
    private Transform _playerTransform;
    private float timer = 0;
    private float currentMoveSpeed;
    private SpriteRenderer sprite;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        _playerTransform = GameManager.instance._player.transform;
        currentMoveSpeed = moveSpeed;
    }

    //If the Enemey sees the Player it starts chasing him
    public override void Chase()
    {
        if (timer < freezeTime) 
        {
            if (flyingAudio != null)
            {
                AudioSource.PlayClipAtPoint(flyingAudio, transform.position);
            }
            Vector3 playerDirection = _playerTransform.position - transform.position;
            playerDirection.Normalize();
            transform.Translate(playerDirection * moveSpeed * Time.deltaTime);
            timer++;
        }
        else
        {
            sprite.color = Color.blue;
            moveSpeed = 0;
            StartCoroutine(UnfreezeFly(4));
        }
    }

    IEnumerator UnfreezeFly(float delay)
    {
        yield return new WaitForSeconds(delay);
        sprite.color = Color.white;
        moveSpeed = 0;
        timer = 0;
    }
}