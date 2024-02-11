using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGS.Task
{
    public class UIInventorySlot : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected TextMeshProUGUI itemAmountText;
        [SerializeField] protected TextMeshProUGUI hotbarNumberText;
        [SerializeField] protected Button discardButton;
        [SerializeField] protected Image icon;
        
        [SerializeField] protected bool isHotbar; public bool IsHotbar => isHotbar;
        
        protected Item item; public Item Item => item;
        protected bool isEmpty => item == null; public bool IsEmpty => isEmpty;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            isHotbar = transform.name.Contains("hotbar");
            itemAmountText.gameObject.SetActive(!isHotbar && item != null);
        }
#endif

        public virtual void Use()
        {
            if(item == null) return;
            
        }
      
        public virtual void Show(bool show, float duration = 0.1f, float delay = 0f)
        {
            if(isHotbar) return;
            
            canvasGroup.transform.DOScale(show ? Vector3.one : Vector3.zero, duration).SetDelay(delay);
            canvasGroup.DOFade(show ? 1 : 0, duration).SetDelay(delay);
            canvasGroup.interactable = show;
            canvasGroup.blocksRaycasts = show;
        }
        
        public virtual void AssignItem(Item item, int amount)
        {
            this.item = item;
            icon.sprite = item.Icon;
            icon.gameObject.SetActive(true);
            itemAmountText.text = amount.ToString();
            itemAmountText.gameObject.SetActive(!isHotbar);
            if(discardButton != null) discardButton.gameObject.SetActive(true);
        }
        
        public void InitializeHotbarSlot(int hotbarNumber)
        {
            Clear();
            hotbarNumberText.text = hotbarNumber.ToString();
        }

        public virtual void Clear()
        {
            icon.gameObject.SetActive(false);
            itemAmountText.gameObject.SetActive(false);
            if(discardButton != null) discardButton.gameObject.SetActive(false);
        }
    }
}
