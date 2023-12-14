using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostItem : Item
{
    private GameObject player;
    private int originalLayer;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    public override void OnPickUp(Collider2D collider)
    {
        originalLayer = player.layer;

        MoveGameObject(new Vector3(1000f, 1000f, 1000f));

        StartCoroutine(ChangeLayerTemporarily(10.0f, LayerMask.NameToLayer("Invisible")));
    }

    IEnumerator ChangeLayerTemporarily(float duration, int newLayer)
    {
        player.layer = newLayer;

        yield return new WaitForSeconds(duration);

        player.layer = originalLayer;

        Destroy(gameObject);
    }

    void MoveGameObject(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
