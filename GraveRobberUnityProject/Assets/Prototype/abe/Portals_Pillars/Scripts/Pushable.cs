using UnityEngine;
using System.Collections;

public class Pushable : MonoBehaviour 
{

	// Use this for initialization
	private float playerSpeed = 3.5f;
	private float blockHeight;
	private bool playerTouching = false;
	void Start () 
	{
		GetComponent<InteractableComponent>().OnNotify += HandleOnNotify;
		GetComponent<InteractableComponent>().OnInteract += HandleOnInteract;
		blockHeight = 0.5f + transform.parent.transform.position.y;
	}

	void HandleOnInteract (InteractableInteractEventData data)
	{
		if(data.Source.name.Contains ("Player") && playerTouching)
		{
			Vector3 direction = data.Source.transform.forward;
			float angle = Vector3.Angle(direction, transform.position - data.Source.transform.position);
			if(angle < 40.0f)
			{
				Vector3 newLocation = data.Source.transform.position + (direction*1.0f);
				newLocation.y = blockHeight;
				transform.position = newLocation;//data.Source.transform.position + (direction*1.0f);
				Vector3 forceVec = data.Source.transform.forward * playerSpeed;
				rigidbody.velocity = forceVec;
			}
		}
	}

	void HandleOnNotify (InteractableNotifyEventData data)
	{
	}

	// Update is called once per frame
	void Update () 
	{
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.name.Contains("Player"))
			playerTouching = true;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.name.Contains("Player"))
			playerTouching = false;
	}
}
