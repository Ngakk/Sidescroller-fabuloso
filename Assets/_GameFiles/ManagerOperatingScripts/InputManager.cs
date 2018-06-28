using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos {
	public class InputManager : MonoBehaviour
    {
        private float shootAxis;
        private bool shootHold;

        void Awake(){
			StaticManager.inputManager = this;
		}
			
		void Update(){
			switch (StaticManager.gameManager.gameState) {
			case GameState.mainGame:
                    shootAxis = Input.GetAxis("ShootTrigger");

                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton4))
                        StaticManager.weaponManager.PreviousWeapon();
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5))
                        StaticManager.weaponManager.NextWeapon();

                    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton2))
                        StaticManager.weaponManager.ShootDown();
                    if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.JoystickButton2))
                        StaticManager.weaponManager.ShootHold();
                    if(shootAxis != 0)
                    {
                        if (!shootHold)
                        {
                            shootHold = true;
                            StaticManager.weaponManager.ShootDown();
                        }
                        else
                        {
                            StaticManager.weaponManager.ShootHold();
                        }
                    }
                    else
                    {
                        shootHold = false;
                    }
                    break;
                default:
				    break;
			}
		}
	}
}