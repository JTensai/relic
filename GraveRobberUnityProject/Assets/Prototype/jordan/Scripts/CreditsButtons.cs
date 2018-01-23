using UnityEngine;
using System.Collections;

public class CreditsButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void returnToMenu()
	{
		Application.LoadLevel("MainMenu");
	}

	public void goToSecretScene()
	{
		Application.LoadLevel("CreditsBonusRoom");
	}
}
