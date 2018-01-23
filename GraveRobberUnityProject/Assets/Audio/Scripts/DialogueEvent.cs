using UnityEngine;
using System.Collections;

public class DialogueEvent : MonoBehaviour {

	public string DialogueTitle;
	public SoundInformation DialogueRecording;
	public float DelayInSeconds = 0;

	void Start() {

	}

	public void Play() {
		if (DelayInSeconds > 0) {
			StartCoroutine (_delayedPlay (DelayInSeconds));
		} else {
			_play();
		}
	}

	private void _play() {
		if (DialogueTitle != null) {
			Debug.Log("Playing dialogue event: " + this.DialogueTitle);
		} else {
			Debug.Log("Playing dialogue event");
		}
		
		//Play the recording
		if (DialogueRecording != null && DialogueRecording.SoundFile != null) {
			SoundInstance s = DialogueRecording.CreateSoundInstance(GameObject.FindGameObjectWithTag("Player"));
			s.Play();
		} else {
			Debug.LogWarning("No SoundInformation provided for dialogue event!");
		}
		
		//TODO: show the text on screen, or update the HUD, or whatever
	}

	private IEnumerator _delayedPlay(float seconds) {
		Debug.Log ("_delayedPlay");

		for (float timer = 0; timer < seconds; timer += Time.deltaTime)
			yield return 0;

		_play();
	}
}
