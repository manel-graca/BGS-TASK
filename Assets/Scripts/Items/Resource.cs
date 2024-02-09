using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "BGS/Resource")]
    public class Resource : Item
    {
        [SerializeField] private EResourceType resourceType;
        public EResourceType ResourceType => resourceType;
        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }
}

