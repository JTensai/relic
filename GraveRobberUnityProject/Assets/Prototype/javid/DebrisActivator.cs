using UnityEngine;
using System.Collections;

public class DebrisActivator : MonoBehaviour {

	public BreakingPot[] breakingPots;

	public FallingGround[] fallingGrounds;

	public Pillar[] pillars;

	public float[] delays;

	//public bool active = true;

	private bool triggered = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player") && !triggered) {
			StartCoroutine(activateAll());	
			triggered = true;
		}
	}	

	public IEnumerator activateAll (){
		int i = 0;

		foreach (BreakingPot pot in breakingPots) {
			if(i<delays.Length)yield return new WaitForSeconds(delays[i]);
			pot.StartCoroutine("Break");
			i++;
		}
		foreach (Pillar pot in pillars) {
			if(i<delays.Length)yield return new WaitForSeconds(delays[i]);
			//pot.HandleOnInteract(new InteractableInteractEventData(this.gameObject,false,2f));
			pot.scriptedFall ();
			i++;
		}
		foreach (FallingGround pot in fallingGrounds) {
			if(i<delays.Length)yield return new WaitForSeconds(delays[i]);
			pot.GetComponent<TriggerDetector> ().ForceTrigger ();
			i++;
		}
	}

}
