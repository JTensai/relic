using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshReplacer : MonoBehaviour {


	public List<GameObject> showables = new List<GameObject>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnDestroy()
	{

		foreach (GameObject obj in showables) {
			obj.GetComponent<MeshRenderer>().enabled = true;

		}

	}

}
