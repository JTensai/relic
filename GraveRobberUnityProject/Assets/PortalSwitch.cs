using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortalSwitch : MonoBehaviour {

	public List<Portal> list;
	private double countDown;
	private bool paused;
	private double delayTime =1;

	public bool InteractToActivate;
	private bool On = false;

	
	// Use this for initialization
	void Start () 
	{
			GetComponent<InteractableComponent>().OnInteract += HandleOnInteract;


	}

	void HandleOnInteract (InteractableInteractEventData data)
	{
		if (!paused) {
						pauseTransports ();
						On = !On;
						
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
