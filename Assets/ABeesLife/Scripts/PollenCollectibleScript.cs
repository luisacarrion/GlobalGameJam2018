using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenCollectibleScript : MonoBehaviour {

	public int order;

	// Use this for initialization
	void Start () {
		//Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		Destroy(gameObject);
	}
}
