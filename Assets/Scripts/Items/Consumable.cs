using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "BGS/Consumable")]

    public class Consumable : Item
    {
        [SerializeField] private EConsumableType consumableType;
        public EConsumableType ConsumableType => consumableType;
        
        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }

}
