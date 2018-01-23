using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SetStartRotation : MonoBehaviour {
	ParticleSystem pSystem;
	public bool ActivateScript = true;
	// Use this for initialization
	void Start () {
		pSystem = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(ActivateScript)
			pSystem.startRotation = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
	}
}
