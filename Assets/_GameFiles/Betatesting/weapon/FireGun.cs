using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class FireGun : Weapon
    {
        public GameObject bullet;
        public float shootingForce;
        public float fireRate;
        public float inaccuracyAngleLow, inaccuracyAngleHigh;
        float lastShootTime;
        public GameObject MuzzleFlash;

        public override void PreSpawnSpawnables()
        {
            base.PreSpawnSpawnables();
            PoolManager.PreSpawn(bullet, 20, false);
            PoolManager.SetPoolLimit(bullet, 40);
        }

        public override void OnActionDown()
        {
            base.OnActionDown();
            Shoot(); 
        }

        public override void OnActionHold()
        {
            base.OnActionHold();
        }

        public bool DepleteBullet()
        {
            if (ammo > 0)
                ammo--;
            if (ammo == 0)
                return false;
            else return true;
        }

        public void Shoot()
        {
            if (Time.time > lastShootTime + fireRate)
            {
                StaticManager.audioManager.PlayBasicShot(StaticManager.playerScript.gameObject.transform.position);
                CancelInvoke("stopMuzzleParticle");
                MuzzleFlash.SetActive(true);
                MuzzleFlash.transform.position = spawnPoint.transform.position;
                MuzzleFlash.GetComponent<ParticleSystem>().Play();
                lastShootTime = Time.time;
                Transform go = PoolManager.Spawn(bullet, spawnPoint.transform.position, Quaternion.identity);
                go.gameObject.GetComponent<Rigidbody>().AddForce(base.getShootDir() * shootingForce, ForceMode.Impulse);
                Invoke("stopMuzzleParticle", MuzzleFlash.GetComponent<ParticleSystem>().main.duration);
            }
        }

        public void stopMuzzleParticle()
        {
            MuzzleFlash.SetActive(false);
            MuzzleFlash.GetComponent<ParticleSystem>().Stop();
        }

        public void InaccurateShoot()
        {
            if (Time.time > lastShootTime + fireRate)
            {
                CancelInvoke("stopMuzzleParticle");
                MuzzleFlash.SetActive(true);
                MuzzleFlash.transform.position = spawnPoint.transform.position;
                MuzzleFlash.GetComponent<ParticleSystem>().Play();
                lastShootTime = Time.time;
                StaticManager.audioManager.PlayMetralleta(StaticManager.playerScript.gameObject.transform.position);
                float inaccuracy = inaccuracyAngleLow + (inaccuracyAngleHigh - inaccuracyAngleLow) * Mathf.PerlinNoise(Time.time, 0.0f);
                Vector3 offsetAngle = new Vector3(Mathf.Cos(inaccuracy * Mathf.Deg2Rad), Mathf.Sin(inaccuracy * Mathf.Deg2Rad));
                Vector3 dir = Quaternion.Euler(0, 0, inaccuracy) * base.getShootDir();

                Transform go = PoolManager.Spawn(bullet, spawnPoint.transform.position, Quaternion.identity);
                go.gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * shootingForce, ForceMode.Impulse);
                Invoke("stopMuzzleParticle", MuzzleFlash.GetComponent<ParticleSystem>().main.duration);
            }
            
        }
    }
}
