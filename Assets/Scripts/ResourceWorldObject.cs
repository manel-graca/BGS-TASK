using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public class ResourceWorldObject : MonoBehaviour
    {
        [SerializeField] protected Resource resource;
        public Resource Resource => resource;
    }
}
