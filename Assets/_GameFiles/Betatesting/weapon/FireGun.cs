using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class FireGun : Weapon
    {
        public override void OnActionDown()
        {
            base.OnActionDown();
            Debug.Log("FG action down");    
        }
    }
}
