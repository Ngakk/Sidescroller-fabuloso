using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class AssaultRifle : FireGun
    {
        public override void OnActionDown()
        {
            base.OnActionDown();
            Debug.Log("AR action down");
        }

        public override void PreSpawnSpawnables()
        {
            base.PreSpawnSpawnables();
        }

        public override void OnActionHold()
        {
            base.OnActionHold();
            InaccurateShoot();
        }
    }
}
