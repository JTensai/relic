using UnityEngine;
using System.Collections;

public class LevelRestart : Triggerable {

	public string levelToLoad;

	private void SelfTriggeredActions() {
		Application.LoadLevel (levelToLoad);
		
	}
	
	//called when this is the target of something that was triggered
	public override void TriggeredActions(bool t) {
		TriggeredActions();
	}
	public override void TriggeredActions() {
		SelfTriggeredActions();
	}
}
