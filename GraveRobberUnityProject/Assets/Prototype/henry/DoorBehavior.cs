using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour {
	public SoundInformation DoorOpenedSound;

	private GameObject _door;
	private GameObject _doorFrame;
	[ReadOnlyAttribute]
	public bool DoorStatus;
	public bool DoorOpened{
		get{return DoorStatus;}
		private set {DoorStatus = value;}
	}
	private bool _doorRequestedPosition;
	private bool _doorChangeRequested;
	// Use this for initialization
	void Start () {
		_door = transform.FindChild("DoorObject").gameObject;
		_doorFrame = transform.FindChild("DoorFrameObject").gameObject;
		DoorOpened = false;
		_doorRequestedPosition = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(_doorChangeRequested){
			_doorChangeRequested = false;
			if(_doorRequestedPosition){
				_door.gameObject.SetActive(false);
				_doorFrame.gameObject.SetActive(false);
			}
			else{
				_door.gameObject.SetActive(true);
				_doorFrame.gameObject.SetActive(true);
			}
		}
	}

	public void OpenDoors(){
		OpenDoors(false);
	}

	public void OpenDoors(bool PlaySound){
		if(!DoorOpened){
			_doorRequestedPosition = true;
			_doorChangeRequested = true;
			
			if(DoorOpenedSound != null && DoorOpenedSound.SoundFile != null && PlaySound){
				DoorOpenedSound.CreateSoundInstance(gameObject).Play();
			}
		}
	}

	public void CloseDoors(){
		_doorRequestedPosition = false;
		_doorChangeRequested = true;
	}
}
