using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BGS.Task
{
    public class Inventory : MonoBehaviour
    {
        protected Dictionary<Item, int> items = new();
        public Dictionary<Item, int> Items => items;
        
        public Action<Dictionary<Item,int>> OnInventoryChanged;
        
        public bool HasItem(Item item)
        {
            return items.Keys.Any(x => x.ID == item.ID);
        }

        public bool HasEnough(Item item, int spendAmount)
        {
            return HasItem(item) && items[item] >= spendAmount;
        }

        protected virtual void Start()
        {
            Initialize();   
        }

        public virtual void Initialize()
        {
            
        }

        public virtual void UpdateSlots()
        {
            
        }
        
        public virtual void AddItem(Item item, int amount)
        {
            if (HasItem(item))
            {
                items[item] += amount;
            }
            else
            {
                items.Add(item, amount);
            }
            OnInventoryChanged?.Invoke(items);
        }
    
        public virtual bool RemoveItem(Item item, int amount)
        {
            if(!HasEnough(item, amount)) return false;
            
            items[item] -= amount;
            if (items[item] <= 0) items.Remove(item); 
            OnInventoryChanged?.Invoke(items);
            return true;
        }

        public virtual void Discard(Item item)
        {
            if(!HasItem(item)) return;
            items.Remove(item);
            OnInventoryChanged?.Invoke(items);
        }

        public virtual void ClearInventory()
        {
            items.Clear();
            OnInventoryChanged?.Invoke(items);
        }
    }
}
