using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public class PlayerInteractions : Player
    {
        private GameObject currentInteractable;

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (currentInteractable != null)
                {
                    if (currentInteractable.TryGetComponent(out Tree tree))
                    {
                        tree.Hit();
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var interactable = other.GetComponent<IInteractable>() ?? other.GetComponentInParent<IInteractable>();
            if(interactable == null) return;
            currentInteractable = other.gameObject;
            Debug.Log($"<color=#BEFFC1>Trying to interact with {other.transform.name}</color>");
            
            if (interactable.CanInteract())
            {
                Debug.Log($"<color=#62FF60>Interacting with {other.transform.name}</color>");
                interactable.StartInteract();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var interactable = other.GetComponent<IInteractable>() ?? other.GetComponentInParent<IInteractable>();
            if(interactable == null) return;

            Debug.Log($"<color=#6AFFEB>Stopping interacting with {other.transform.name}</color>");
            interactable.StopInteract();
        }
    }
}