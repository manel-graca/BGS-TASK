using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    [CreateAssetMenu(fileName = "New Outfit", menuName = "BGS/Clothing/Outfit")]
    public class Outfit : Item
    {
        [SerializeField] private List<OutfitDataEntry> clothes = new();
        public List<OutfitDataEntry> Clothes => clothes;

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if(clothes.Count >= 16) return;

            for (int i = 0; i < 16; i++)
            {
                var bp = (EBodyPart) i+1;
                var k = new OutfitDataEntry()
                {
                    bodyPart = bp,
                    clothing = null
                };
                clothes.Add(k);
            }
        }
#endif

        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }
}

