using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistantTrigger : MonoBehaviour {

	private HashSet<Collider> colliders = new HashSet<Collider>();

	void FixedUpdate(){
		colliders.Clear ();
	}

	void OnTriggerEnter(Collider c){
		colliders.Add (c);
	}

	void OnTriggerStay(Collider c){
		colliders.Add (c);
	}

	public HashSet<Collider> GetOverlappingColliders(){
		return colliders;
	}

}
