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
    }
}
