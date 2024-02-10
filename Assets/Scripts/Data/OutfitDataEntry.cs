using System;
using System.Collections;
using System.Collections.Generic;
using BGS.Task;
using UnityEngine;

namespace BGS.Task
{
    [Serializable]
    public class OutfitDataEntry
    {
        [SerializeField] internal EBodyPart bodyPart;
        [SerializeField] internal Clothing clothing;
    }
}

