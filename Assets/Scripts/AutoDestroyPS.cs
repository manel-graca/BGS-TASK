using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS.Task
{
    public class AutoDestroyPS : MonoBehaviour
    {
        private ParticleSystem ps;

        void Start()
        {
            ps = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            if (ps && !ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
