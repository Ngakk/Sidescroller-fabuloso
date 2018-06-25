﻿using System.Collections;
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
                if (i != currentWeaponId)
                {
                    myWeapons[i].gameObject.SetActive(false);
                }
                else
                {
                    myWeapons[i].gameObject.SetActive(true);
                }
            }
        }

        public void NextWeapon()
        {
            currentWeaponId++;
            if (currentWeaponId >= myWeapons.Count)
                currentWeaponId = 0;
            for (int i = 0; i < myWeapons.Count; i++)
            {
                if (i != currentWeaponId)
                {
                    myWeapons[i].gameObject.SetActive(false);
                }
                else
                {
                    myWeapons[i].gameObject.SetActive(true);
                }
            }
        }

        public void PreviousWeapon()
        {
            currentWeaponId--;
            if (currentWeaponId < 0)
                currentWeaponId = myWeapons.Count;
            for (int i = 0; i < myWeapons.Count; i++)
            {
                if (i != currentWeaponId)
                {
                    myWeapons[i].enabled = false;
                }
                else
                {
                    myWeapons[i].enabled = true;
                }
            }
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
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKey(KeyCode.JoystickButton4))
                PreviousWeapon();
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.JoystickButton5))
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