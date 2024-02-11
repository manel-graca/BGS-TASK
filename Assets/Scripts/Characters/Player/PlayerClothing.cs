using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace BGS.Task
{
    public class PlayerClothing : Player
    {
        [FormerlySerializedAs("currentOutfitData")] 
        [SerializeField] private List<CharacterOutfitData> currentWardrobeData = new();

        private Outfit currentOutfit => RuntimeData.CurrentOutfit;
        

        protected override void Start()
        {
            base.Start();
            if(currentOutfit != null) ChangeOutfit(currentOutfit);
        }
        
        protected override void Update()
        {
            base.Update();
            UpdateRenderers();
        }

        public void ChangeOutfit(Outfit outfit)
        {
            RuntimeData.CurrentOutfit = outfit;
            UpdateRenderers();
        }

        private void UpdateRenderers()
        {
            if (currentOutfit != null)
            {
                foreach (var k in currentOutfit.Clothes)
                {
                    foreach (var j in currentWardrobeData.Where(j => k.bodyPart == j.bodyPart && k.clothing != null))
                    {
                        j.clothing = k.clothing;
                        j.defaultSprite = k.clothing.ClothingSprite;
                        j.renderer.sprite = k.clothing.ClothingSprite;
                    }
                }
            }
        }
    }
}

