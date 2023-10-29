using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField]
    public class Slot
    {
        public string name;
        //public ItemType type;
        public int quantity;
        public int maxQuantity;

        public Sprite icon;

        public Slot() 
        {
            name = "";
            //type = ItemType.NONE;
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
            this.name = item.itemName;
            //this.type = item.type;
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
        foreach (Slot slot in slots)
        {
            if (slot.name == item.itemName && slot.CanAddItem())
            {
                slot.AddItem(item);
                return;
            }
        }
        
        foreach (Slot slot in slots)
        {
            if (slot.name == "")
            {
                slot.AddItem(item);
                return;
            }
        }
    }

    public void RemoveItem(string itemName, int quantityToRemove)
    {
        foreach (Slot slot in slots)
        {
            if (slot.name == itemName)
            {
                if (slot.quantity >= quantityToRemove)
                {
                    slot.quantity -= quantityToRemove;
                }
                else
                {
                    // If the quantity to remove is greater than the available quantity, set quantity to 0.
                    slot.quantity = 0;
                }
                return;
            }
        }
    }

    public int CheckQuantity(Material material)
    {
        foreach (Slot slot in slots)
        {
            if (slot.name == material.name)
            {
                return slot.quantity;
            }
        }

        return 0;
    }
}