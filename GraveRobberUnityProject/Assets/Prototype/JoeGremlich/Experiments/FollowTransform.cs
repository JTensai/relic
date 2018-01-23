using UnityEngine;
using System.Collections;

public class FollowTransform : MonoBehaviour {
	public Transform followThis;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (followThis != null)
			this.transform.position = followThis.transform.position;
	}
}
