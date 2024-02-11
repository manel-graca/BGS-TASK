using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BGS.Task
{
    public class Item : ScriptableObject
    {
        [SerializeField] protected string id; 
        public string ID => id;
    
        [SerializeField] protected string displayName; 
        public string DisplayName => displayName;
        
        [SerializeField][TextArea(2,3)] protected string description; 
        public string Description => description;
        
        [SerializeField] protected EItemType type;
        public EItemType Type => type;
        
        public string TypeString => type.ToString().Replace("_", " ");
        
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
        
        [SerializeField] private bool tradable = true;
        public bool Tradable => tradable;
        
        [SerializeField] private float baseValue;
        
        [SerializeField] private bool equippableInHotbar;
        public bool EquippableInHotbar => equippableInHotbar;
        
        [SerializeField][Range(0.1f,5)] private float sellRatio = 0.75f;
        [SerializeField][Range(0.1f,5)] private float demandFactor = 1f;
        [SerializeField][Range(0.1f,5)] private float supplyFactor = 1f;
        [SerializeField][Range(0.1f,5)] private float utilityFactor= 1f;

        
        [SerializeField] private int buyPrice;
        public int BuyPrice => buyPrice;
        
        [SerializeField] private int sellPrice;
        public int SellPrice => sellPrice;

        
#if UNITY_EDITOR
        
        protected virtual void OnValidate()
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString();
            }
            
            buyPrice = Mathf.RoundToInt(baseValue * demandFactor * utilityFactor / supplyFactor);
            sellPrice = Mathf.RoundToInt(buyPrice * sellRatio);
            sellPrice = Mathf.Max(1, sellPrice);
            buyPrice = Mathf.Max(1, buyPrice);
        }
#endif

        protected virtual void OnEnable()
        {
            
        }
    }
}


