using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace BGS.Task
{
    [CreateAssetMenu(fileName = "New Outfit", menuName = "BGS/Clothing/Outfit")]
    public class Outfit : Item
    {
        [FormerlySerializedAs("clothes")][SerializeField] public List<OutfitDataEntry> Clothes = new();

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if(Clothes.Count >= 16) return;

            for (int i = 0; i < 16; i++)
            {
                var bp = (EBodyPart) i+1;
                var k = new OutfitDataEntry()
                {
                    bodyPart = bp,
                    clothing = null
                };
                Clothes.Add(k);
            }
        }
#endif

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        public void ChangePiece(Clothing clothing)
        {
            var k = Clothes.FirstOrDefault(x => x.bodyPart == clothing.ClothingBodyPart);
            if (k != null)
            {
                k.clothing = clothing;
            }
        }
        
    }
}

