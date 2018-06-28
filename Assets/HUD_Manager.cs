﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mangos
{
    public class HUD_Manager : MonoBehaviour
    {
        public RawImage UI_Blink1;
        public RawImage UI_Blink2;
        public RawImage UI_Blink3;
        public Text UI_Vida;
        public Text UI_Ammo;
        public Text UI_Enemie;

        public int deadEnemies;

        public GameObject Player;
        public playerMovement_SideScroller3D playerScript;

        void Awake()
        {
            StaticManager.HUD_Script = this;
        }

        // Use this for initialization
        void Start()
        {
            deadEnemies = 0;
            UI_Blink1.enabled = true;
            UI_Blink2.enabled = true;
            UI_Blink3.enabled = true;
            playerScript = Player.GetComponent<playerMovement_SideScroller3D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            UI_Vida.text = playerScript.life.ToString();
            UI_Enemie.text = deadEnemies.ToString();
            UI_Ammo.text = StaticManager.weaponManager.myWeapons[StaticManager.weaponManager.currentWeaponId].ammo.ToString();

            if (playerScript.blinks == 0)
            {
                UI_Blink1.enabled = false;
                UI_Blink2.enabled = false;
                UI_Blink3.enabled = false;
            }

            if (playerScript.blinks == 1)
            {
                UI_Blink1.enabled = true;
                UI_Blink2.enabled = false;
                UI_Blink3.enabled = false;
            }

            if (playerScript.blinks == 2)
            {
                UI_Blink1.enabled = true;
                UI_Blink2.enabled = true;
                UI_Blink3.enabled = false;
            }

            if (playerScript.blinks == 3)
            {
                UI_Blink1.enabled = true;
                UI_Blink2.enabled = true;
                UI_Blink3.enabled = true;
            }

        }
    }
}
