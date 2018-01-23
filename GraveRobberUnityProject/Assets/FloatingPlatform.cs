using UnityEngine;
using System.Collections;

public class FloatingPlatform : MonoBehaviour {

	public WaterController controls;
	public bool moving;
	// Use this for initialization
	void Start () {
	
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	void OnTriggerEnter(Collider other)
	
	{Debug.Log ("adding " +other.gameObject.name);
		controls.player = other.gameObject;

	}




}
