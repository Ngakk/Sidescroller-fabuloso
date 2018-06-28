using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mangos
{
    public class playerMovement_SideScroller3D : MonoBehaviour
    {
        public Camera cam;
        public float maximumSpeed;
        [SerializeField]
        private float currentAcceleration;
        public float airAcceleration;
        public float floorAcceleration;
        [Range(1, 20)]
        public float jumpForce;
        public float fallMultiplier;
        public float lowJumpMultiplier;
        public GameObject EmptyBlink;
        public GameObject BlinkTarget;
        public GameObject ShotTarget;
        public bool floorBool;
        private Rigidbody rigi;
        private float m_Horizontal;
        private float m_Vertical;
        private float cam_Horizontal;
        private float cam_Vertical;
        public float blinkDistance;
        private bool m_Jump;
        private bool jumpReq;
        [Range(0, 1)]
        public float desacceleration;
        public bool wallCheck;
        public int jumpCount;
        public Vector3 playerSize;
        private Vector3 blinkPosition;
        private Vector3 targetPosition;
        public int blinks;
        private bool blinkAvailable;
        public bool facingRight;
        List<ContactPoint> objectsTouched = new List<ContactPoint>();
        public int life = 100;

        void Awake()
        {
            StaticManager.playerScript = this;
        }

        void Start()
        {
            rigi = GetComponent<Rigidbody>();
            playerSize = gameObject.GetComponent<MeshRenderer>().bounds.extents;
            blinkAvailable = true;
            blinks = 3;
        }

        void Update()
        {
            //DECLARACION DE VARIABLES
            m_Horizontal = Input.GetAxis("Horizontal");
            m_Vertical = Input.GetAxis("Vertical");
            cam_Horizontal = Input.GetAxis("Cam_Horizontal");
            cam_Vertical = Input.GetAxis("Cam_Vertical");

            //RAY CAST FOR BLINK
            if (Input.GetJoystickNames().Length > 0)
            {
                blinkPosition = new Vector3(m_Horizontal, m_Vertical, 0f).normalized * 3f;
                if (cam_Horizontal < 0.05 && cam_Vertical < 0.05 && cam_Horizontal > -0.05 && cam_Vertical > -0.05)
                {
                    if (m_Horizontal > 0.05 || m_Vertical > 0.05 || m_Horizontal < -0.05 || m_Vertical < -0.05)
                        targetPosition = blinkPosition;
                }
                else
                    targetPosition = new Vector3(cam_Horizontal, cam_Vertical, 0f).normalized * 3f;
            }
            else
            {
                blinkPosition = Vector3.Scale(Input.mousePosition - cam.WorldToScreenPoint(EmptyBlink.transform.position), new Vector3(1f, 1f, 0f)).normalized * 2;
                targetPosition = blinkPosition;
            }
            Ray blinkCheckRay = new Ray(EmptyBlink.transform.position, blinkPosition);
            Debug.DrawRay(EmptyBlink.transform.position, blinkPosition, Color.blue);
            Debug.DrawRay(EmptyBlink.transform.position, targetPosition);

            //RAYCAST HELPER
            BlinkTarget.transform.position = EmptyBlink.transform.position + blinkPosition;
            ShotTarget.transform.position = EmptyBlink.transform.position + targetPosition;
            gameObject.GetComponentInChildren<WeaponManager>().gameObject.transform.LookAt(ShotTarget.transform);



            if (targetPosition.x < 0)
            {
                facingRight = true;
            }
            else
            {
                facingRight = false;
            }

            if (facingRight)
            {
                Quaternion rot = Quaternion.Euler(0, 180, 0);
                gameObject.transform.rotation = rot;
            }
            else
            {
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                gameObject.transform.rotation = rot;
            }

            if (Input.GetButtonDown("Blink"))
            {
                Blink();
                blinks--;
                if (blinks <= 0)
                {
                    blinkAvailable = false;
                    StartCoroutine("BlinkAproveAvailability");
                }
            }

            //INPUT JUMP
            if (Input.GetButtonDown("Jump")/* && groundTouched.Count != 0*/)
            {
                jumpCount++;
                if (jumpCount <= 1)
                    jumpReq = true;
            }

            if(life <= 0)
            {
                SceneManager.LoadScene(3);
            }
        }

        void FixedUpdate()
        {
            /* SECCION DE SALTO */
            if (jumpReq)
            {
                if (jumpCount == 0)
                {
                    currentAcceleration = airAcceleration;
                    rigi.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    jumpReq = false;
                }
                else
                {
                    currentAcceleration = airAcceleration;
                    rigi.velocity = new Vector3(rigi.velocity.x, 0f, rigi.velocity.z);
                    rigi.AddForce(Vector3.up * jumpForce / 2, ForceMode.Impulse);
                    jumpReq = false;
                }
            }

            if (rigi.velocity.y < 0)
            {
                rigi.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rigi.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rigi.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

            /* SECCION DE MOVIMIENTO HORIZONTAL */

            if (m_Horizontal != 0)
            {
                rigi.velocity += new Vector3(currentAcceleration * m_Horizontal * Time.deltaTime, 0.0f, 0.0f);
                if (rigi.velocity.x >= maximumSpeed)
                {
                    rigi.velocity = new Vector3(maximumSpeed, rigi.velocity.y, rigi.velocity.z);
                }
                else if (rigi.velocity.x <= -maximumSpeed)
                {
                    rigi.velocity = new Vector3(-maximumSpeed, rigi.velocity.y, rigi.velocity.z);
                }
            }
            else
            {
                if (rigi.velocity.x != 0)
                {
                    rigi.velocity = new Vector3(rigi.velocity.x * desacceleration, rigi.velocity.y, rigi.velocity.z);
                }
            }
        }

        void OnCollisionStay(Collision _col)
        {
            List<ContactPoint> temp = new List<ContactPoint>();
            for (int i = 0; i < _col.contacts.Length; i++)
            {
                temp.Add(_col.contacts[i]);
            }
            objectsTouched = temp;

            if (_col.contacts[0].normal.y >= 0.9f)
            {
                jumpCount = 0;
                currentAcceleration = floorAcceleration;
                floorBool = true;
            }
            else if (_col.contacts[0].normal.x >= 0.9f || _col.contacts[0].normal.x <= -0.9f)
            {
                rigi.velocity = new Vector3(0f, rigi.velocity.y, rigi.velocity.z);
            }
        }

        void OnCollisionExit(Collision _col)
        {
            for (int i = 0; i < objectsTouched.Count; i++)
            {
                if (objectsTouched[i].normal.y >= 0.9f)
                {
                    currentAcceleration = airAcceleration;
                    floorBool = true;
                }
            }
        }

        public void Blink()
        {
            if (blinkAvailable == true)
            {
                if (Physics.CheckBox(EmptyBlink.transform.position + blinkPosition, playerSize) == false)
                {
                    rigi.velocity = new Vector3(rigi.velocity.x, 0f, rigi.velocity.z);
                    gameObject.transform.Translate(BlinkTarget.transform.localPosition);
                }
                else
                {
                    gameObject.transform.Translate(BlinkTarget.transform.localPosition);
                }
            }
        }

        IEnumerator BlinkAproveAvailability()
        {
            yield return new WaitForSeconds(3f);
            blinks = 3;
            blinkAvailable = true;
        }

        public Vector3 GetShootDir()
        {
            return (ShotTarget.transform.position - EmptyBlink.transform.position).normalized;
        }

    }
}
