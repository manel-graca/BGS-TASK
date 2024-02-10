using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    [Serializable]
    public class CharacterOutfitData
    {
        [SerializeField] internal EBodyPart bodyPart;
        [SerializeField] internal Clothing clothing;
        [SerializeField] internal SpriteRenderer renderer;
        [SerializeField] internal Sprite defaultSprite;
    }

}
