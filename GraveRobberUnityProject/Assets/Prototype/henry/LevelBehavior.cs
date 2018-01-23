using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class LevelBehavior : MonoBehaviour {
	private static LevelBehavior instance;
	public static LevelBehavior Instance{
		get{
			if (instance == null){
				GameObject findTry = GameObject.Find("LevelManager");
				if (findTry != null){
					instance = findTry.GetComponent<LevelBehavior>();
					if (!instance._isInitialized){
						instance.initialize();
					}
				} else {
					/*GameObject g = new GameObject("LevelManager");
					g.AddComponent<LevelBehavior>();
					instance = g.GetComponent<LevelBehavior>();
					instance.initialize();*/
				}
			}

			return instance;
		}
	}

	public RoomBehavior[] Rooms;
	public List<RoomBehavior> PlayerIn = new List<RoomBehavior>();

	public SoundInformation LevelMusic;
	public SoundInformation ReturnTripMusic;

	public RoomBehavior LastRoomEntered;
	public Vector3 LastPlaceEntered;
	public Vector3 RelicPickUpLocation;
	public bool ReturnTrip{get;private set;}
	private RespawnTrigger lastRespawn;

	public Deathbox deathbox;
	public FinishLevel finishlevel;

	private SoundInstance _musicSoundInstance;
	private float _musicIntensity;
    //private string _playerInString = "";
	private bool _isInitialized;

	// Use this for initialization
	void Start() {
		//PlayerIn = new List<RoomBehavior>();
		if(!_isInitialized){
			initialize();
		}
	}

	private void initialize() {
		Rooms = GameObject.FindObjectsOfType<RoomBehavior>();
		_isInitialized = true;
		initializeMusic();
	}

	// Update is called once per frame
	void Update () {
		StringBuilder build = new StringBuilder();
		foreach(RoomBehavior r in PlayerIn){
			build.Append(r.gameObject.name + ", ");
		}
        //_playerInString = build.ToString();
		
//		updateLevelSelectorKeys();

		PlayerIn.Clear();
		foreach(RoomBehavior r in Rooms){
			if(r.PlayerInRoom){
				PlayerIn.Add (r);
				break;
			}
		}

		/* //TODO: delete this if you've noticed nothing is broken without it.
		if(PlayerIn.Count > 0){
			RoomBehavior curRoom = PlayerIn[0];
			if(LastRoomEntered != curRoom){
				LastRoomEntered = curRoom;
				//TODO there's a better way to do this
				LastPlaceEntered = GameObject.FindObjectOfType<PlayerController>().transform.position;
			}
		}
		*/
	}

	private void updateLevelSelectorKeys() {
//		if (Input.GetKeyDown(KeyCode.Alpha1)) {
//			loadLevel(0);
//		}
//		else if (Input.GetKeyDown(KeyCode.Alpha2)) {
//			loadLevel(1);
//		}
//		else if (Input.GetKeyDown(KeyCode.Alpha3)) {
//			loadLevel(2);
//		}
//		else if (Input.GetKeyDown(KeyCode.Alpha4)) {
//			loadLevel(3);
//		}
//		else if (Input.GetKeyDown(KeyCode.Alpha5)) {
//			loadLevel(4);
//		}
//
//		if (Input.GetKeyDown(KeyCode.Alpha7)) {
//			stopMusic();
//		}
	}

	private void loadLevel(int levelNumber) {
		stopMusic();
		Application.LoadLevel(levelNumber);
	}

//	void OnGUI(){
	/*	GUI.Label(new Rect(0, 0, 100, 40), _playerInString);
		if (LastRoomEntered != null) {
			GUI.Label (new Rect (100, 0, 100, 40), LastRoomEntered.name);
		}
		GUI.Label(new Rect(200, 0, 100, 40), LastPlaceEntered.ToString());*/
//	}

	public void ArtifactGrabbed(){
		ReturnTrip = true;
//		Debug.Log ("Artifact Grabbed! Activate Return Trip: " + ReturnTrip);
		RelicPickUpLocation = GameObject.FindObjectOfType<PlayerController> ().gameObject.transform.position;
		deathbox = GameObject.FindObjectOfType<Deathbox> ();
		if (deathbox != null)
		{
			deathbox.Activate();
		}
		if (finishlevel == null){
			finishlevel = GameObject.FindObjectOfType<FinishLevel> ();
		}
		if (finishlevel != null){
			finishlevel.Activate ();
		}
		foreach(RoomBehavior r in Rooms){
			r.ActivateReturnTrip();
		}

		initializeReturnTripMusic();

		CameraShakeManager.CameraShakeData dat = new CameraShakeManager.CameraShakeData(new Vector3(0.1f, 0, 0), 300f, 600);
		CameraShakeManager.Shake(dat);
		//CountdownTimer.CreateTimer (100);
	}

	public void ResetPlayer(){
//		Debug.Log ("Reseting Player! ReturnTrip? " + ReturnTrip);
		if (ReturnTrip) {
			GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position = RelicPickUpLocation;
			if (deathbox != null)
			{
				deathbox.Reset();
			}
		}
		else{
			if(lastRespawn != null){
				GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position = lastRespawn.GetRespawnPosition();
				lastRespawn.PlayRespawnEffect();
			}
			else {
				GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position = new Vector3(0,0,0);
			}
		}
		//CameraManagerScript.CurrentCameraManager.SnapToPlayer();
	}

	/******************
	 * MUSIC INTERFACE
	 ******************/
	
	private void initializeMusic() {
		if (_musicSoundInstance != null) {
			//We've already started the music! Don't make another copy.
			return;
		}
		
		if(LevelMusic != null && LevelMusic.SoundFile != null) {
			//We have a music file to play, so play it
			_musicSoundInstance = LevelMusic.CreateSoundInstance();
			_musicSoundInstance.Play();
			_musicIntensity = 0.0f;
		}
	}

	private void initializeReturnTripMusic() {
		Debug.Log("Initializing return trip music");

		if(ReturnTripMusic != null && ReturnTripMusic.SoundFile != null) {

			stopMusic();	//Stop the regular world music first.

			_musicSoundInstance = ReturnTripMusic.CreateSoundInstance();
			_musicSoundInstance.Play();
			_musicIntensity = 0.0f;
		}
	}

	private void stopMusic() {
		if (_musicSoundInstance != null) {
			_musicSoundInstance.Stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		}
	}

	public void SetMusicIntensity(float intensity, float time) {
		if (_musicSoundInstance != null && _musicSoundInstance.HasParameter("Intensity")) {
			StartCoroutine(setMusicIntensityOverTime(intensity, time));
		}
	}

	private IEnumerator setMusicIntensityOverTime(float newIntensity, float time) {
		float oldIntensity = _musicIntensity;
		float scale = (newIntensity - oldIntensity) / time;

		int changes = 0;

		for (float timer = 0.0f; timer < time; timer += Time.deltaTime) {
			float currentIntensity = (scale * timer) + oldIntensity;
			_musicSoundInstance.SetParameter("Intensity", currentIntensity);
			_musicIntensity = currentIntensity;
			//Debug.Log("" + currentIntensity);
			changes++;
			yield return null;
		}
		
		_musicSoundInstance.SetParameter("Intensity", newIntensity);
		_musicIntensity = newIntensity;
		//Debug.Log("" + newIntensity + " - done in " + changes + " changes.");
	}

	public void SetRespawn(RespawnTrigger respawn){
		lastRespawn = respawn;
	}

	void OnDestroy(){
		_musicSoundInstance.Stop ();
	}

}
