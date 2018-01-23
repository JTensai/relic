using UnityEngine;
using System.Collections;

public class LockedDoor : Triggerable {

	public bool unlocked = false;
	public enum doorType{doorOpen,doorOpenSciFi};
		public doorType openType;

	// Use this for initialization
	void Start () {
		if ( unlocked ) {
			animation[openType.ToString()].speed = 1;
			animation[openType.ToString()].time = 0;
			animation.Play(openType.ToString());
			//Depending on what de define, this might need to be enabled
			//NotifyTargets(true);
		}
	}

	private void SelfTriggeredActions(bool triggered) {
		if (unlocked == triggered)
			return;
		unlocked = triggered;
		//call delegates
		NotifyTargets(triggered);

		if (triggered) {	//on switch activating
			//play animation forward
			animation[openType.ToString()].speed = 1;
			animation[openType.ToString()].time = 0;
			animation.Play(openType.ToString());
			//play sound
			
		} else {		//on switch deactivating
			//play animation reverse
			animation[openType.ToString()].speed = -1;
			animation[openType.ToString()].time = animation[openType.ToString()].length;
			animation.Play(openType.ToString());
			//play sound
			
		}
	}


	public override void TriggeredActions(bool active) {
		SelfTriggeredActions(active);
	}


	// Update is called once per frame
	void Update () {
	
	}
}
