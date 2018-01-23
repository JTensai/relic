using UnityEngine;
using System.Collections;

public class RelicMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
#if UNITY_EDITOR
	[UnityEditor.MenuItem ("Relic/Disable All Colliders")]
	static void DisableColliders(){
		Collider[] cols = GameObject.FindObjectsOfType<Collider>();
		foreach(Collider c in cols){
			c.enabled = false;
		}
	}

	[UnityEditor.MenuItem ("Relic/Enable All Colliders")]
	static void EnableColliders(){
		Collider[] cols = GameObject.FindObjectsOfType<Collider>();
		foreach(Collider c in cols){
			c.enabled = true;
		}
	}
#endif
}
