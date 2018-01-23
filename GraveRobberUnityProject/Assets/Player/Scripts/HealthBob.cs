using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HealthBob : MonoBehaviour {
	public AnimationCurve yPos = new AnimationCurve();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float xPos = transform.position.x;
		float zPos = transform.position.z;
		float y = Mathf.Sin (Time.time) + transform.position.y;
		transform.position = new Vector3 (xPos, y, zPos);
	}
}
