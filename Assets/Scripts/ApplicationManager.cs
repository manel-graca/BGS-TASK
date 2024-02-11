using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private int targetFPS = 143;
        private void Awake()
        {
            Application.targetFrameRate = targetFPS;
        }
    }
}
