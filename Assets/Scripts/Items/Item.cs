using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BGS.Task
{
    public class Item : ScriptableObject
    {
        [SerializeField] protected string id; 
        public string ID => id;
    
        [SerializeField] protected string displayName; 
        public string DisplayName => displayName;
        
        [SerializeField] protected Enums.EItemType type;
        public Enums.EItemType Type => type;
        
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
        
    
        protected virtual void OnEnable()
        {
            
        }
    }
}


