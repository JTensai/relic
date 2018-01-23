using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuManager : MonoBehaviour {
	public UITexture backgroundVideoTexture;
	public UISprite loadingButton;
	public GameObject defaultButton;

	public SoundInformation ButtonClick;
	public SoundInformation MenuMusic;

	private SoundInstance _musicSound;

	// Use this for initialization
	void Start () {
		if(loadingButton != null) {
			loadingButton.alpha = 0f;
		}
		((MovieTexture)backgroundVideoTexture.material.mainTexture).loop = true;
		((MovieTexture)backgroundVideoTexture.material.mainTexture).Play ();
		ButtonClick.Initialize ();
		PlayMusic ();
	}
	
	// Update is called once per frame
	void Update () {
		if (UICamera.selectedObject == null) {
			UICamera.selectedObject = defaultButton;
			defaultButton.GetComponent<UIButton>().SendMessage("OnHover", true);
		}
	}

	public void StartGame() {
		if(loadingButton != null) {
			loadingButton.alpha = 1f;
		}
		PlayButtonClick ();
		Application.LoadLevel("SmallCompilation");
		StopMusic ();
	}
	
	public void Credits() {
		PlayButtonClick ();
		Application.LoadLevel("Credits");
		StopMusic ();
	}
	
	public void Quit() {
		if (Application.isEditor){
#if UNITY_EDITOR
			PlayButtonClick ();
			EditorApplication.isPlaying = false;
#endif
		}
		else{
			PlayButtonClick ();
			//No special controller, so just quit now.
			Application.Quit();
		}
	}

	public void PlayButtonClick(){
		ButtonClick.CreateSoundInstance ().Play ();
	}

	public void PlayMusic(){
		if (MenuMusic.SoundFile != null) {
			MenuMusic.Initialize();
			_musicSound = MenuMusic.CreateSoundInstance();
			_musicSound.SetParameter("Intensity", 0.3f);
			_musicSound.Play();
		}
	}

	public void StopMusic(){
		_musicSound.Stop ();
	}
}
