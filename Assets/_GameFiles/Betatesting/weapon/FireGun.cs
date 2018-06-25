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
            Shoot();
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
                lastShootTime = Time.time;
                Transform go = PoolManager.Spawn(bullet, spawnPoint.transform.position, Quaternion.identity);
                go.gameObject.GetComponent<Rigidbody>().AddForce(base.getShootDir() * shootingForce, ForceMode.Impulse);
            }
        }

        public void InaccurateShoot()
        {
            if (Time.time > lastShootTime + fireRate)
            {
                lastShootTime = Time.time;

                float inaccuracy = inaccuracyAngleLow + (inaccuracyAngleHigh - inaccuracyAngleLow) * Mathf.PerlinNoise(Time.time, 0.0f);
                Vector3 offsetAngle = new Vector3(Mathf.Cos(inaccuracy * Mathf.Deg2Rad), Mathf.Sin(inaccuracy * Mathf.Deg2Rad));
                Vector3 dir = Quaternion.Euler(0, 0, inaccuracy) * base.getShootDir();

                Transform go = PoolManager.Spawn(bullet, spawnPoint.transform.position, Quaternion.identity);
                go.gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * shootingForce, ForceMode.Impulse);
            }
            
        }



    }
}
