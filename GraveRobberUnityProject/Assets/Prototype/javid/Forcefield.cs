using UnityEngine;
using System.Collections;

public class Forcefield : Triggerable {

	public virtual void OnTriggerEnter (Collider other)
	{
		throw new System.NotImplementedException ();
	}

	public virtual void OnTriggerExit (Collider other)
	{
		throw new System.NotImplementedException ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void Toggle(){
		throw new System.NotImplementedException ();
	}
}
