using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class malMov : MonoBehaviour {

    Rigidbody rigi;
    public float speed;
    float dirX = 0;

	// Use this for initialization
	void Start () {
        rigi = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKey(KeyCode.A))
            dirX = -1;
        else if (Input.GetKey(KeyCode.D))
            dirX = 1;
        else
            dirX = 0;*/
        dirX = 1;
	}

    private void FixedUpdate()
    {
        transform.Translate(dirX * speed * Time.fixedDeltaTime, 0, 0);
    }
}
