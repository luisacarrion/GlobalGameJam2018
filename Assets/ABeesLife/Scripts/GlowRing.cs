using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowRing : MonoBehaviour {
	public float frequency = 1;
	public float magnitude = 1;
	public float rotationAngle = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.up * Mathf.Sin (Time.time * frequency) * magnitude);
		//transform.Rotate (Vector3.up, rotationAngle, Space.World);
		transform.Rotate(Vector3.up * Time.deltaTime * rotationAngle, Space.World);
	}
}
