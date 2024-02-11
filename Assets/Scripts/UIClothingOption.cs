using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BGS.Task
{
    public class UIClothingOption : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI clothingName;
        
        internal Clothing currentClothing;
        
        private List<Clothing> clothes = new();
        private int currentIndex;
        private Wardrobe wardrobe;
        
        public void Setup(List<Clothing> clothingList, Wardrobe wardrobe)
        {
            if(clothingList.Count <= 0) return;
            this.wardrobe = wardrobe;
            clothes = clothingList;
            currentIndex = 0;
            currentClothing = clothes[currentIndex];
            clothingName.text = currentClothing.name;
        }
        
        public void ChangePiece(Clothing clothing)
        {
            if(clothing == null) return;
            currentIndex = clothes.IndexOf(clothing);
            currentClothing = clothing;
            clothingName.text = currentClothing.name;
        }
        
        
        public void NextClothing()
        {
            currentIndex++;
            if (currentIndex >= clothes.Count)
            {
                currentIndex = 0;
            }
            currentClothing = clothes[currentIndex];
            clothingName.text = currentClothing.name;
            wardrobe.UpdatePiece(currentClothing);
        }
        
        public void PreviousClothing()
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = clothes.Count - 1;
            }
            currentClothing = clothes[currentIndex];
            clothingName.text = currentClothing.name;
            wardrobe.UpdatePiece(currentClothing);
        }
        
        
        
        
    }
}
