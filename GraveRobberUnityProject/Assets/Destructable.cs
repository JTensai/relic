using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision other)
	{Debug.Log (other.gameObject.tag);
		if (other.gameObject.CompareTag("SqaurePiller")) {
			Destroy(this.gameObject);
				
		
		}

		}

}
