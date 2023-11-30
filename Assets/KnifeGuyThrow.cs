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

    private float timer;
    private float knifeCooldown;

    // Update is called once per frame
    void Update()
    {
        knifeCooldown = Random.Range(knifeCooldownMin, knifeCooldownMax);
        timer += Time.deltaTime;
        if (timer >= knifeCooldown)
        {
            ThrowKnife();
            timer = 0;
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
            knifeRb.AddForce(new Vector2(-1, 1).normalized * throwForce, ForceMode2D.Impulse);
        }
    }
}
