using System.Collections;
using UnityEngine;

public class FireItem : Item
{
    private Damager damager;

    public override void OnPickUp(Collider2D collision)
    {
        StartCoroutine(DestroyObject(10.0f));
    }

    private IEnumerator DestroyObject(float delay) 
    {
        yield return new WaitForSeconds(delay);
    }
}
