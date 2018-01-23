using UnityEngine;
using System.Collections;

public class ToolsEnableObjects: Triggerable {

	public GameObject[] ToEnable;

	public override void TriggeredActions(bool active) {
		NotifyTargets(active);
		foreach (GameObject g in ToEnable) {
			g.SetActive(active);
		}
	}


	void OnDrawGizmosSelected() {

		foreach (GameObject obj in ToEnable) {
			if (obj) {
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(transform.position, obj.transform.position );
			}
		}

		foreach (Triggerable trig in targets) {
			if (trig) {
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(transform.position, trig.transform.position );
			}
		}
	}
}
