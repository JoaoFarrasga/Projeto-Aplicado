using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathItem : Item
{
    [SerializeField] private float deathRange;
    [SerializeField] private float deathDamage;

    public int circleSegments = 30;



    public override void OnPickUp(Collider2D collision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, deathRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Enemy")
            {
                Debug.Log("ENEMY FOUND");
                colliders[i].GetComponent<IDamageable>()?.Damage(deathDamage);
            }
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, deathRange);

        // Draw a circle to visualize the attack range
        float angleStep = 360f / circleSegments;
        Vector3 prevPos = Vector3.zero;
        for (int i = 0; i <= circleSegments; i++)
        {
            float angle = Mathf.Deg2Rad * i * angleStep;
            Vector3 newPos = transform.position + new Vector3(Mathf.Cos(angle) * deathRange, Mathf.Sin(angle) * deathRange, 0f);
            if (i > 0)
            {
                //Gizmos.DrawLine(prevPos, newPos);
            }
            prevPos = newPos;
        }
    }
}
