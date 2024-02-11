using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BGS.Task
{
    public class Wardrobe : MonoBehaviour
    {
        [SerializeField] private UIClothingOption clothingOption;
        [SerializeField] private UIFadeCanvasGroupTween fadeCanvasGroupTween;
        [SerializeField] private RectTransform leftParent, rightParent;
        [SerializeField] private int optionsPerParent = 6;
        [SerializeField] private TextMeshProUGUI totalValueText;
        [SerializeField] private List<WardrobePlayerData> playerWardrobeData = new();
        
        private Outfit currentOutfit => GameManager.Instance.Player.GetComponent<Player>().RuntimeData.CurrentOutfit;

        [Serializable]
        internal class WardrobePlayerData
        {
            [SerializeField] internal SpriteRenderer renderer;
            [SerializeField] internal EBodyPart bodyPart;
        }
        
        private List<UIClothingOption> uiClothingOptions = new();
        
        public Action<bool> OnOpenClose;
        
        private bool showing;
        
        private List<Clothing> initialClothesWhenOpen = new();
        private int totalValue;


        private void Start()
        {
            var allClothes = FindObjectOfType<GameDataHolder>().Clothing.ToList();

            var bodyParts = Enum.GetValues(typeof(EBodyPart)).Cast<EBodyPart>().ToList();
            var c = 0;
            foreach (var bodyPart in bodyParts)
            {
                if(bodyPart == EBodyPart.None || bodyPart == EBodyPart.Left_Hand || bodyPart == EBodyPart.Right_Hand) continue;
                
                c++;
                var clothes = allClothes.FindAll(x => x.ClothingBodyPart == bodyPart);
                
                var uiClothingOption = Instantiate(clothingOption, c <= optionsPerParent ? leftParent : rightParent);
                uiClothingOptions.Add(uiClothingOption);
                uiClothingOption.Setup(clothes, this);
            }
            
            UpdateRenderers();   
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                showing = !showing;
                if (showing)
                {
                    initialClothesWhenOpen = new List<Clothing>();
                    foreach(var k in currentOutfit.Clothes.Where(x => x.clothing != null)) initialClothesWhenOpen.Add(k.clothing);
                }
                fadeCanvasGroupTween.Show(showing);
                OnOpenClose?.Invoke(showing);
                foreach (var j in uiClothingOptions)
                {
                    j.ChangePiece(initialClothesWhenOpen.FirstOrDefault(x => x.ClothingBodyPart == j.currentClothing.ClothingBodyPart));
                }
                UpdateTotalValue();
                if (!showing)
                {
                    // reset the outfit using the 'initialClothesWhenOpen'
                    currentOutfit.Clothes = new List<OutfitDataEntry>();
                    foreach (var k in initialClothesWhenOpen)
                    {
                        currentOutfit.Clothes.Add(new OutfitDataEntry()
                        {
                            bodyPart = k.ClothingBodyPart,
                            clothing = k
                        });
                    }
                    // now set that to the player outfit otherwise it will not update
                    GameManager.Instance.Player.GetComponent<Player>().RuntimeData.CurrentOutfit = currentOutfit;
                    
                }
            }
            UpdateRenderers();
        }
        
        private void UpdateRenderers()
        {
            if (currentOutfit != null)
            {
                foreach (var k in currentOutfit.Clothes)
                {
                    foreach (var j in playerWardrobeData.Where(j => k.bodyPart == j.bodyPart && k.clothing != null))
                    {
                        j.renderer.sprite = k.clothing.ClothingSprite;
                    }
                }
            }
        }

        public void Buy()
        {
            var newOutfit = ScriptableObject.CreateInstance<Outfit>();
            newOutfit.name = "New Outfit";
            newOutfit.Clothes = new List<OutfitDataEntry>();
            foreach (var k in uiClothingOptions)
            {
                newOutfit.Clothes.Add(new OutfitDataEntry()
                {
                    bodyPart = k.currentClothing.ClothingBodyPart,
                    clothing = k.currentClothing
                });
            }
            GameManager.Instance.Player.GetComponent<Player>().RuntimeData.CurrentOutfit = newOutfit;
            showing = !showing;
            fadeCanvasGroupTween.Show(showing);
            OnOpenClose?.Invoke(showing);
            
        }

        public void UpdatePiece(Clothing clothing)
        {
            var k = currentOutfit.Clothes.FirstOrDefault(x => x.bodyPart == clothing.ClothingBodyPart);
            k.clothing = clothing;

            var j = playerWardrobeData.FirstOrDefault(x => x.bodyPart == clothing.ClothingBodyPart);
            j.renderer.sprite = clothing.ClothingSprite;

            foreach (var h in currentOutfit.Clothes)
            {
                var g = uiClothingOptions.FirstOrDefault(x => x.currentClothing.ClothingBodyPart == h.bodyPart);
                if(g == null) continue;
                g.ChangePiece(h.clothing);
            }

            UpdateTotalValue();
        }

        private void UpdateTotalValue()
        {
            totalValue = 0;
            foreach (var x in currentOutfit.Clothes)
            {
                if (x.clothing == null)
                {
                    continue;
                }

                if (!initialClothesWhenOpen.Contains(x.clothing))
                {
                    totalValue += x.clothing.BuyPrice;
                }
            }

            totalValue = Mathf.Clamp(totalValue, 0, 999999);
            totalValueText.text = $"Total: {totalValue.ToString()} Gold";
        }
    }
}
