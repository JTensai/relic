using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {


	private InteractableComponent interact;
	public float doorNumber;
	private float timer = 1f;
	private bool on = false;

	// Use this for initialization
	void Start () {


		interact = GetComponent<InteractableComponent> ();
		interact.OnNotify += showDoor;
		interact.OnInteract += openDoor;
	}





	
	// Update is called once per frame
	void Update () {
		if (on) {
		if(timer <=0)	
			{
				on  = false;
			}
			else
			{timer -= Time.deltaTime;
				Vector3 pos = this.transform.position;
				pos.y -= .12f;
				this.transform.position = pos;

			}


		}




	
	}
	public void open()
	{on = true;
		}

	public void showDoor(InteractableNotifyEventData data){
	}


	public void openDoor(InteractableInteractEventData data){
		if (GameObject.FindGameObjectWithTag ("Inventory").GetComponent<StatTracker> ().getKey (doorNumber)) {
			on = true;			
			//Vector3 pos = this.transform.position;
						//pos.y += 5;
						//this.transform.position = pos;
				}
		else {
			GameUI.DisplayInstructionTextArea("It's Locked", 5f);
		}


	}




}
