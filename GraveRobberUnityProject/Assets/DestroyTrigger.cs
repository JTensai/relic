using UnityEngine;
using System.Collections;

public class DestroyTrigger : MonoBehaviour {

	public GameObject Link;
	public GameObject Link2;

	// Use this for initialization
	void Start () {

	
	}

	void OnDestroy()
		{
		Destroy (Link.gameObject);
		Destroy (Link2.gameObject);
	}
	
	// Update is called once per frame
	void Update () {


	
	}
}
