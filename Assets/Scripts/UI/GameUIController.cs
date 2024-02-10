using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        
        
        private List<Shop> openShops = new ();

        private void Start()
        {
            openShops = FindObjectsOfType<Shop>().ToList();
        }


        public void OpenShop(ShopData shop)
        {
            var shopToOpen = openShops.FirstOrDefault(s => s.shopData.ID == shop.ID);
            if (shopToOpen != null)
            {
                shopToOpen.Show(true);
            }
        }
        
        public void CloseShop(ShopData shop)
        {
            var shopToOpen = openShops.FirstOrDefault(s => s.shopData.ID == shop.ID);
            if (shopToOpen != null)
            {
                shopToOpen.Show(false);
            }
        }
    }
}
