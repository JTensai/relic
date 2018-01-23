using UnityEngine;
using System.Collections;

public class ToolsADDER : Triggerable {

	public int requires = 2;

	private int on = 0;

	public override void TriggeredActions(bool active) {
		if (active) {
			on++;
		if (on == requires )
			NotifyTargets(true);
		}
	}

}
