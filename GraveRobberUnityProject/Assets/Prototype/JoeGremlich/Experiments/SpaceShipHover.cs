using UnityEngine;
using System.Collections;

public class SpaceShipHover : MonoBehaviour {
	private float ySpot;

	// Use this for initialization
	void Start () {
		ySpot = this.gameObject.transform.position.y;
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tempPostion = transform.position;
		tempPostion.y = ySpot + (Mathf.Sin (Time.time) * 0.07f);
		transform.position = tempPostion;
		//rigidbody.MovePosition (tempPostion);
		Quaternion tempRotation = transform.rotation;
		tempRotation.z = Mathf.Sin (Time.time * 0.5f) * (0.04f);
		transform.rotation = tempRotation;
		//rigidbody.MoveRotation (tempRotation);
	}
}
