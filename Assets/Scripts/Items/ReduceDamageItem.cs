using System.Collections;
using UnityEngine;

public class ReduceDamageItem : Item
{
    [Header("Reduce Damage Item Info")]
    public int reduceMultiplyer;
    public float effectTime;

    private TimeManager timeManager;

    public override void OnPickUp(Collider2D collision)
    {
        timeManager = collision.GetComponent<TimeManager>();

        StartCoroutine(TimeCoroutine());
    }

    private IEnumerator TimeCoroutine()
    {
        timeManager.multiplyer *= reduceMultiplyer;

        yield return new WaitForSeconds(effectTime);

        timeManager.multiplyer *= 1 / reduceMultiplyer;

        Destroy(gameObject);
    }
}
