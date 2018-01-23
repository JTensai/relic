using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public SoundInformation MenuMusic;
	public float SecondsBetweenDialogueEvents = 10.0f;
	public SoundInformation[] DialogueEvents;
	public SoundInformation LevelStartSound;

	private SoundInstance _musicSoundInstance;
	private SoundInstance _currentDialogueInstance;

	private bool _dialogueEnabled = false;
	private int _currentDialogueIndex = -1;
	private float _dialogueWaitTimer = -1f;
	
	void Start() {
		EnterMainMenu();
	}
	
	void Update() {
		if (isPlayingDialogue()) {
			//Keep doing what you're doing.
		} else if (_dialogueEnabled) {
			//Run the timer if we're in between dialogue events.
			_dialogueWaitTimer -= Time.deltaTime;
			if (_dialogueWaitTimer <= 0.0f) {
				//Debug.Log("Time to play the next dialogue event");
				PlayNextDialogue();
			}
		}
	}

	/*****************
	 * MAIN INTERFACE
	 *****************/

	public void LoadLevel(string levelName) {
		LeaveMainMenu();

		if (LevelStartSound != null && LevelStartSound.SoundFile != null) {
			//Play a sound before starting the level.
			SoundInstance sound = LevelStartSound.CreateSoundInstance ();
			sound.Play();
			Application.LoadLevel(levelName);
		} else {
			//Just load the level right away.
			Application.LoadLevel(levelName);
		}
	}
	
	public void Quit() {
		Application.Quit();
	}

	public void EnterMainMenu() {
		if (DialogueEvents.Length != 0) {
			_dialogueEnabled = true;
			_dialogueWaitTimer = SecondsBetweenDialogueEvents;
		}
		playMusic();
	}

	public void LeaveMainMenu() {
		_dialogueEnabled = false;
		stopDialogue();
		stopMusic();
	}

	/***********************
	 * CONTROLLING DIALOGUE
	 ***********************/

	public void PlayNextDialogue() {
		int nextDialogueIndex = ((_currentDialogueIndex + 1) % DialogueEvents.Length);
		playDialogue(nextDialogueIndex);
	}

	private void playDialogue(int dialogueIndex) {
		//Debug.Log("Playing dialogue event " + dialogueIndex);

		_dialogueWaitTimer = SecondsBetweenDialogueEvents;
		_currentDialogueIndex = dialogueIndex;

		SoundInformation sound = DialogueEvents[dialogueIndex];
		SoundInstance instance = sound.CreateSoundInstance();
		_currentDialogueInstance = instance;
		instance.Play();
	}

	private void stopDialogue() {
		if (_currentDialogueInstance != null) {
			_currentDialogueInstance.Stop ();
		}
	}

	private bool isPlayingDialogue() {
		return (_currentDialogueInstance != null);
	}

	/********************
	 * CONTROLLING MUSIC
	 ********************/

	private void playMusic() {
		if (_musicSoundInstance != null) {
			//We've already started the music! Don't make another copy.
			return;
		}
		
		if(MenuMusic != null && MenuMusic.SoundFile != null) {
			//We have a music file to play, so play it
			_musicSoundInstance = MenuMusic.CreateSoundInstance();
			_musicSoundInstance.Play();
		}
	}

	private void stopMusic() {
		if (_musicSoundInstance != null) {
			_musicSoundInstance.Stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		}
	}

}
