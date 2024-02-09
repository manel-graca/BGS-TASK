using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    [CreateAssetMenu(fileName = "New Tool", menuName = "BGS/Tool")]
    public class Tool : Item
    {
        [SerializeField] private EToolType toolType;
        public EToolType ToolType => toolType;
        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }

}
