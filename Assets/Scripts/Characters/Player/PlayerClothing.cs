using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public class PlayerClothing : Player
    {
        [Serializable]
        public class PlayerClothingData
        {
            [SerializeField] internal EBodyPart bodyPart;
            [SerializeField] internal Clothing clothing;
            [SerializeField] internal SpriteRenderer renderer;
            [SerializeField] internal Sprite defaultSprite;
        }
        
        [SerializeField] private List<PlayerClothingData> playerClothingData = new();

        private void OnValidate()
        {
            foreach (var k in playerClothingData)
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
            foreach(var k in playerClothingData)
            {
                k.renderer.sprite = k.clothing == null ? k.defaultSprite : k.clothing.ClothingSprite;
            }
        }
    }
}

