using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour {

    public Transform ChargingBar;
    public Transform TextIndicator;
    [SerializeField] private float currentAmount;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void UpdateAmount(float change) {
        currentAmount += change;
        TextIndicator.GetComponent<Text>().text = ((int)currentAmount).ToString() + "%";
        ChargingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
    }

    public float GetCurrentAmount() {
        return currentAmount;
    }

    public void ResetAmount() {

    }
}
