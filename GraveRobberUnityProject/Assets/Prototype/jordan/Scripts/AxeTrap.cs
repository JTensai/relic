using UnityEngine;
using System.Collections;

public class AxeTrap : MonoBehaviour {
	public AxeTrap axeTrap;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateTrap(){
		Debug.Log ("Axe Trap has been activated!");
		this.gameObject.rigidbody.useGravity = true;
	}

	void OnCollisionEnter(Collision other){
		//if (i == 0) 
		//{
		//	i++;
		//} 
		//else {
		//	Debug.Log ("Sleeping");
		//	this.gameObject.rigidbody.Sleep ();
		//}
	}

}
