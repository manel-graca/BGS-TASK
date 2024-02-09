using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    [CreateAssetMenu(fileName = "New Clothing", menuName = "BGS/Clothing")]
    public class Clothing : Item
    {
        [SerializeField] private EBodyPart clothingBodyPart;
        public EBodyPart ClothingBodyPart => clothingBodyPart;
        
        
        [SerializeField] private Sprite clothingSprite;
        public Sprite ClothingSprite => clothingSprite;
        
        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }
}

