using UnityEngine;
using System.Collections;

public class ToolsForceState : Triggerable {

	public enum outgoing{True,False};
	public outgoing forceAllTo;

	private bool state = false;

	void Start() {
		if ( forceAllTo == outgoing.True)
			state = true;
	}

	public override void TriggeredActions(bool active) {
		NotifyTargets(state);
	}

}
