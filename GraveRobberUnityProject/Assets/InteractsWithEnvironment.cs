using UnityEngine;
using System.Collections;

public class InteractsWithEnvironment : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		InteractableComponent[] inters = GameObject.FindObjectsOfType<InteractableComponent>();
		foreach(InteractableComponent i in inters)
		{
			if(Vector3.Distance(transform.position, i.transform.position) < 3f)
				i.NotifyProximity(new InteractableNotifyEventData(gameObject, false, Vector3.Distance(transform.position, i.transform.position), 1f));
		}
	}
}
