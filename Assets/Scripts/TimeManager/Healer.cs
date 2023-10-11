using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private float _damageAmountePerSecond = 10f;
    private bool isHealing;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IHealable healable = other.GetComponent<IHealable>();
        if (healable != null && !isHealing)
        {
            isHealing = true;
            StartCoroutine(ApplyDamageOverTime(healable));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isHealing = false;
    }

    private IEnumerator ApplyDamageOverTime(IHealable healable)
    {
        while (isHealing)
        {
            healable.Heal(_damageAmountePerSecond);
            yield return new WaitForSeconds(1f);
        }
    }
}
