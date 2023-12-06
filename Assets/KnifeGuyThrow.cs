using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KnifeGuyThrow : MonoBehaviour
{
    [SerializeField] private Transform knifeOrigin;
    [SerializeField] private GameObject knife;
    [SerializeField] private float knifeCooldownMax;
    [SerializeField] private float knifeCooldownMin;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwForceUp;
    [SerializeField] private float knifeLifeSpan;
    [SerializeField] private GameObject player;
    private bool knifeDespawn;

    private float timer;
    private float knifeCooldown;
    private Vector2 knifeThrowDirection;
    private EnemyPlayerRange range;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        range = GetComponent<EnemyPlayerRange>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(range.playerIsInRange);
        if (range.playerIsInRange)
        {
            knifeCooldown = Random.Range(knifeCooldownMin, knifeCooldownMax);
            knifeThrowDirection = new Vector2((player.transform.position.x - transform.position.x), throwForceUp);
            timer += Time.deltaTime;
            if (timer >= knifeCooldown)
            {
                ThrowKnife();
                timer = 0;
            }
        }
    }

    void ThrowKnife()
    {
        GameObject newObject = Instantiate(knife, knifeOrigin.position, Quaternion.identity);
        Rigidbody2D knifeRb = newObject.GetComponent<Rigidbody2D>();

        if (knifeRb != null)
        {
            // Apply an upward force to make the knife go up
            Debug.Log("Knife force");
            knifeRb.AddForce(knifeThrowDirection * throwForce, ForceMode2D.Impulse);
            float i = 0;
            StartCoroutine(DestroyKnifeAfterDelay(newObject, knifeLifeSpan));
        }
    }

    IEnumerator DestroyKnifeAfterDelay(GameObject knifeObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(knifeObject);
    }
}
