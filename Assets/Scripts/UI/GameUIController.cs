using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace BGS.Task
{
    public class GameUIController : MonoBehaviour
    {
        private static GameUIController instance;
        public static GameUIController Instance => instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        [SerializeField] private GameObject popupText;
        [SerializeField] private RectTransform popupTextParent;
        
        public Action<bool> OnStoreOpenClose;
        
        private List<GameObject> popupTexts = new();
        private const int maxPopupTextsAlive = 6;
        private const int popupTextLifetime = 4;
        
        private List<Shop> openShops = new ();

        private void Start()
        {
            openShops = FindObjectsOfType<Shop>().ToList();
        }

        public void ShowPopupText(string message, Color color)
        {
            var text = Instantiate(popupText, popupTextParent);
            var tmpro = text.GetComponentInChildren<TextMeshProUGUI>();
            tmpro.text = message;
            tmpro.color = color;
            tmpro.DOFade(0,1).SetDelay(popupTextLifetime).OnComplete(() =>
            {
                popupTexts.Remove(text);
                Destroy(text.gameObject, 0.1f);
            });
            
            popupTexts.Add(text);
            if (popupTexts.Count > maxPopupTextsAlive)
            {
                var k = popupTexts[0];
                k.GetComponentInChildren<TextMeshProUGUI>().DOFade(0,0.1f).OnComplete(() =>
                {
                    Destroy(k, 0.1f);
                });
                popupTexts.RemoveAt(0);
            }
        }

        public void OpenShop(ShopData shop)
        {
            var shopToOpen = openShops.FirstOrDefault(s => s.shopData.ID == shop.ID);
            if (shopToOpen != null)
            {
                shopToOpen.Show(true);
            }
            OnStoreOpenClose?.Invoke(true);
        }
        
        public void CloseShop(ShopData shop)
        {
            var shopToOpen = openShops.FirstOrDefault(s => s.shopData.ID == shop.ID);
            if (shopToOpen != null)
            {
                shopToOpen.Show(false);
            }
            OnStoreOpenClose?.Invoke(false);
        }
    }
}
