using UnityEngine;
using System.Collections;

public class seesaw : MonoBehaviour {

	private BoxCollider left;
	BoxCollider right;


	// Use this for initialization
	void Start () {
	

		right = (BoxCollider)this.gameObject.AddComponent ("BoxCollider");
		right.enabled = true;
		right.isTrigger = true;
		right.center = new Vector3 (.35f,1,0);
		right.size = new Vector3 (.25f,2,1);

		left = (BoxCollider)this.gameObject.AddComponent ("BoxCollider");
		left.enabled = true;
		left.isTrigger = true;
		left.center = new Vector3 (-.35f, 1, 0);
		left.size = new Vector3 (.25f,2,1);


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<HealthComponent>() != null)
		{Debug.Log (other.transform.position);
			Debug.Log ("Left " + left.center);

			if(other.transform.position.x < this.gameObject.transform.position.x)
			{Debug.Log("go left");
				this.rigidbody.AddForce(new Vector3(75,0,0));}
				                      
			else{this.rigidbody.AddForce(new Vector3(-75,0,0));
				Debug.Log("go right");}
			//this.gameObject.rigidbody.AddForce(new Vector3(5,0,0));
		
			}
	}


}
