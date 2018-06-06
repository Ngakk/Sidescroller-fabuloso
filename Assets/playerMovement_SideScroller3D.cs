using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement_SideScroller3D : MonoBehaviour {
    public float maximumSpeed;
    public float maxAcceleration;
    [Range(1, 20)]
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public GameObject floorChecker;
    private Rigidbody rigi;
    private float m_Horizontal;
    private bool m_Jump;
    private bool jumpReq;

    List<Collider> groundTouched = new List<Collider>();


    void Start()
    {
        rigi = GetComponent<Rigidbody>();
    }

    void Update()
    {
        m_Horizontal = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && groundTouched.Count != 0)
        {
            jumpReq = true;
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

    void FixedUpdate()
    {
        if(jumpReq)
        {
            //rigi.velocity = Vector3.up * jumpForce;
            rigi.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpReq = false;
        }

        if (rigi.velocity.y < 0)
        {
            rigi.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if (rigi.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigi.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void OnCollision(Collision _col)
    {
        ContactPoint[] points = new ContactPoint[2];
        
    }

}
