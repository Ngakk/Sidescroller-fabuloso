﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVisionRange : MonoBehaviour {

	public float distance;
	Camera cam;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
