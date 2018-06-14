using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class WeaponManager : MonoBehaviour
    {
        public List<Weapon> myWeapons;
        int currentWeaponId;
        // Use this for initialization
        void Start()
        {
            currentWeaponId = 0;
            //PreSpawning bullets
            for(int i = 0; i < myWeapons.Count; i++)
            {
                myWeapons[i].PreSpawnSpawnables();
            }
        }

        public void NextWeapon()
        {
            currentWeaponId++;
        }

        public void PreviousWeapon()
        {
            currentWeaponId--;
        }

        public void ShootDown()
        {
            myWeapons[currentWeaponId].OnActionDown();
        }

        public void ShootHold()
        {
            myWeapons[currentWeaponId].OnActionHold();
        }

        //TESTing
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                PreviousWeapon();
            if (Input.GetKeyDown(KeyCode.E))
                NextWeapon();

            if (Input.GetMouseButtonDown(0))
                ShootDown();
            if (Input.GetMouseButton(0))
                ShootHold();

        }

        public void DeleteWeapon(Weapon wpn)
        {
            
        }
    }
}
