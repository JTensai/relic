using UnityEngine;
using System.Collections;

public class ToolsIF : Triggerable {

	public Triggerable[] targetsOnFalse;
	
	
	private void NotifyTargetsOnFalse(bool active) {
		foreach (Triggerable tar in targetsOnFalse) {
			tar.TriggeredActions(active);
		}
	}
	
	public override void TriggeredActions(bool active) {
		if (active) {
			NotifyTargets(true);
		} else {
			NotifyTargetsOnFalse(true);
		}

	}

	
	
	void OnDrawGizmosSelected() {
		foreach (Triggerable trig in targets) {
			if (trig) {
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(transform.position, trig.transform.position );
			}
		}
		foreach (Triggerable trig in targetsOnFalse) {
			if (trig) {
				Gizmos.color = Color.red;
				Gizmos.DrawLine(transform.position, trig.transform.position );
			}
		}
	}
}
