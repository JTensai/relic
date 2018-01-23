using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	Transform lookAtPos;
	// Use this for initialization
	void Start () {
		lookAtPos = GameObject.Find ("LookAtPoint").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (lookAtPos);
	}
}
