using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The SoundEngine class handles integration with FMOD Studio.
/// 
/// To make a sound effect, call SoundEngine.instance.playSound(soundName, soundSource, parameters).
/// You will need to pass in the name of the sound effect.
/// </summary>
public class SoundEngine : MonoBehaviour {

	/********************
	 * SINGLETON PATTERN
	 ********************/

	private static SoundEngine _instance;

	public static SoundEngine instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<SoundEngine>();
			}
			return _instance;
		}
	}

	/*************************
	 * UNITY INSTANCE METHODS
	 *************************/

	// Use this for initialization
	void Start() {
		//Debug.Log("SoundEngine initialized");

		_followingEvents = new Dictionary<FMOD.Studio.EventInstance, GameObject>();

		initializeMusic();
	}

	bool _musicForwardPressed = false;
	float _musicForwardKeyValue = 0;

	// Update is called once per frame
	void Update() {

		updateFollowingEvents();



		//Change the music when the '=' key is pressed.
		//TODO: move this code into a more appropriate controller.
		//Or just get rid of this feature...

		if (Input.GetAxisRaw("MusicForward") != _musicForwardKeyValue) {
			_musicForwardPressed = !_musicForwardPressed;
			_musicForwardKeyValue = Input.GetAxisRaw("MusicForward");
			if (_musicForwardPressed == true) {
				Debug.Log("Change the music!");
				playNextSong();
			}
		}
	}

	/**********
	 * MEMBERS
	 **********/

	private Dictionary<FMOD.Studio.EventInstance, GameObject> _followingEvents;

	private FMOD.Studio.EventInstance _prototypeMusic;
	private FMOD.Studio.ParameterInstance _currentSongParam;
	private int _currentSong = 1;
	private int _prototypeSongs = 4;

	/**********************
	 * INTERFACE FOR MUSIC
	 **********************/

	public void playFirstSong() {
		_currentSong = 1;
		_currentSongParam.setValue(_currentSong);

		//TODO: play switching sound
	}

	public void playNextSong() {
		_currentSong++;
		if (_currentSong > _prototypeSongs)
			_currentSong = 1;
		_currentSongParam.setValue(_currentSong);

		//TODO: play switching sound
	}

	public void stopMusic() {
		_prototypeMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}

	private void initializeMusic() {
		Debug.Log("initializeMusic() called...");

		_prototypeMusic = getEventByName("Prototype Music");

		//Make sure we're setting a real parameter
		if (_prototypeMusic.getParameter("Change Song", out _currentSongParam) != FMOD.RESULT.OK) {
			Debug.LogError("Change Song parameter not found on Prototype Music event");
			return;
		}

		setEventLocation(_prototypeMusic, GameObject.FindGameObjectWithTag("Player"));	//MainCamera?

		playFirstSong();
		_prototypeMusic.start();

		FMOD.Studio.PLAYBACK_STATE curState;
		_prototypeMusic.getPlaybackState(out curState);
		Debug.Log("Music playback state: " + curState.ToString());

	}

	/******************************
	 * INTERFACE FOR SOUND EFFECTS
	 ******************************/

	public void playSound(string soundName, GameObject soundSource = null, Dictionary<string, float> parameters = null, bool followSource = false) {

		Debug.Log ("SoundEngine.playSound(\"" + soundName + "\")");

		//This line works! It's too simple to use with parameters, but we can fall back on this!
		//FMOD_StudioSystem.instance.PlayOneShot("event:/Alien Noises/Alien Noise", soundSource.transform.position);
		
		FMOD.Studio.EventInstance soundEvent = getEventByName(soundName);
		if (soundEvent == null) {
			Debug.LogError("Sound event \"" + soundName + "\" not found!");
			return;
		}

		if (soundSource != null)
			setEventLocation(soundEvent, soundSource.transform.position);

		if (parameters != null)
			setEventParameters(soundEvent, parameters);

		if (followSource) {
			followEventSource(soundEvent, soundSource);
		}

		soundEvent.start();
	}

	/**************************
	 * HELPERS FOR FMOD EVENTS
	 **************************/
	
	private string getEventPath(FMOD.Studio.EventInstance soundEvent) {
		FMOD.Studio.EventDescription desc;
		soundEvent.getDescription(out desc);
		string path = "";
		desc.getPath(out path);
		return path;
	}

	private FMOD.Studio.EventInstance getEventByName(string eventName) {
		//TODO: make a config file and read from it
		//Look up PlayerPrefs and TextAsset

		//Example: "event:/Alien Noises/Alien Noise"
		string eventPath = "event:/" + eventName;

		return FMOD_StudioSystem.instance.GetEvent(eventPath);
	}

	private void setEventLocation(FMOD.Studio.EventInstance soundEvent, GameObject soundSource) {
		//See http://www.reddit.com/r/GameAudio/comments/2f5gf1/help_with_scripts_for_integrating_fmod_in_unity/

		var attributes = FMOD.Studio.UnityUtil.to3DAttributes(soundSource);
		if (soundEvent.set3DAttributes(attributes) != FMOD.RESULT.OK) {
			Debug.LogError("Error setting 3D location of sound \"" + getEventPath(soundEvent) + "\"");
		}
	}

	private void setEventLocation(FMOD.Studio.EventInstance soundEvent, Vector3 soundSource) {
		var attributes = FMOD.Studio.UnityUtil.to3DAttributes(soundSource);
		if (soundEvent.set3DAttributes(attributes) != FMOD.RESULT.OK) {
			Debug.LogError("Error setting 3D location of event \"" + getEventPath(soundEvent) + "\"");
		}
	}

	private void setEventParameters(FMOD.Studio.EventInstance soundEvent, Dictionary<string, float> parameters) {

		foreach (KeyValuePair<string, float> entry in parameters) {
			string paramName = entry.Key;
			float paramValue = entry.Value;
			FMOD.Studio.ParameterInstance parameter;

			//Get the parameter.
			if (soundEvent.getParameter(paramName, out parameter) != FMOD.RESULT.OK) {
				Debug.LogError("Parameter \"" + paramName + "\" not found on event \"" + getEventPath(soundEvent) + "\"");
				continue;
			}

			//Set the parameter value.
			if (parameter.setValue(paramValue) != FMOD.RESULT.OK) {
				Debug.LogError("Error setting parameter \"" + paramName + "\" on event \"" + getEventPath(soundEvent) + "\"");
			}
		}
	}

	/************************
	 * HELPERS FOR FOLLOWING
	 ************************/

	private void followEventSource(FMOD.Studio.EventInstance soundEvent, GameObject soundSource) {
		if (_followingEvents.ContainsKey(soundEvent)) {
			Debug.LogWarning("Changing sound source for sound event \"" + getEventPath(soundEvent) + "\"");
			_followingEvents[soundEvent] = soundSource;
		} else {
			_followingEvents.Add(soundEvent, soundSource);
		}
	}

	private void updateFollowingEvents() {
		foreach (KeyValuePair<FMOD.Studio.EventInstance, GameObject> entry in _followingEvents) {
			FMOD.Studio.EventInstance soundEvent = entry.Key;
			GameObject soundSource = entry.Value;

			FMOD.Studio.PLAYBACK_STATE playbackState;
			soundEvent.getPlaybackState(out playbackState);
			if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED) {
				//The sound has finished, so remove it from the Dictionary
				_followingEvents.Remove(soundEvent);
			} else {
				//Move the soundEvent to the new location of the object.
				setEventLocation(soundEvent, soundSource);
			}
		}
	}

} //End class SoundEngine
