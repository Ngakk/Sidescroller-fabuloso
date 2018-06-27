using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour {

	public Camera cam;
	public float zDistance;
	public GameObject center;
	public float xOffset;
	public float yOffset;
	// Use this for initialization
	void Start () {
		transform.position = new Vector3(center.transform.position.x, center.transform.position.y, -20);
	}
	
	// Update is called once per frame
	void Update () {
		xOffset = (cam.transform.position.x - center.transform.position.x) / zDistance;
		yOffset = (cam.transform.position.y - center.transform.position.y) / zDistance;
		transform.position = new Vector3 (
				center.transform.position.x + xOffset,
				center.transform.position.y + yOffset,
				-60);
	}
}
