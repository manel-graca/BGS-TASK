using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public interface IInteractable
    {
        public bool CanInteract();
        public void StartInteract();
        public void StopInteract();
    }
}
