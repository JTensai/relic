using UnityEngine;
using System.Collections;

public class PlatformMover : Triggerable {

	public float time = 1f;
	public bool startOn = false;
	public bool cycle = false;



	private Vector3 startingPos;
	private Vector3 targetPos;

	// Use this for initialization
	void Start () {
		startingPos = transform.position;
		targetPos = transform.GetChild(0).position;
		if (startOn)
			goTo();
	}

	//called when this is the target of something that was triggered
	public override void TriggeredActions(bool t) {
			TriggeredActions();
	}
	public override void TriggeredActions() {
		goTo();
	}

	void goTo() {

		iTween.MoveTo(gameObject,iTween.Hash(
			"position"   , targetPos,
			"time", time,
			"easetype", "linear"
			));
		if (cycle)
			Invoke("goBack", time+1 );
	}
	void goBack() {
		iTween.MoveTo(gameObject,iTween.Hash(
			"position"   , startingPos,
			"time", time,
			"easetype", "linear"
			));
		if (cycle)
			Invoke("goTo", time+1 );
	}
}
