using UnityEngine;
using System.Collections;

public class MusicCue : MonoBehaviour {

	public float Intensity;
	public float TransitionTime = 2.0f;
	public bool ReturnTrip = false;

	private bool _triggered;

	void Start() {
	
	}

	public void OnTriggerEnter(Collider other) {
		if (_triggered || other.tag != "Player") {
			return;
		}

		if (LevelBehavior.Instance.ReturnTrip != ReturnTrip) {
			return;
		}

		_triggered = true;
		TriggerMusicCue();
	}

	public void TriggerMusicCue() {
		Debug.Log("Triggering music cue: " + Intensity);

		LevelBehavior.Instance.SetMusicIntensity(Intensity, TransitionTime);
	}


}
