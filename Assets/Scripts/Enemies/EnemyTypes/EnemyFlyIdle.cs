using UnityEngine;

public class EnemyFlyIdle : EnemyController
{ 
    //Player
    private Transform _playerTransform;

    public void Start()
    {
        _playerTransform = GameManager.instance._player.transform;
    }

    //If the Enemey sees the Player it starts chasing him
    public override void Chase()
    {
        Vector3 playerDirection = _playerTransform.position - transform.position;
        playerDirection.Normalize();
        transform.Translate(playerDirection * moveSpeed * Time.deltaTime);
    }
}