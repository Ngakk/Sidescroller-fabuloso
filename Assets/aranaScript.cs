using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class aranaScript : MonoBehaviour
    {
        public int Elife = 15;
        private Animator Anim;
        private Rigidbody Rigi;
        private Vector3 moveDirection;
        public float enemieMaxSpeed;
        public float enemieCurrentAcceleration;
        private bool changeDir = false;
        private int dir = -1;
        public GameObject leftDetector;
        public GameObject rightDetector;
        public AudioSource AS;
        public AudioClip attack;
        public AudioClip dead;
        public AudioClip damaged;

        void Awake()
        {
            StaticManager.aranaScript = this;
        }

        // Use this for initialization
        void Start()
        {
            Rigi = GetComponent<Rigidbody>();
            Anim = GetComponentInChildren<Animator>();
            Anim.SetTrigger("walk");
            AS = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            Rigi.velocity += new Vector3(enemieCurrentAcceleration * dir * Time.deltaTime, 0f, 0f);
            if (Rigi.velocity.x >= enemieMaxSpeed)
            {
                Rigi.velocity = new Vector3(enemieMaxSpeed, Rigi.velocity.y, Rigi.velocity.z);
            }
            else if (Rigi.velocity.x <= -enemieMaxSpeed)
            {
                Rigi.velocity = new Vector3(-enemieMaxSpeed, Rigi.velocity.y, Rigi.velocity.z);
            }

            if (changeDir)
            {
                Quaternion rot = Quaternion.Euler(0, 90, 0);
                gameObject.transform.rotation = rot;
            }
            else
            {
                Quaternion rot = Quaternion.Euler(0, -90, 0);
                gameObject.transform.rotation = rot;
            }
        }

        void OnTriggerEnter(Collider _col)
        {
            if (Elife > 0)
            {
                if (_col.gameObject.CompareTag("limit"))
                {
                    dir = dir * -1;
                    changeDir = !changeDir;
                }

                if (_col.gameObject.CompareTag("bullet"))
                {
                    AS.PlayOneShot(damaged);
                    Elife = Elife - 5;
                }

                if (_col.gameObject.CompareTag("bullet2"))
                {
                    AS.PlayOneShot(damaged);
                    Elife = Elife - 2;
                }
                if (Elife <= 0)
                {
                    Anim.SetTrigger("walk");
                    EnemieDead();
                }
            }
        }

        void OnCollisionEnter(Collision _col)
        {
            if (Elife > 0)
            {
                if (_col.gameObject.CompareTag("Player"))
                {
                    AS.PlayOneShot(attack);
                    Anim.SetTrigger("atack");
                    StaticManager.playerScript.life = StaticManager.playerScript.life - 10;
                }
            }
        }

        public void EnemieDead()
        {
            dir = 0;
            StaticManager.HUD_Script.deadEnemies++;
            StartCoroutine(deadArana());
        }

        public IEnumerator deadArana()
        {
            AS.PlayOneShot(dead);
            Anim.SetTrigger("die");
            yield return new WaitForSeconds(3f);
        }
    }
}
