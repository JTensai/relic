using UnityEngine;
using System.Collections;

public class TrailBase : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void StopEffect(){
		GameObject.Destroy(gameObject);
	}

	public TrailBase GetInstance(Vector3 position){
		GameObject createdEffect = (GameObject)GameObject.Instantiate(this.gameObject, position, this.transform.rotation);
		TrailBase tb = createdEffect.GetComponent<TrailBase>();
		return tb;
	}
}
