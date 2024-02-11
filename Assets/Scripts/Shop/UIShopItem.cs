using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace BGS.Task
{
    public class UIShopItem : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [FormerlySerializedAs("tween")] [FormerlySerializedAs("highlightImage")] [SerializeField] private UIFadeImageTween imageTween;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemPriceText;
        
        private Shop shop;
        internal Item item;

        public void Highlight(bool value, bool immediate = false)
        {
            imageTween.Show(value, immediate);
        }

        public void Setup(Item item, Shop shop, bool highlight)
        {
            this.item = item;
            this.shop = shop;
            
            icon.sprite = item.Icon;
            itemNameText.text = item.DisplayName;
            itemPriceText.text = item.BuyPrice.ToString();
            
            Highlight(highlight);
        }
    }
}
