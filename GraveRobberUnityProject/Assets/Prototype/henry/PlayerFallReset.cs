using UnityEngine;
using System.Collections;

public class PlayerFallReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if(c.GetComponent<PlayerBase>() != null){
			LevelBehavior.Instance.ResetPlayer();
		}
	}
}
