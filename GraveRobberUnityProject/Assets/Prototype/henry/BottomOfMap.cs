using UnityEngine;
using System.Collections;

public class BottomOfMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			HealthComponent h = c.gameObject.GetComponent<HealthComponent>();
			h.Kill ();
				}
	}

	void OnControllerColliderHit (ControllerColliderHit c){
		if (c.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			HealthComponent h = c.gameObject.GetComponent<HealthComponent>();
			h.Kill ();
		}
	}
}
