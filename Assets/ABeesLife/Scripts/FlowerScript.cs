using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour {

	public bool pollenPresent = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool hasPollen() {
		return pollenPresent;
	}

	public void takeNectar() {
		Destroy (gameObject);
	}
}
