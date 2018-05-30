using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

    public Camera cam;
    public float speed;
    public float lowerShakeThreshold;
    Rigidbody rigi;
    int dirX;

	// Use this for initialization
	void Start () { 
        rigi = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
            dirX = -1;
        else if (Input.GetKey(KeyCode.D))
            dirX = 1;
        else
            dirX = 0;

        if (Input.GetKeyDown(KeyCode.Space))
            rigi.AddForce(Vector3.up * speed, ForceMode.Impulse);

        rigi.velocity += new Vector3(dirX * speed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > lowerShakeThreshold)
            cam.GetComponent<CameraShake>().AddTrauma(collision.relativeVelocity.magnitude/50);
    }
}
