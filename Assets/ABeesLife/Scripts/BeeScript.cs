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
	//public GameObject uiPic01;
	public GameObject uiPic02;
	public GameObject uiPic03;

	public string dieOnGroundMessage = "Fuiste devorado por una araña gigantesca :(";

    [SerializeField] private AudioSource worldAudioSource;
    [SerializeField] private AudioClip deathSound;  // the sound played when character touches the ground

    private CharacterController characterController;

    private float nectarMeter;
    private float pollenMeter;
    private float bellyMeter;

    private float nectarIncrease = 5;
    private float pollenIncrease = 10;
    private float flyingEnergyDecrease = -1;
    private float flyingEnergyIncrease = 5;
    private float bellyDecrease = -0.0833333F; //(100 / (20 * 60))

	private int pollinatedFlowersCounter = 0;
	private bool isAlive = true;

	private bool pic01Seen = true;
	private bool pic02Seen = true;
	private bool pic03Seen = true;

    

    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        txtPollinatedFlowersCounter.text = "" + pollinatedFlowersCounter;
    }
	
	// Update is called once per frame
	void Update () {
		if (isAlive) {
			bellyRadialProgressBar.UpdateAmount(bellyDecrease * Time.deltaTime);

			if (Input.GetButtonDown("Jump")) {
				flyingEnergyRadialProgressBar.UpdateAmount(flyingEnergyDecrease);
			} 
			else if (flyingEnergyRadialProgressBar.GetCurrentAmount() < 100 && characterController.isGrounded) {
				//Debug.Log(flyingEnergyIncrease * Time.deltaTime);
				flyingEnergyRadialProgressBar.UpdateAmount(flyingEnergyIncrease * Time.deltaTime);
			}
		}
	}

    void PlayDeathSound() {
        worldAudioSource.clip = deathSound;
        worldAudioSource.Play();
    }

    void Die() {
		isAlive = false;
	}

	void ShowEndScreen(string message) {
        txtDie.text = message;
		txtDie.text += "\n\nPero polinizaste " + pollinatedFlowersCounter + " flores!\n\nY aquí hay unas lindas fotos de los lugares que visitaste";

		if (pic01Seen) {
			//uiPic01.SetActive (true);
		} else if (pic02Seen) {
			uiPic02.SetActive (true);
		} else if (pic03Seen) {
			uiPic03.SetActive (true);
		}

		uiDie.SetActive (true);

		if (pic01Seen) {
			//uiPic01.SetActive (true);
		} else if (pic02Seen) {
			uiPic02.SetActive (true);
		} else if (pic03Seen) {
			uiPic03.SetActive (true);
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Trigger with: " + other.tag);

		if (isAlive) {
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
                PlayDeathSound ();
                ShowEndScreen (dieOnGroundMessage);
			} else if (other.CompareTag ("Pic02")) {
				Debug.Log ("PIC02");
				pic02Seen = true;
			} else if (other.CompareTag ("Pic03")) {
				Debug.Log ("PIC03");
				pic03Seen = true;
			} 
		}
	}
}
