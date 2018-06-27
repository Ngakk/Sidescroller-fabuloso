using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos{
	public class Pickup : MonoBehaviour {

		public enum PickupType{
			health,
			ammo,
			AR
		}

		bool pickedUp;
		public float pickUpTime;
		float touchedTime;
		public PickupType type;
		GameObject toucher;
		string message;
		// Use this for initialization
		void Start () {
			switch (type) {
			case PickupType.health:
				message = "PickupHealth";
				break;
			case PickupType.ammo:
				message = "PickupAmmo";
				break;
			case PickupType.AR:
				message = "PickupAR";
				break;
			default:
				break;
			}
		}
		
		// Update is called once per frame
		void Update () {
			if (pickedUp) {
				Move ();
				Shrink ();
			}
		}
		
		void Move(){
			transform.Translate((toucher.transform.position - transform.position)*(6*(Time.time - touchedTime)/(pickUpTime))*Time.deltaTime);
		}

		void Shrink(){
			transform.localScale -= transform.localScale*Time.deltaTime/(3*pickUpTime);
		}

		void getPickedUp(){
			toucher.SendMessage (message, SendMessageOptions.DontRequireReceiver);
			gameObject.SetActive (false);//Place holder effect
		}

		void OnTriggerEnter(Collider _col){
			if (_col.gameObject.CompareTag ("Player")) {
				toucher = _col.gameObject;
				if (!pickedUp)
					touchedTime = Time.time;
				pickedUp = true;
			}	
		}

		void OnTriggerStay(Collider _col){
			if (_col.gameObject.CompareTag ("Player")) {
				if (Time.time > touchedTime + pickUpTime)
					getPickedUp ();
			}
		}
	}
}