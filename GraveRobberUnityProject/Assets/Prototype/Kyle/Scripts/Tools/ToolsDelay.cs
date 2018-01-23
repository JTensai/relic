using UnityEngine;
using System.Collections;

public class ToolsDelay : Triggerable {

	public float timer = 1f;
	
	public override void TriggeredActions(bool active) {
		StartCoroutine("notify", active);
	}

	private IEnumerator notify( bool active ) {
		yield return new WaitForSeconds(timer);
		print ("got: " + active);
		NotifyTargets(active);
	}

}
