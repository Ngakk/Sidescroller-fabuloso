using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class CameraMovement : MonoBehaviour
    {
        public GameObject player;
        Limit[] limits;
        Vector3 desiredPos;
        float zOffset;

        void Start()
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Limit");
            limits = new Limit[temp.Length];
            for (int i = 0; i < temp.Length; i++)
                limits[i] = temp[i].GetComponent<Limit>();


        }

        private void Update()
        {
            Move();
        }
        void Move()
        {

        }
    }
}
