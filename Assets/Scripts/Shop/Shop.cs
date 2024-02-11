using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BGS.Task
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private UIFadeCanvasGroupTween canvasGroup;
        
        [Header("Left Side")]
        [SerializeField] private UIShopItemEntry leftSideEntryPrefab;
        [SerializeField] private RectTransform leftSideEntryParent;
        [SerializeField] private TextMeshProUGUI shopNameText;
        [SerializeField] private TextMeshProUGUI shopTypeText;
        [SerializeField] private TextMeshProUGUI balanceGoldText;
        
        private List<UIShopItemEntry> leftSideEntries = new();
        
        [Header("Right Side")]
        [SerializeField] private UIShopItemEntry rightSideEntryPrefab;
        [SerializeField] private RectTransform rightSideEntryParent;
        [SerializeField] private GameObject purchaseButton;
        [SerializeField] private GameObject sellButton;
        [SerializeField] private TextMeshProUGUI totalValueText;
        
        private List<UIShopItemEntry> rightSideEntries = new();

        [Header("Middle")]
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemDescriptionText;
        [SerializeField] private TextMeshProUGUI itemTypeText;
        [SerializeField] private TextMeshProUGUI pickButtonText;
        
        [Header("Data")]
        [SerializeField] internal ShopData shopData;

        private List<Item> listedItems = new();
        
        private bool selectedEntryIsRightSide;
        private bool isStoreBuyingMode = true;
        
        private int totalValue;
        
        private UIShopItemEntry selectedEntry;

        private void Start()
        {
            listedItems = shopData.ListedItems.OrderBy(x => x.DisplayName).ToList();
            SwitchStoreMode(true);
        }

        public void Show(bool show)
        {
            canvasGroup.Show(show);
            UpdateBalance();
        }

        private void UpdateBalance()
        {
            var pInv = GameManager.Instance.Player.GetComponent<PlayerInventory>();
            if (isStoreBuyingMode)
            {
                balanceGoldText.text = totalValue > 0 
                    ? $"{pInv.OwnedGold.ToString()}<color=#C60009> - {totalValue} = {pInv.OwnedGold - totalValue}</color>" 
                    : pInv.OwnedGold.ToString();
            }
            else
            {
                balanceGoldText.text = totalValue > 0 
                    ? $"{pInv.OwnedGold.ToString()}<color=#0DC600> + {totalValue} = {pInv.OwnedGold + totalValue}</color>" 
                    : pInv.OwnedGold.ToString();
            }
            totalValueText.text = totalValue.ToString();
        }

        public void SwitchStoreMode(bool buying)
        {
            isStoreBuyingMode = buying;
            totalValue = 0;
            ClearRightSide();
            UpdateStoreEntries(!isStoreBuyingMode ? new List<Item>() : listedItems);
            
            shopNameText.text = isStoreBuyingMode ? shopData.ShopName : "Player Inventory";
            shopTypeText.text = isStoreBuyingMode ? shopData.ShopTypeString : "";
            
            purchaseButton.SetActive(isStoreBuyingMode);
            sellButton.SetActive(!isStoreBuyingMode);
            
            UpdateBalance();
        }
        
        public void UpdateStoreEntries(List<Item> _incomingItems)
        {
            if (_incomingItems == null) return;

            foreach(var entry in leftSideEntries)
            {
                Destroy(entry.gameObject);
            }
            leftSideEntries.Clear();

            if (isStoreBuyingMode)
            {
                for (var i = 0; i < _incomingItems.Count; i++)
                {
                    var _item = _incomingItems[i];
                    var shopItem = CreateShopItemEntry(leftSideEntryPrefab, leftSideEntryParent, _item, i, _item.BuyPrice, false);
                    leftSideEntries.Add(shopItem);
                }
            }
            else
            {
                var pInventory = GameManager.Instance.Player.GetComponent<PlayerInventory>();
                var c = -1;
                foreach (var j in pInventory.Items.ToList())
                {
                    var _item = j.Key;
                    var shopItem = CreateShopItemEntry(leftSideEntryPrefab, leftSideEntryParent, _item, c, _item.SellPrice, false);
                    leftSideEntries.Add(shopItem);
                    shopItem.SetItemAmount(j.Value);
                    shopItem.SetAmountToTransact(j.Value);
                }
            }

            if(leftSideEntries.Count > 0) HandleHighlight(leftSideEntries[0]);
        }

        public void PickItem()
        {
            List<UIShopItemEntry> targetEntries = selectedEntry.isInRightSide ? leftSideEntries : rightSideEntries;
            UIShopItemEntry entryPrefab = selectedEntry.isInRightSide ? leftSideEntryPrefab : rightSideEntryPrefab;
            RectTransform entryParent = selectedEntry.isInRightSide ? leftSideEntryParent : rightSideEntryParent;
            int price = isStoreBuyingMode ? selectedEntry.item.BuyPrice : selectedEntry.item.SellPrice;

            var pInventory = GameManager.Instance.Player.GetComponent<PlayerInventory>();
            int playerItemCount = pInventory.Items.ContainsKey(selectedEntry.item) ? pInventory.Items[selectedEntry.item] : 0;

            if (targetEntries.All(x => x.item.ID != selectedEntry.item.ID))
            {
                if (playerItemCount > 0 || isStoreBuyingMode)
                {
                    var newEntry = CreateShopItemEntry(entryPrefab, entryParent, selectedEntry.item, targetEntries.Count, price, true);
                    targetEntries.Add(newEntry);
                    newEntry.SetAmountToTransact(1);
                }
            }
            else
            {
                var existingEntry = targetEntries.First(x => x.item.ID == selectedEntry.item.ID);
                if (existingEntry.itemAmountToTransact < playerItemCount || isStoreBuyingMode)
                {
                    existingEntry.IncrementAmountToTransact();
                }
                else
                {
                    GameUIController.Instance.ShowPopupText($"Not enough {selectedEntry.item.DisplayName}!", Color.red);

                }
            }

            int sum = 0;
            foreach (var entry in rightSideEntries)
            {
                sum += entry.itemAmountToTransact * (isStoreBuyingMode ? entry.item.BuyPrice : entry.item.SellPrice);
            }
            totalValueText.text = sum.ToString();
            totalValue = sum;
            UpdateBalance();
        }

        private UIShopItemEntry CreateShopItemEntry(UIShopItemEntry prefab, RectTransform parent, Item item, int index, int price, bool rightSide)
        {
            var newEntry = Instantiate(prefab, parent);
            newEntry.Setup(item, this, index, price, rightSide);
            return newEntry;
        }

        private void ClearRightSide()
        {
            foreach(var entry in rightSideEntries)
            {
                Destroy(entry.gameObject);
            }
            rightSideEntries.Clear();
        }

        public void HandleHighlight(UIShopItemEntry shopItem)
        {
            selectedEntry = shopItem;
            selectedEntryIsRightSide = shopItem.isInRightSide;
            
            foreach (var item in leftSideEntries)
            {
                item.Highlight(false);
            }
            foreach (var item in rightSideEntries)
            {
                item.Highlight(false);
            }
            
            pickButtonText.text = !selectedEntryIsRightSide ? "Pick" : "Unpick";

            shopItem.Highlight(true);
            itemNameText.text = shopItem.item.DisplayName;
            itemDescriptionText.text = shopItem.item.Description;
            itemTypeText.text = shopItem.item.TypeString;
        }

        
        public void Buy()
        {
            var pInv = GameManager.Instance.Player.GetComponent<PlayerInventory>();
            if (pInv.OwnedGold < totalValue)
            {
                GameUIController.Instance.ShowPopupText("Not enough gold!", Color.red);
                return;
            }
            
            foreach (var entry in rightSideEntries)
            {
                if (pInv.RemoveGold(entry.item.BuyPrice * entry.itemAmountToTransact))
                {
                    pInv.AddItem(entry.item, entry.itemAmountToTransact);
                }
            }
            totalValue = 0;

            ClearRightSide();
            UpdateStoreEntries(listedItems);
            UpdateBalance();
        }

        public void Sell()
        {
            var pInv = GameManager.Instance.Player.GetComponent<PlayerInventory>();
            
            foreach (var entry in rightSideEntries)
            {
                if (pInv.RemoveItem(entry.item, entry.itemAmountToTransact))
                {
                    pInv.AddGold(entry.item.SellPrice * entry.itemAmountToTransact);
                }
            }
            totalValue = 0;

            ClearRightSide();
            UpdateStoreEntries(new List<Item>());
            UpdateBalance();
        }

    }
}