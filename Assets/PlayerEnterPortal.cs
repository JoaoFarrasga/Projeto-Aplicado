using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEnterPortal : MonoBehaviour
{
    [SerializeField] private string PortalDestination = "Village1";
    [SerializeField] private Item ItemGivenToPlayer;
    [SerializeField] private Vector3 PlayerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player enter portal.");
            collision.transform.position = PlayerPosition; 
            SceneManager.LoadScene(PortalDestination);

            if (ItemGivenToPlayer != null)
            {
                collision.GetComponent<CharacterController2D>().inventory.Add(ItemGivenToPlayer);
            }
        }
    }
}
