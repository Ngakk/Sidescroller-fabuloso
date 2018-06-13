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
        public virtual void OnAction() { }

        public virtual void OnActionUp() { }

        public virtual void OnReload() { }


        //TODO restar balas, magazines
    }
}
