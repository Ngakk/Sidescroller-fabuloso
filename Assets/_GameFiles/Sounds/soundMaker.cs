using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class soundMaker : MonoBehaviour
    {

        public AudioSource AS;

        void Start()
        {
            AS = GetComponent<AudioSource>();
        }

        public virtual void SelfDespawn()
        {
            PoolManager.Despawn(gameObject);
        }

        public virtual void OnDespawn()
        {
            AS.Stop();
        }

        public virtual void OnSpawn()
        {
            Invoke("SelfDespawn", AS.clip.length);
            AS.Play();
        }
    }
}
