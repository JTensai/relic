using UnityEngine;
using System.Collections;

public class FallingPillarSoundTrigger : MonoBehaviour {
	
	private BoxCollider FinishedFallTrigger;
	private bool _enabled;

	// Use this for initialization
	void Start () {		
		FinishedFallTrigger = GetComponent<BoxCollider> ();
		if (!FinishedFallTrigger.isTrigger) {
			FinishedFallTrigger = GetComponent<BoxCollider>();
		}
		else
		{
			FinishedFallTrigger = gameObject.AddComponent<BoxCollider> ();
			FinishedFallTrigger.center = new Vector3 (0.6f, 4.5f, 0f);
			FinishedFallTrigger.size = new Vector3 (0.2f, 1f, 1f);
			FinishedFallTrigger.isTrigger = true;
		}
		_enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (rigidbody.velocity.magnitude);
//		if (rigidbody.velocity.magnitude > 0.05)
//		{
//			if (!enabled){
//				enabled = true;
//				PlayStartFallSoundEffect();
//			}
//		}
	}	

	void OnTriggerEnter(Collider c)
	{
		if (_enabled){
			if (!c.gameObject.CompareTag ("Player")) {
				_enabled = false;
				PlayCrashSoundEffect ();
			}
		}
	}
	
//	private void PlayStartFallSoundEffect()
//	{
//		Debug.Log ("PlayStartFallSoundEffect");
//	}
	
	private void PlayCrashSoundEffect()
	{
//		Debug.Log ("PlayCrashSoundEffect");
	}
}
