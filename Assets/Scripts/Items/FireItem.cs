using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FireItem : Item
{
    [SerializeField] private float fireRange;
    [SerializeField] private float fireDamage;
    private GameObject player;
    private bool toggle = false;
    private bool isRepeaterRunning = false;

    public void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

public override void OnPickUp(Collider2D collision)
    {
        toggle = true;

        if (!isRepeaterRunning)
        {
            isRepeaterRunning = true;
            StartCoroutine(Repeater());
            StartCoroutine(DestroyObject(10.0f));
        }
    }

    public void Update()
    {
        if (toggle)
        {
            MoveGameObject();
        }
    }

    private IEnumerator DestroyObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        toggle = false;
        isRepeaterRunning = false;
        Destroy(gameObject);
    }

    void MoveGameObject()
    {
        transform.position = player.transform.position;
    }

    private IEnumerator Repeater()
    {
        while (toggle)
        {
            DealDamage();

            yield return new WaitForSeconds(2.0f);
        }
    }

    void DealDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, fireRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Enemy")
            {
                Debug.Log("ENEMY FOUND");
                colliders[i].GetComponent<IDamageable>()?.Damage(fireDamage);
            }
        }
    }
}
