using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public class PersistentObjects : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
