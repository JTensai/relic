using UnityEngine;
using System.Collections;
using FMOD.Studio;

/// <summary>
/// Should not be used as a component!  These should only be created by a SoundInformation
/// </summary>
public class SoundInstance : MonoBehaviour {
	public SoundInformation SourceSound{get;set;}
	public PLAYBACK_STATE State;

	private PLAYBACK_STATE _lastPlaybackState = PLAYBACK_STATE.STOPPED;

	// Use this for initialization
	void Start() {
		this.OnPlayStateChanged += HandleOnPlayStateChanged;
	}

	void HandleOnPlayStateChanged (PLAYBACK_STATE state)
	{
		if(state == PLAYBACK_STATE.STOPPED){
			GameObject.Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update() {
		if (SourceSound != null) {
			SourceSound.UpdatePosition (this.transform);
			SourceSound.Update ();

			if (_lastPlaybackState != SourceSound.CurrentPlaybackState) {
					if (OnPlayStateChanged != null) {
							OnPlayStateChanged (SourceSound.CurrentPlaybackState);
					}
			}
			_lastPlaybackState = SourceSound.CurrentPlaybackState;
			State = _lastPlaybackState;
		}
	}

	void OnDestroy (){
		if (SourceSound != null) {
			SourceSound.Cleanup ();
		}
	}

	/***********
	 * PLAYBACK
	 ***********/
	
	public delegate void PlayStateChanged(PLAYBACK_STATE state);
	public event PlayStateChanged OnPlayStateChanged;

	public void Play(){
		SourceSound.Play();
	}

	public void Stop() {
		Stop (STOP_MODE.ALLOWFADEOUT);
	}

	public void Stop(STOP_MODE mode){
		SourceSound.Stop(mode);
	}

	/*************
	 * PARAMETERS
	 *************/

	public bool HasParameter(string paramName) {
		return SourceSound.HasParameter(paramName);
	}

	public void SetParameter(string paramName, float value){
		SourceSound.SetParameter(paramName, value);
	}

}
