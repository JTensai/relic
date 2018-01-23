using UnityEngine;
using System.Collections;

public class ToggleForceFields : MonoBehaviour {

	public Forcefield[] forcefields;

	private bool interactable = true;

	// Use this for initialization
	void Start () {
		GetComponent<InteractableComponent>().OnInteract += HandleOnInteract;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void HandleOnInteract (InteractableInteractEventData data)
	{

		if (interactable) {
			Debug.Log("!!!");
						StartCoroutine (enableDelay ());
						if (forcefields != null) {
								foreach (Forcefield forcefield in forcefields) {
										forcefield.Toggle ();
								}
						}
				}

	}

	private IEnumerator enableDelay() {
		interactable = false;
		yield return new WaitForSeconds(.3f);	
		interactable = true;

	}

}
