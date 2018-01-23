using UnityEngine;
using System.Collections;

public class DoorSwitch : MonoBehaviour {
	
	private double countDown;
	private bool paused;
	private double delayTime =1;
	private bool used = false;
	private InteractableComponent interact;
	public GameObject door;
	public GameObject handle;

	// Use this for initialization
	void Start () {
		
		interact = GetComponent<InteractableComponent> ();
		interact.OnNotify += showDoor;
		interact.OnInteract += openDoor;
	}
	
	
	void HandleOnInteract (InteractableInteractEventData data)
	{

	}
	



	public void showDoor(InteractableNotifyEventData data){
	}
	
	
	public void openDoor(InteractableInteractEventData data){
		if(!used)
			{
			door.GetComponent<DoorOpen> ().open ();
			used = true;
		}
		pauseTransports ();


		
		
	}

	
	public void pauseTransports()
	{
		paused = true;
		countDown = delayTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(paused)
		{handle.transform.Rotate(new Vector3(1f,0,0));
			countDown -= Time.deltaTime;
			if(countDown <= 0.0f)
				paused = false;
		}
	}}
