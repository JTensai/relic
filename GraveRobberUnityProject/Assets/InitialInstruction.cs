using UnityEngine;
using System.Collections;

public class InitialInstruction : MonoBehaviour {

	private GameHUDManager manager;
	private bool leftStartZone;
	private GameObject player;
	private LevelBehavior lb;


	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameHUD").GetComponent<GameHUDManager>();
		leftStartZone = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		lb = GameObject.FindObjectOfType<LevelBehavior> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!lb.ReturnTrip){
			if (Vector3.Distance (player.transform.position, this.gameObject.transform.position) < 4) {
				manager.DisplayNotificationTextArea("Objectives: " + System.Environment.NewLine + System.Environment.NewLine + 
				                                    "1. Get the Main Relic" + System.Environment.NewLine + 
				                                    "2. Get back Out", .3f);

			//	Debug.Log("SS" + Vector3.Distance (player.transform.position, this.gameObject.transform.position));
			}
		}
	}

	/*void OnTriggerExit(Collider c)
	{
		if (c.CompareTag("Player"))
		{
			Debug.Log("OnTriggerExit");
	//		if (!leftStartZone)
	//		{
			Debug.Log("Displaying Text");
				manager.DisplayNotificationTextArea("Objectives: " + System.Environment.NewLine + System.Environment.NewLine + 
				                                "1. Get the Main Relic" + System.Environment.NewLine + 
				                                "2. Get back Out", 3f);
	//		}
			leftStartZone = true;
		}
	}
	void OnTriggerEnter(Collider c)
	{
		if (c.CompareTag("Player"))
		{
			Debug.Log("OnTriggerEnter");
			if(leftStartZone){}
			//	manager.DisplayNotificationTextArea("Come back when you have the relic.",3f);
		}
	}

	void OnTriggerStay(Collider c)
	{
		if (c.CompareTag("Player"))
		{
			Debug.Log("OnTriggerStay");
			Debug.Log("> "+c.gameObject.rigidbody.IsSleeping());
			manager.DisplayNotificationTextArea("Objectives: " + System.Environment.NewLine + System.Environment.NewLine + 
			                                    "1. Get the Main Relic" + System.Environment.NewLine + 
			                                    "2. Get back Out", .1f);

			if(leftStartZone)
				manager.UpdateNotificationDuration(0.1f);
		}
	}*/
}
