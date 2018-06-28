using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos {
    public class IKController : MonoBehaviour {

        Animator anim;
        public Transform trans;
		Rigidbody rigi;
		bool becameWind;
		public float maxVel;
        // Use this for initialization
        void Start() {
			rigi = GetComponentInParent<Rigidbody> ();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {
			anim.SetFloat ("Blend", Mathf.Abs(rigi.velocity.x)/maxVel);
			if(base && 
        }

        private void OnAnimatorIK(int layerIndex)
        {
            anim.SetIKPosition(AvatarIKGoal.RightHand, trans.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.75f);

            anim.SetIKPosition(AvatarIKGoal.LeftHand, trans.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.75f);

            anim.SetLookAtPosition(trans.position);
            anim.SetLookAtWeight(1, 0.2f, 0.2f, 1, 0.5f);

        }

		public void Jump(){
			anim.SetTrigger ("Jump");
		}

		public void DettachFromEarthlyThethers(bool b){
			becameWind = b;
		}
    }
}