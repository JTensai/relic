using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameHUDMenu : MonoBehaviour {
	public SoundInformation ButtonClick;
	private GameHUDManager GameHUD = null;

	// Use this for initialization
	void Start () {
		GameHUD = this.gameObject.GetComponentInParent<GameHUDManager>();
		setVolume (0.5f);
		ButtonClick.Initialize ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void MainMenu()
	{
		// Load Main Menu
		// Application.loa
		Application.LoadLevel ("MainMenu");
	}

	public void PlayAgain()
	{
		PlayButtonClick ();
		GameHUD.stats.ResetStats ();
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void Resume()
	{
		PlayButtonClick ();
		GameHUD.ResumeGame ();
	}

	public void MissionObjective()
	{
		GameHUD.ShowMissionObjective ();
	}

	public void SoundOptions()
	{
		GameHUD.ShowSoundOptions ();
	}

	public void HideSoundOptions()
	{
		GameHUD.HideSoundOptions ();
	}

	public void UpdateSoundLevel()
	{
		if (GameHUD.volumeSlider != null)
			setVolume(GameHUD.volumeSlider.value);
		else
			setVolume(0.5f);
	}

	public void ToggleSound()
	{
		if (GameHUD.soundEnabled)
		{
			GameHUD.soundEnabled = false;
			GameHUD.volumeSlider.alpha = 0.4f;
			GameHUD.volumeSlider.gameObject.collider.enabled = false;
			setVolume(0f);
		}
		else{
			GameHUD.soundEnabled = true;
			GameHUD.volumeSlider.alpha = 1f;
			GameHUD.volumeSlider.gameObject.collider.enabled = true;
			UpdateSoundLevel();
		}
	}

	private void setVolume(float volume)
	{
		FMOD.Studio.Bus masterBus;
		FMOD.Studio.System system = FMOD_StudioSystem.instance.System;
		if (system != null){
			system.getBus("bus:/", out masterBus);
			masterBus.setFaderLevel(volume);
		}
	}

	public void HighScores()
	{
		GameHUD.ShowHighScores ();
	}

	public void HideHighScores()
	{
		GameHUD.HideHighScores ();
	}

	public void Options()
	{
		Debug.Log ("Options Button is unimplemented");
	}

	public void Quit()
	{Debug.Log ("HIELL");
		if (Application.isEditor){
#if UNITY_EDITOR
			PlayButtonClick();
			EditorApplication.isPlaying = false;
#endif
		}
		else{
			//No special controller, so just quit now.
			PlayButtonClick();
			Application.Quit();
		}		
	}

	public void PlayButtonClick(){
		ButtonClick.CreateSoundInstance ().Play ();
	}
}
