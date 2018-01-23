using UnityEngine;
using System.Collections;
using FMOD.Studio;

[System.Serializable]
public class SoundInformation {
	public string SoundName;
	public FMODAsset SoundFile;

	public PLAYBACK_STATE CurrentPlaybackState{get;private set;}

	private EventInstance _soundEvent;
	private bool _loaded;
	private bool _soundFileIsThere = true;

	public void Start() {
	}

	public void Initialize(){
		if(SoundFile != null && EditorUtils.IsPro){
			_soundEvent = FMOD_StudioSystem.instance.GetEvent(SoundFile);
			_loaded = true;
		}
		else{
			_loaded = false;
			_soundFileIsThere = false;
		}
	}

	public void OnDestroy() {
		Cleanup ();
	}

	public void Cleanup(){
		if(checkIsLoaded()){
			_loaded = false;
			_soundEvent.release();
		}
	}

	public void Update(){
		if(checkIsLoaded()){
			PLAYBACK_STATE curPlayState;
			_soundEvent.getPlaybackState(out curPlayState);
			CurrentPlaybackState = curPlayState;
		}
	}
	
	private SoundInformation Clone(){
		SoundInformation retVal = new SoundInformation();
		retVal.SoundName = this.SoundName;
		retVal.SoundFile = this.SoundFile;
		return retVal;
	}

	/***********
	 * PLAYBACK
	 ***********/

	public void Play(){
		if(checkIsLoaded()){
			_soundEvent.start();
		}
	}

	public void Stop(STOP_MODE mode){
		//if(checkIsLoaded()){
			_soundEvent.stop(mode);
		//}
	}
	
	private bool checkIsLoaded(){
		if(!_soundFileIsThere){
			return false;
		}
		if(!_loaded){
			Debug.LogError("YOU ARE TRYING TO PERFORM AN OPERATION ON UNLOADED SOUND FILE " + SoundName +
			               " CALL INITIALIZE ON ALL SOUNDINFORMATIONS BEFORE USING THEM");
			return false;
		}
		return true;
	}

	/*************
	 * PARAMETERS
	 *************/

	public bool HasParameter(string paramName) {
		ParameterInstance p = null;
		if(_soundEvent != null){
			_soundEvent.getParameter(paramName, out p);
		}
		return (p != null);
	}

	public void SetParameter(string paramName, float value){
		ParameterInstance p = null;
		if (_soundEvent != null)
			_soundEvent.getParameter(paramName, out p);
		if (p != null)
			p.setValue(value);
	}

	/***************
	 * POSITIONING
	 **************/
	
	public void UpdatePosition(Vector3 position){
		UpdatePosition(position, Vector3.up);
	}
	
	public void UpdatePosition(Transform transform){
		UpdatePosition(transform.position, transform.up);
	}
	
	public void UpdatePosition(Vector3 position, Vector3 up){
		if(checkIsLoaded()){
			_3D_ATTRIBUTES _3dAttribute;
			_soundEvent.get3DAttributes(out _3dAttribute);
			_3dAttribute.position = FMOD.Studio.UnityUtil.toFMODVector(position);
			_3dAttribute.up = FMOD.Studio.UnityUtil.toFMODVector(up);
			_soundEvent.set3DAttributes(_3dAttribute);
		}
	}

	/***************************
	 * MANAGING SOUND INSTANCES
	 ***************************/

	/// <summary>
	/// Creates an instance of the sound that follows the camera
	/// </summary>
	/// <returns>The sound instance.</returns>
	public SoundInstance CreateSoundInstance(){
		SoundInstance soundInstance = createSoundInstance();

		//We don't need to follow anything.
		//ObjectFollower follow = sound.gameObject.AddComponent<ObjectFollower>();
		//follow.ToFollow = CameraManagerScript.CurrentCameraManager.transform;
		
		return soundInstance;
	}

	/// <summary>
	/// Creates an instance of the sound that follows the given object
	/// </summary>
	/// <returns>The sound instance.</returns>
	/// <param name="toFollow">To follow.</param>
	public SoundInstance CreateSoundInstance(GameObject toFollow) {
		SoundInstance soundInstance = createSoundInstance();

		ObjectFollower follow = soundInstance.gameObject.AddComponent<ObjectFollower>();
		follow.ToFollow = toFollow.transform;
		soundInstance.transform.position = toFollow.transform.position;
		return soundInstance;
	}

	private SoundInstance createSoundInstance(){
		
		SoundInformation newSound = Clone();
		newSound.Initialize();
		GameObject soundFollower = new GameObject("Sound Instance: " + newSound.SoundName);
		SoundInstance soundInstance = soundFollower.AddComponent<SoundInstance>();
		soundInstance.SourceSound = newSound;

		return soundInstance;
	}

}
