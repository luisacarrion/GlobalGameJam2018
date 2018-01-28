using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour {

	private bool nectarPresent = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool hasNectar() {
		return nectarPresent;
	}

	public void takeNectar() {
		nectarPresent = false;
	}
}
