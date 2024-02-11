using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BGS.Task
{
    public class PlayerInventory : Inventory
    {
        [SerializeField] private PlayerInventoryUI UI;
        [SerializeField] private int initialGold = 50;
        [Serializable] internal struct ItemAmount
        {
            [SerializeField] internal Item item;
            [SerializeField] internal int amount;
        }
        
        [SerializeField] private List<ItemAmount> startingItems = new();
        
        private int ownedGold;
        public int OwnedGold => ownedGold;


        protected override void Start()
        {
            base.Start();
            foreach (var i in startingItems.OrderBy(x => x.item.EquippableInHotbar))
            {
                AddItem(i.item,i.amount);
                if (i.item is Outfit outfit)
                {
                    GetComponent<PlayerClothing>().ChangeOutfit(outfit);
                }
            }
            ownedGold = initialGold;
            GameUIController.Instance.OnStoreOpenClose += b => { UI.ForceClose(); };
        }

        private void OnDestroy()
        {
            GameUIController.Instance.OnStoreOpenClose -= b => { UI.ForceClose(); };
        }


        private void Update()
        {
            var key = -1;
            for (int i = 1; i <= 8; i++)
            {
                if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), "Alpha" + i)))
                {
                    key = i;
                    UI.SendKey(key);
                    break;
                }
            }

            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
            {
                UI.ShowNormalInventory();
            }
        }

        public override void AddItem(Item item, int amount)
        {
            base.AddItem(item, amount);
            GetComponent<PlayerInfoMessages>().ShowText($"+{amount} {item.DisplayName}", Color.cyan);
        }


        public void AddGold(int amount)
        {
            ownedGold += amount;
            GetComponent<PlayerInfoMessages>().ShowText($"+{amount} Gold", Color.yellow);
        }
        
        public bool RemoveGold(int amount)
        {
            if(!CanPurchase(amount))
            {
                return false;
            }
            ownedGold -= amount;
            if (ownedGold < 0)
            {
                ownedGold = 0;
            }
            GetComponent<PlayerInfoMessages>().ShowText($"-{amount} Gold", Color.red);
            return true;
        }
        
        private bool CanPurchase(int amount)
        {
            return ownedGold >= amount;
        }
        
        
    }
}
