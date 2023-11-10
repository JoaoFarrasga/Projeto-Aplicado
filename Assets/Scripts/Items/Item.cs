using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite icon;

    public void Start()
    {
        // Use in final product only
        // Check PlayerPrefs to see if the item was picked up
        /*
        if (PlayerPrefs.HasKey(itemName) && PlayerPrefs.GetInt(itemName) == 1)
        {
            // If the item was picked up, keep it deactivated
            gameObject.SetActive(false);
            GetComponent<Collider2D>().enabled = false;
        }
        */
    }

    //Update to update Logic of the Item
    public virtual void Update()
    {

    }

    //On Pick Up happens when the Items Collides with the Player, Item Logic Here
    public virtual void OnPickUp(Collider2D collision)
    {
        // Save the item as picked up
        PlayerPrefs.SetInt(itemName, 1); // You can use 1 to indicate the item is picked up

        // Deactivate the item gameObject
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
    }

    //On Trigger Enter 2D gives the start to the Logic of the Item, it also destroys it
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<CharacterController2D>().inventory.Add(this);
            OnPickUp(collision);
        }
    }
}