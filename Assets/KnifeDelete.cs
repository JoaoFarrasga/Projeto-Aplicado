using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDelete : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    [SerializeField] private float knifeDamage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyKnife(gameObject, lifeSpan)); 
    }

    private IEnumerator DestroyKnife(GameObject gameObject, float lifeSpan)
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Knife touch player");
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable?.Damage(knifeDamage);
            Destroy(gameObject);
        }
    }
}
