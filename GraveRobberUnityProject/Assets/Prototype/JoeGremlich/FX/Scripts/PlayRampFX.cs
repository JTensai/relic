using UnityEngine;
using System.Collections;

public class PlayRampFX : MonoBehaviour {
	public EffectBase FX;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		EffectBase newInstance = FX.GetInstance (other.transform.position);
		newInstance.PlayEffect ();
	}
}
