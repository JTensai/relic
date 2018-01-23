using UnityEngine;
using System.Collections;

public class WaterTrigger : MonoBehaviour {

	public GameObject control;
	private WaterController trigger;
	private InteractableComponent interact;


	// Use this for initialization
	void Start () {
		trigger = control.GetComponent<WaterController> ();
		interact = GetComponent<InteractableComponent> ();
		interact.OnNotify +=handleOnNotify;
		interact.OnInteract += HandleOnInteract;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void handleOnNotify(InteractableNotifyEventData data){
	}
	
	
	
	public void HandleOnInteract (InteractableInteractEventData data)
	{

		trigger.HandleOnInteract (data);

		}

}
