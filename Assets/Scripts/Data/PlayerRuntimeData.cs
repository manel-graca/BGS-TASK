using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    [CreateAssetMenu(fileName = "Player Runtime Data", menuName = "BGS/Player Runtime Data")]

    public class PlayerRuntimeData : ScriptableObject
    {
        public float Stamina;
        
        public int Gold;
        
        public List<CharacterOutfitData> OutfitData = new();
        public Outfit CurrentOutfit;
        
        public Vector2 CurrentDirection;
        
        
        // This ensures all runtime data is reset when the game is started
        private void OnEnable()
        {
            ResetData();
        }

        private void ResetData()
        {
            Stamina = 0;
            Gold = 0;
            OutfitData.Clear();
            CurrentOutfit = null;
            CurrentDirection = Vector2.down;
        }
    }
}
