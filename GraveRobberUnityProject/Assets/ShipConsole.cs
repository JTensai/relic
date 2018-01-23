using UnityEngine;
using System.Collections;

public class ShipConsole : MonoBehaviour {


	private InteractableComponent interact;
	private GameHUDManager GameHUD;

	public bool showMission = false;
	public bool showInventory = false;

	// Use this for initialization
	void Start () {

		
		interact = GetComponent<InteractableComponent> ();
		interact.OnNotify +=handleOnNotify;
		interact.OnInteract += HandleOnInteract;

		if (GameObject.FindGameObjectWithTag ("GameHUD") != null)
		{
			GameHUD = GameObject.FindGameObjectWithTag ("GameHUD").GetComponent<GameHUDManager> ();
		}

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HandleOnInteract (InteractableInteractEventData data)
		{if (showMission) {
			GameHUD.ShowMissionObjective ();
				} 
		else if (showInventory) {

			GameHUD.showShipShop();

		


		}

		}



	
	public void handleOnNotify(InteractableNotifyEventData data){
		}



}
