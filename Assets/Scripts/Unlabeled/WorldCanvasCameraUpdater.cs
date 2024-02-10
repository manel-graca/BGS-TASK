using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public class WorldCanvasCameraUpdater : MonoBehaviour
    {
        private Canvas canvas;
        void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        void Update()
        {
            if (canvas.worldCamera == null)
            {
                var c = Camera.main;
                if(c != null) canvas.worldCamera = c;
            }
        }
    }
}
