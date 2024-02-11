using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGS.Task
{
    public class UIShopItemEntry : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemPrice;
        [SerializeField] private TextMeshProUGUI itemAmount;
        [SerializeField] private Image icon;
        [SerializeField] private Image highlightImage;
        
        internal bool selected; public bool Selected => selected;
        internal int itemAmountToTransact;
        internal int index;
        internal bool isInRightSide;
        internal Item item;
        private Shop shop;

        public void Setup(Item item, Shop shop, int index, int price, bool rightSide)
        {
            this.item = item;
            this.shop = shop;
            this.index = index;
            isInRightSide = rightSide;
            itemPrice.text = price.ToString();
            icon.sprite = item.Icon;
        }

        public void SetAmountToTransact(int amount)
        {
            itemAmountToTransact = amount;
            itemAmount.text = itemAmountToTransact.ToString();
        }

        public void IncrementAmountToTransact()
        {
            itemAmountToTransact++;
            itemAmount.text = itemAmountToTransact.ToString();
        }
        
        public void DecrementAmountToTransact()
        {
            itemAmountToTransact--;
            itemAmount.text = itemAmountToTransact.ToString();
        }
        
        public void SetItemAmount(int amount)
        {
            itemAmount.text = amount.ToString();
        }

        public void Highlight(bool highlight)
        {
            selected = highlight;
            highlightImage.DOFade(highlight ? 0.3f : 0, 0.1f);
        }
        
        public void Select()
        {
            shop.HandleHighlight(this);
        }
    }
}
