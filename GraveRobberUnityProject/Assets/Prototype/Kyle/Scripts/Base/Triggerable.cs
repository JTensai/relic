using UnityEngine;
using System.Collections;

public class Triggerable : EnvironmentBase {

	public Triggerable[] targets;


	protected void NotifyTargets(bool active) {
		foreach (Triggerable tar in targets) {
			tar.TriggeredActions(active);
		}
	}

	public virtual void TriggeredActions(bool active) {
		NotifyTargets(active);
		//print ("No TriggeredActions set for " + name + "  state - " + active);
	}
	public virtual void TriggeredActions() {
		TriggeredActions(true);
	}


	void OnDrawGizmosSelected() {
		foreach (Triggerable trig in targets) {
			if (trig) {
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(transform.position, trig.transform.position );
			}
		}
	}

}
