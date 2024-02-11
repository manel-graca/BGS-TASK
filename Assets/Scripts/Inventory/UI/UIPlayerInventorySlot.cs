using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGS.Task
{
    public class UIPlayerInventorySlot : UIInventorySlot
    {
        [SerializeField] private Image selectedImage;
        
        private PlayerInventory player;
        private bool selected; public bool Selected => selected;

        public void Discard()
        {
            player.Discard(item);
        }

        public override void AssignItem(Item item, int amount)
        {
            base.AssignItem(item,amount);
            
            player = GameManager.Instance.Player.GetComponent<PlayerInventory>();
        }

        public override void Use()
        {
            base.Use();
            if (item is Outfit outfit)
            {
                player.GetComponent<PlayerClothing>().ChangeOutfit(outfit);
            }
        }

        public void Select(bool select)
        {
            selected = select;
            selectedImage.DOFade(selected ? 0.15f : 0, 0.1f);
        }
    }
}
