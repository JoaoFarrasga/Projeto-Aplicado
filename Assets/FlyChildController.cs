using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyChildController : MonoBehaviour
{
    public float movementSpeed = 5f;

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Move the flychild
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

        // Check for collisions with other flychildren
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Flychild"))
            {
                // Adjust movement direction here
                // For example, you could reflect the direction
                movementSpeed *= -1;
            }
        }
    }
}
