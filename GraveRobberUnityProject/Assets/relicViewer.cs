using UnityEngine;
using System.Collections;

public class relicViewer : MonoBehaviour {

	
	private InteractableComponent interact;
	public string ReadMe;
	
	// Use this for initialization
	void Start () {
		
		
		interact = GetComponent<InteractableComponent> ();
		interact.OnNotify += showDoor;
		interact.OnInteract += viewRelic;
	}
	
	// Update is called once per frame
	void Update () {
	
	}






	
	public void showDoor(InteractableNotifyEventData data){
	}
	
	
	public void viewRelic(InteractableInteractEventData data){
		GameUI.DisplayInstructionTextArea(ReadMe,10f);

		
		
	}

}
