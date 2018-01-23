using UnityEngine;
using System.Collections;

public class ToolsAND : Triggerable {

	public int requires = 2;

	private int on = 0;

	public override void TriggeredActions(bool active) {
		if (active) {
			on++;
		} else {
			on--;
		}
		if (on == requires )
			NotifyTargets(true);
	}

}
