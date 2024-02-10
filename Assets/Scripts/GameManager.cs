using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance;

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
    }
}
