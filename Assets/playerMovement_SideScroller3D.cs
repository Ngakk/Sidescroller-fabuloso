using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement_SideScroller3D : MonoBehaviour {
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
    public bool floorBool;
    private Rigidbody rigi;
    private float m_Horizontal;
    private float m_Vertical;
    private float cam_Horizontal;
    private float cam_Vertical;
    private float blinkDistance;
    private bool m_Jump;
    private bool jumpReq;
    [Range(0, 1)]
    public float desacceleration;
    public bool wallCheck;
    public int jumpCount;

    List<ContactPoint> objectsTouched = new List<ContactPoint>();


    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        blinkDistance = 10.0f;
    }

    void Update()
    {
        m_Horizontal = Input.GetAxis("Horizontal");
        m_Vertical = Input.GetAxis("Vertical");
        cam_Horizontal = Input.GetAxis("Cam_Horizontal");
        cam_Vertical = Input.GetAxis("Cam_Vertical");
        RaycastHit hit;
        Ray blinkCheckRay = new Ray(EmptyBlink.transform.position, new Vector3 (m_Horizontal * 3,m_Vertical * 3, 0f));
        Debug.DrawRay(EmptyBlink.transform.position, new Vector3(m_Horizontal * 2,m_Vertical * 2, 0f));
        if (Input.GetButtonDown("Jump")/* && groundTouched.Count != 0*/)
        {
            jumpCount++;
            if(jumpCount <= 1)
            jumpReq = true;
        }

        if(floorBool == false)
        {
            AirDesacceleration();
        }
    }

    void FixedUpdate()
    {
        /* SECCION DE SALTO */
        if (jumpReq)
        {
            if(jumpCount == 0)
            {
                currentAcceleration = airAcceleration;
                rigi.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpReq = false;
            }
            else
            {
                currentAcceleration = airAcceleration;
                rigi.velocity = new Vector3(rigi.velocity.x, 0f, rigi.velocity.z);
                rigi.AddForce(Vector3.up * jumpForce/2, ForceMode.Impulse);
                jumpReq = false;
            }
        }

        if (rigi.velocity.y < 0)
        {
            rigi.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if (rigi.velocity.y > 0 && !Input.GetButton("Jump"))
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
        for(int i = 0; i<_col.contacts.Length; i++)
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
        for(int i = 0; i < objectsTouched.Count; i++)
        {
            if (objectsTouched[i].normal.y >= 0.9f)
            {
                currentAcceleration = airAcceleration;
                floorBool = true;
            }
        }
    }

    public void AirDesacceleration()
    {
        if(rigi.velocity.x > 0)
            rigi.velocity = new Vector3(rigi.velocity.x * -desacceleration * Time.deltaTime, rigi.velocity.y, rigi.velocity.z);
        else
            rigi.velocity = new Vector3(rigi.velocity.x * desacceleration * Time.deltaTime, rigi.velocity.y, rigi.velocity.z);
    }
}
