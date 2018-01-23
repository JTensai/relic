using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TurretController : MonoBehaviour {
	
	public List<Turret> list;
	private double countDown;
	private bool paused = false;
	private double delayTime =1;
	

	private bool On = false;
	
	
	// Use this for initialization
	void Start () 
	{

		GetComponent<InteractableComponent>().OnNotify += HandleOnNotify;
		GetComponent<InteractableComponent>().OnInteract += HandleOnInteract;		
	}

	void HandleOnNotify (InteractableNotifyEventData data)
	{

	}
	
	void HandleOnInteract (InteractableInteractEventData data)
	{
		if (!paused) {
			pauseTransports ();
			On = !On;

			foreach (Turret obj in  list) {
				obj.GetComponent<Turret>().toggleState();

			}

			GameUI.DisplayInstructionTextArea("Turrets Toggled", 2f);
		}
	}
	
	
	public void pauseTransports()
	{
		paused = true;
		countDown = delayTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(paused)
		{
			countDown -= Time.deltaTime;
			if(countDown <= 0.0f)
				paused = false;
		}
	}
}
