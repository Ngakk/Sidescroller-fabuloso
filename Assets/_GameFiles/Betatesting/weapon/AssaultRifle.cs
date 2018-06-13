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
    }
}
