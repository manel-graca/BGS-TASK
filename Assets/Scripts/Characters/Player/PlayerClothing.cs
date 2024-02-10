using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public class PlayerClothing : Player
    {
        [SerializeField] private Outfit currentOutfit;
        
        [SerializeField] private List<CharacterOutfitData> currentOutfitData = new();

        private void OnValidate()
        {
            foreach (var k in currentOutfitData)
            {
                if (k.defaultSprite == null && k.renderer.sprite != null)
                {
                    k.defaultSprite = k.renderer.sprite;
                } 
            }
        }

        protected override void Start()
        {
            base.Start();
        }
        
        protected override void Update()
        {
            base.Update();
            UpdateRenderers();
        }

        private void UpdateRenderers()
        {
            foreach(var k in currentOutfitData)
            {
                k.renderer.sprite = k.clothing == null ? k.defaultSprite : k.clothing.ClothingSprite;
            }
        }
    }
}

