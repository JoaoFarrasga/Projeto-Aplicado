using UnityEngine;

public class ItemHealth : Item
{
    [Header("Health Item Info")]
    //Health Item Info
    public int health;

    //Heals Player On Pick Up the Item
    public override void OnPickUp(Collider2D collision)
    {
        collision.GetComponent<IHealable>().Heal(health);

        Destroy(gameObject);
    }
}
