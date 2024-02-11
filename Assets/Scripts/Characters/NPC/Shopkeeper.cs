using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BGS.Task
{
    public class Shopkeeper : Character, IInteractable
    {
        [SerializeField] private UIFadeCanvasGroupTween welcomeScreen;
        
        [SerializeField] private ShopData shop;
        
        private bool interacting;
        
        private Player player;

        protected override void Start()
        {
            base.Start();
            player = GameManager.Instance.Player;
        }

        public void OpenShop()
        {
            GameUIController.Instance.OpenShop(shop);
            CloseWelcomeScreen();
        }

        public void CloseShop()
        {
            GameUIController.Instance.CloseShop(shop);
        }

        public void CloseWelcomeScreen()
        {
            welcomeScreen.Show(false);
        }

        public bool CanInteract()
        {
            return true;
        }

        public void StartInteract()
        {
            if(interacting) return;
            
            interacting = true;
            welcomeScreen.Show(true);
        }

        public void StopInteract()
        {
            if(!interacting) return;
            
            interacting = false;
            CloseWelcomeScreen();
            CloseShop();
        }

    }
}

