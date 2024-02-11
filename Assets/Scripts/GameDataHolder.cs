using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public class GameDataHolder : MonoBehaviour
    {
        [SerializeField] private List<Item> items = new();
        public List<Item> Items => items;
        
        public List<Clothing> Clothing => items.FindAll(x => x is Clothing).ConvertAll(x => (Clothing)x);
    }
}
