using UnityEngine;
using System.Collections;

public class DialogueTrigger : DialogueEvent {

	private bool _played = false;

	void OnTriggerEnter(Collider other) {
		if (_played || other.tag != "Player") {
			return;
		}

		Play();
		_played = true;
	}

}
