using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance;
        
        [SerializeField] private Wardrobe wardrobe;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        [SerializeField] private Player player;
        public Player Player => player;


        private void Start()
        {
            wardrobe.OnOpenClose += b => {player.gameObject.SetActive(!b);}; 
        }

        private void OnDestroy()
        {
            wardrobe.OnOpenClose -= b => {player.gameObject.SetActive(!b);}; 
        }
    }
}
