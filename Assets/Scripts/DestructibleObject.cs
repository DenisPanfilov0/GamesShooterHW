using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestructibleObject : MonoBehaviour

    {
        [SerializeField] private float hpInitial = 100;
        [SerializeField] public float hpCurrent = 100;

        internal void ReceiveDamage(float damage)
        {
            hpCurrent -= damage;
            // можно писать так hpCurrent = hpCurrent - 30f;

            if (hpCurrent <= 0f)
            {
                Destroy(gameObject);
            }
        }


    }
}
