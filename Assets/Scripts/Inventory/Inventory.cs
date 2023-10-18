using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField]
    public class Slot
    {
        public ItemType type;
        public int quantity;
        public int maxQuantity;

        public Sprite icon;

        public Slot() 
        {
            type = ItemType.NONE;
            quantity = 0;
            maxQuantity = 99;
        }

        public bool CanAddItem()
        {
            if (quantity < maxQuantity)
            {
                return true;
            }

            return false;
        }

        public void AddItem(Item item)
        {
            this.type = item.type;
            this.icon = item.icon;
            quantity++;
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int num_Slots)
    {
        for (int i = 0; i < num_Slots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Add(Item item)
    {
        foreach(Slot slot in slots)
        {
            if (slot.type == item.type && slot.CanAddItem())
            {
                slot.AddItem(item);
                return;
            }
        }

        foreach(Slot slot in slots)
        {
            if(slot.type == ItemType.NONE)
            {
                slot.AddItem(item);
                return;
            }
        }
    }
}
