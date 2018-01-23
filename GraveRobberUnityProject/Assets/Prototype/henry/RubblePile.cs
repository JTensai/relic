using UnityEngine;
using System.Collections;

public class RubblePile : EnvironmentBase {
	public GameObject ThrowableDebris;
	private InteractableComponent _interactable;
	// Use this for initialization
	void Start () {
		_interactable = GetComponent<InteractableComponent>();
		_interactable.OnInteract += HandleOnInteract;
		_interactable.OnNotify += HandleOnNotify;
	}

	void HandleOnNotify (InteractableNotifyEventData data)
	{
		
	}

	void HandleOnInteract (InteractableInteractEventData data)
	{
		if(data.IsPlayer){
			GameObject g = (GameObject)GameObject.Instantiate(ThrowableDebris, 
				gameObject.transform.position + new Vector3(0, 0.5f, 0), gameObject.transform.rotation);
			Physics.IgnoreCollision(collider, g.collider);
			g.GetComponent<ThrowablePrefab>().HandleOnInteract(data);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
