using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageShoot : EnemyController
{
    [SerializeField] private Transform spellOrigin;
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private float spellCooldown;
    [SerializeField] private float spellLifeSpan;
    [SerializeField] private float spellForce;
    [SerializeField] private float spellSideAngle;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip fireAudio;

    private EnemyPlayerRange spellRange;
    private Vector2 spellDirection;
    private Vector2 spell1Direction;
    private Vector2 spell2Direction;
    private float timer = 0;

    private void Awake()
    {
        timer = spellCooldown;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        spellRange = GetComponent<EnemyPlayerRange>();
    }

    private void Update()
    {
        if (spellRange.playerIsInRange)
        {
            spellDirection = player.transform.position - transform.position;
            spell1Direction = RotateVector(spellDirection, spellSideAngle);
            spell2Direction = RotateVector(spellDirection, -spellSideAngle);
            timer += Time.deltaTime;
            if (timer >= spellCooldown)
            {
                ShootSpell();
                timer = 0;
            }
        }
    }

    private void ShootSpell() 
    {
        AudioSource.PlayClipAtPoint(fireAudio, transform.position);
        GameObject newSpell = Instantiate(spellPrefab, spellOrigin.position, Quaternion.identity);
        GameObject newSpell1 = Instantiate(spellPrefab, spellOrigin.position, Quaternion.identity);
        GameObject newSpell2 = Instantiate(spellPrefab, spellOrigin.position, Quaternion.identity);
        Rigidbody2D spellRb = newSpell.GetComponent<Rigidbody2D>();
        Rigidbody2D spellRb1 = newSpell1.GetComponent<Rigidbody2D>();
        Rigidbody2D spellRb2 = newSpell2.GetComponent<Rigidbody2D>();

        if (spellRb != null)
        {
            // Apply an upward force to make the knife go up
            Debug.Log("Knife force");
            spellRb.AddForce(spellDirection.normalized * spellForce, ForceMode2D.Impulse);
            spellRb1.AddForce(spell1Direction.normalized * spellForce, ForceMode2D.Impulse);
            spellRb2.AddForce(spell2Direction.normalized * spellForce, ForceMode2D.Impulse);
            float i = 0;
            //StartCoroutine(DestroyKnifeAfterDelay(newObject, knifeLifeSpan));
        }
    }

    Vector2 RotateVector(Vector2 vector, float angleDegrees)
    {
        // Create a rotation quaternion based on Euler angles
        Quaternion rotation = Quaternion.Euler(0, 0, angleDegrees);

        // Rotate the vector using the quaternion
        Vector2 rotatedVector = rotation * vector;

        return rotatedVector;
    }
}
