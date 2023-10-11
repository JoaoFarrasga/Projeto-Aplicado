using System.Collections;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _damageAmountePerSecond = 10f;
    private bool isDamaging;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null && !isDamaging)
        {
            isDamaging = true;
            StartCoroutine(ApplyDamageOverTime(damageable));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isDamaging = false;
    }

    private IEnumerator ApplyDamageOverTime(IDamageable damageable)
    {
        while (isDamaging)
        {
            damageable.Damage(_damageAmountePerSecond);
            yield return new WaitForSeconds(1f);
        }
    }
}


