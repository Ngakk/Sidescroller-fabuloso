using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos {
    public class IKController : MonoBehaviour {

        Animator anim;
        public Transform trans;
        // Use this for initialization
        void Start() {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {

        }

        private void OnAnimatorIK(int layerIndex)
        {
            anim.SetIKPosition(AvatarIKGoal.RightHand, trans.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);

            anim.SetIKPosition(AvatarIKGoal.LeftHand, trans.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);

            anim.SetLookAtPosition(trans.position);
            anim.SetLookAtWeight(1, 0.2f, 0.2f, 1, 1);
        }
    }
}