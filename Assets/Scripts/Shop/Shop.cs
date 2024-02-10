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
        [SerializeField] private TextMeshProUGUI buyPriceText;
        [SerializeField] private TextMeshProUGUI sellPriceText;
        
        [SerializeField] private UIFadeCanvasGroupTween canvasGroup;
        
        [SerializeField] private UIShopItem shopItemPrefab;
        [SerializeField] private Transform shopItemContainer;

        [SerializeField] internal ShopData shopData;

        internal Action<Item> OnBuyItemHighlighted;
        
        private List<UIShopItem> shopItemsEntries = new();
        
        private List<Item> listedItems = new ();

        private void Start()
        {
            listedItems = shopData.ListedItems.OrderBy(x => x.DisplayName).ToList();
            if(listedItems.Count > 0) UpdateStore(listedItems);
        }

        public void Show(bool show)
        {
            canvasGroup.Show(show);
        }

        public void UpdateStore(List<Item> items)
        {
            if(items == null || items.Count <= 0) return;

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var shopItem = Instantiate(shopItemPrefab, shopItemContainer);
                shopItem.Setup(item, this, false);
                shopItemsEntries.Add(shopItem);
            }
            StartCoroutine(HighlightFirstItem());
        }
        
        public void HandleHighlight(UIShopItem shopItem)
        {
            foreach (var item in shopItemsEntries)
            {
                item.Highlight(false,true);
            }
            shopItem.Highlight(true,true);
            
            buyPriceText.text = shopItem.item.BuyPrice.ToString();
            sellPriceText.text = shopItem.item.SellPrice.ToString();
            
            OnBuyItemHighlighted?.Invoke(shopItem.item);
        }
        
        IEnumerator HighlightFirstItem()
        {
            yield return new WaitForSeconds(0.1f);
            shopItemsEntries[0].OnClick();
        }
    }
}