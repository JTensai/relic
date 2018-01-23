using UnityEngine;
using System.Collections;

public class TurretBase : MonoBehaviour {

	private CannonMovement cannonMovement;
	private FireRoutine fireRoutine;

	// Use this for initialization
	void Start () {
		cannonMovement = GetComponent<CannonMovement> ();
		fireRoutine = GetComponent<FireRoutine> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(cannonMovement.canFire) {
			fireRoutine.canFire = true;
		}
	}
}
