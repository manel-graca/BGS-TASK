using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public class Player : Character
    {
        [SerializeField] protected PlayerRuntimeData runtimeData;
        public PlayerRuntimeData RuntimeData => runtimeData;
    }
}

