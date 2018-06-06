using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class CameraMovement : MonoBehaviour
    {
        Limit[] limites;

        private void Start()
        {
            limites = FindObjectsOfType<Limit>();
        }

        private void Update()
        {
            CheckBounds();
        }

        void CheckBounds()
        {
            for (int i = 0; i < limites.Length; i++)
            {
                bool[] inOrOut = limites[i].checkBounds(transform.position.x, transform.position.y, 10, 10);
                if (inOrOut[(int)Dir.up])
                {

                }
                if (inOrOut[(int)Dir.right])
                {

                }
                if (inOrOut[(int)Dir.down])
                {

                }
                if (inOrOut[(int)Dir.left])
                {

                }
            }
        }        

        private void FixedUpdate()
        {
            
        }
    }
}