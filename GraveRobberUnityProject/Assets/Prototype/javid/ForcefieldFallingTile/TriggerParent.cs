using UnityEngine;
using System.Collections;

public class TriggerParent : MonoBehaviour {

	private GameObject parent;
	private Forcefield parentScript;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		parentScript = parent.GetComponent<Forcefield> ();
	//	detectorScript = GetComponent<TriggerDetector>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		parentScript.OnTriggerEnter (other);
	}
	
	void OnTriggerExit(Collider other) {
		parentScript.OnTriggerExit (other);
	}
}
