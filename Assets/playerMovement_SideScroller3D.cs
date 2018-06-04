using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement_SideScroller3D : MonoBehaviour {
    public float maximumSpeed;
    public float maxAcceleration;
    public float jumpForce;
    private Rigidbody rigi;
    private float m_Horizontal;
    private bool m_Jump;

    void Start()
    {
        rigi = GetComponent<Rigidbody>();
    }

    void Update()
    {
        m_Horizontal = Input.GetAxis("Horizontal");
        m_Jump = Input.GetButton("Jump");

        if(m_Jump == true)
        {
            rigi.velocity += new Vector3();
        }

        if(m_Horizontal != 0)
        {
            rigi.velocity += new Vector3(maxAcceleration * m_Horizontal * Time.deltaTime, 0.0f, 0.0f);
            if(rigi.velocity.x >= maximumSpeed)
            {
                rigi.velocity = new Vector3(maximumSpeed, rigi.velocity.y, rigi.velocity.z);
            }
            else if (rigi.velocity.x <= -maximumSpeed)
            {
                rigi.velocity = new Vector3(-maximumSpeed, rigi.velocity.y, rigi.velocity.z);
            }
        }
        
    }

}
