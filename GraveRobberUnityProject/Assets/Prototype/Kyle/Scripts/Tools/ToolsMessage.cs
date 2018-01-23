using UnityEngine;
using System.Collections;

public class ToolsMessage : Triggerable {

	public string message;
	public bool startOn;
	//negitive to not erase unless  from command
	public float eraseAfter;
	public bool ignoreFalse;

	private TextMesh text;

	void Start() {
		text = GetComponentInChildren<TextMesh>();
		if ( startOn ) {
			text.text = message;
			if (eraseAfter > 0) {
				Invoke("erase", eraseAfter);
			}
		}
	}

	public override void TriggeredActions(bool active) {
		//NotifyTargets(active);
		if ( active ) {
			text.text = message;
			if (eraseAfter > 0) {
				Invoke("erase", eraseAfter);
			}
		} else {
			if (!ignoreFalse)
				text.text = "";
		}


	}

	private void erase() {
		text.text = "";
	}

}
