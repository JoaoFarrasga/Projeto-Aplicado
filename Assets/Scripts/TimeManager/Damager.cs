using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _damageAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ApplyDamage(other.GetComponent<IDamageable>());
    }

    protected void ApplyDamage(IDamageable damageable)
    {
        damageable.Damage(_damageAmount);
    }
}
