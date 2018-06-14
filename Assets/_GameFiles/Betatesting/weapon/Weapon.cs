using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Weapon : MonoBehaviour
    {
        public Animator anim;
        public Transform spawnPoint;

        internal int ammo;

        public virtual void OnActionDown() {
            
        }
        public virtual void OnActionHold() { }

        public virtual void OnActionUp() { }

        public virtual void OnReload() { }

        public virtual void OnDestroy() {
            //Tirar el arma y quitarla del weapon manager
               
        }

        public virtual void PreSpawnSpawnables() { }

        public virtual Vector3 getShootDir() {
            return StaticManager.playerScipt.GetShootDir();
        }
        //TODO restar balas, magazines

        
    }
}
