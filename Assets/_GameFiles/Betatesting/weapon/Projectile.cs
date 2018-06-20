using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        public float lifeTime;

        public virtual void SelfDespawn(){
            PoolManager.Despawn(gameObject);
        }

        public virtual void OnDespawn(){
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        public virtual void OnSpawn(){
            Invoke("SelfDespawn", lifeTime);
        }

        void OnCollisionEnter(Collision _col)
        {
            myCollision();
        }

        public virtual void myCollision()
        {
            SelfDespawn();
        }
    }
}
