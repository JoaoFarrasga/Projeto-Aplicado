using UnityEngine;

public class EnemyChargerFly : EnemyController
{
    private Transform _playerTransform;

    [Header("Fly Charger")]
    public float chargingTime = 1f;
    private float chargeTime = 0f;

    public void Start()
    {
        _playerTransform = GameManager.instance._player.transform;
    }

    public override void Chase()
    {
        if (chargeTime < chargingTime)
        {
            chargeTime += Time.deltaTime;

            Debug.Log("Waiting");
        }
        else
        {
            float originalSpeed = moveSpeed;
            moveSpeed *= 2;

            Vector3 playerDirection = _playerTransform.position - transform.position;
            playerDirection.Normalize();
            transform.Translate(playerDirection * moveSpeed * Time.deltaTime);

            Debug.Log("Charger");

            moveSpeed = originalSpeed;
        }
    }

    public override void Attack()
    {
        base.Attack();

        chargeTime = 0f;
    }
}
