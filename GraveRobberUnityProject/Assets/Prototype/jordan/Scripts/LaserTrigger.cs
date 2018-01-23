using UnityEngine;
using System.Collections;

public class LaserTrigger : MonoBehaviour {
	public AxeTrap axeTrap;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		axeTrap.ActivateTrap ();
		Destroy (this.gameObject);
	}
}
