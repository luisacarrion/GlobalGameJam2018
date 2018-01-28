using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeScript : MonoBehaviour {

    public RadialProgressBar nectarRadialProgressBar;
    public RadialProgressBar pollenRadialProgressBar;
    public RadialProgressBar bellyRadialProgressBar;
    public RadialProgressBar flyingEnergyRadialProgressBar;

	public GameObject uiDie;
	public Text txtDie;
	public Text txtPollinatedFlowersCounter;

	public string dieOnGroundMessage = "Fuiste devorado por una araña gigantesca :(";

    private float nectarMeter;
    private float pollenMeter;
    private float bellyMeter;

    private float nectarIncrease = 5;
    private float pollenIncrease = 10;
    private float flyingEnergyDecrease = -1;
    private float flyingEnergyIncrease = 0.8F;
    private float bellyDecrease = -0.0833333F; //(100 / (20 * 60))

	private int pollinatedFlowersCounter = 0;


    // Use this for initialization
    void Start () {
		txtPollinatedFlowersCounter.text = "" + pollinatedFlowersCounter;
	}
	
	// Update is called once per frame
	void Update () {
        bellyRadialProgressBar.UpdateAmount(bellyDecrease * Time.deltaTime);

		if (Input.GetButtonDown("Jump")) {
            flyingEnergyRadialProgressBar.UpdateAmount(flyingEnergyDecrease);
        } 
        else if (flyingEnergyRadialProgressBar.GetCurrentAmount() < 100) {
            //Debug.Log(flyingEnergyIncrease * Time.deltaTime);
            flyingEnergyRadialProgressBar.UpdateAmount(flyingEnergyIncrease * Time.deltaTime);
        }
	}

	void Die() {
		
	}

	void ShowEndScreen(string message) {
		txtDie.text = message;
		txtDie.text += "\n\nPero polinizaste " + pollinatedFlowersCounter + " flores!\n\nY aquí hay unas lindas fotos de los lugares que visitaste";
		uiDie.SetActive (true);
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Trigger with: " + other.tag);

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

					if (pollenRadialProgressBar.GetCurrentAmount () > 0) {
						pollenRadialProgressBar.UpdateAmount(-pollenIncrease);
						pollinatedFlowersCounter++;
						txtPollinatedFlowersCounter.text = "" + pollinatedFlowersCounter;
					}
                    
				}
				//Debug.Log ("Belly: " + bellyMeter);
				//Debug.Log ("Nectar: " + nectarMeter);
				//Debug.Log ("Pollen: " + pollenMeter);
            }
            else {
				Debug.Log ("FlowerScript not set to object with Nectar tag");
			}
		} else if (other.CompareTag ("Respawn")) {
			// ROGELIO: Fill belly to 100%
		} else if (other.CompareTag ("Ground")) {
			Die ();
			ShowEndScreen (dieOnGroundMessage);
		}
	}
}
