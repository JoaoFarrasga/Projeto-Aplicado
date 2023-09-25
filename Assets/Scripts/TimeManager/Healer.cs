using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private float _healAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ApplyHeal(other.GetComponent<IHealable>());
    }

    protected void ApplyHeal(IHealable healable)
    {
        healable.Heal(_healAmount);
    }
}
