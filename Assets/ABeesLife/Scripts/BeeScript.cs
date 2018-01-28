using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScript : MonoBehaviour {

    public RadialProgressBar nectarRadialProgressBar;
    public RadialProgressBar pollenRadialProgressBar;
    public RadialProgressBar bellyRadialProgressBar;
    public RadialProgressBar flyingEnergyRadialProgressBar;

    private float nectarMeter;
    private float pollenMeter;
    private float bellyMeter;

    private float nectarIncrease = 5;
    private float pollenIncrease = 10;
    private float flyingEnergyDecrease = -1;
    private float flyingEnergyIncrease = 0.8F;
    private float bellyDecrease = -0.0833333F; //(100 / (20 * 60))

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        bellyRadialProgressBar.UpdateAmount(bellyDecrease * Time.deltaTime);

		if (Input.GetButtonDown("Jump")) {
            flyingEnergyRadialProgressBar.UpdateAmount(flyingEnergyDecrease);
        } 
        else if (flyingEnergyRadialProgressBar.GetCurrentAmount() < 100) {
            Debug.Log(flyingEnergyIncrease * Time.deltaTime);
            flyingEnergyRadialProgressBar.UpdateAmount(flyingEnergyIncrease * Time.deltaTime);
        }
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Nectar")) {
			FlowerScript flowerScript = other.GetComponent<FlowerScript> ();
			if (flowerScript != null) {
				flowerScript.takeNectar ();
				nectarMeter += nectarIncrease;
                nectarRadialProgressBar.UpdateAmount(nectarIncrease);

				if (flowerScript.hasPollen ()) {
					pollenMeter += pollenIncrease;
                    pollenRadialProgressBar.UpdateAmount(pollenIncrease);
				} else {
					pollenMeter -= pollenIncrease;
                    pollenRadialProgressBar.UpdateAmount(-pollenIncrease);
				}
				Debug.Log ("Belly: " + bellyMeter);
				Debug.Log ("Nectar: " + nectarMeter);
				Debug.Log ("Pollen: " + pollenMeter);
            }
            else {
				Debug.Log ("FlowerScript not set to object with Nectar tag");
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag ("Respawn")) {
			// Fill belly to 100%
		}
	}
}
