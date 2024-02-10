using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    [CreateAssetMenu(fileName = "New Shop", menuName = "BGS/Shop/Shop Data")]
    public class ShopData : ScriptableObject
    {
        [SerializeField] protected string id; 
        public string ID => id;
    
        [SerializeField] protected string shopName; 
        public string ShopName => shopName;
        
        [SerializeField][TextArea(2,4)] protected string shopWelcomeMessage; 
        public string ShopWelcomeMessage => shopWelcomeMessage;
        
        [SerializeField] protected EShopType type;
        public EShopType Type => type;
        
        [SerializeField] private List<Item> listedItems = new();
        public List<Item> ListedItems => listedItems;

        [SerializeField] protected Shop shopPrefab;
        public Shop ShopPrefab => shopPrefab;
        
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString();
            }
        }

        private void OnEnable()
        {
            
        }
    }
}
