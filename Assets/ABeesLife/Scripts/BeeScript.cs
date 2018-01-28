using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScript : MonoBehaviour {

	public float bellyMeter = 100;
	public float nectarMeter = 0;
	public float pollenMeter = 0;

	public float bellyDecrease = 1;
	public float nectarIncrease = 5;
	public float pollenIncrease = 10;

	// Use this for initialization
	void Start () {
		Debug.Log ("Belly: " + bellyMeter);
		Debug.Log ("Nectar: " + nectarMeter);
		Debug.Log ("Pollen: " + pollenMeter);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Jump")) {
			bellyMeter -= bellyDecrease;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Nectar")) {
			FlowerScript flowerScript = other.GetComponent<FlowerScript> ();
			if (flowerScript != null) {
				flowerScript.takeNectar ();
				nectarMeter += nectarIncrease;

				if (flowerScript.hasPollen ()) {
					pollenMeter += pollenIncrease;
				} else {
					pollenMeter -= pollenIncrease;
				}
				Debug.Log ("Belly: " + bellyMeter);
				Debug.Log ("Nectar: " + nectarMeter);
				Debug.Log ("Pollen: " + pollenMeter);
			} else {
				Debug.Log ("FlowerScript not set to object with Nectar tag");
			}
		}
	}
}
