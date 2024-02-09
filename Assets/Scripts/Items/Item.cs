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
        
        [SerializeField] protected EItemType type;
        public EItemType Type => type;
        
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;

        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString();
            }
        }
#endif

        protected virtual void OnEnable()
        {
            
        }
    }
}


