using UnityEngine;
using System.Collections;

public class delayedRigidBody : MonoBehaviour {


	public GameObject obj;	//The object that is supporting the pillar
	public SoundInformation FallingSound;
	public SoundInformation CrashSound;
	public float TimeToFall = 1.7f;
	public float FallTime = 1f;
	public float Angle = 90f;

	private float timer;
//	private bool falling = false;
	private bool fallen = false;

	private Transform _pivotPoint;
	private float targetRotate;

	// Use this for initialization
	void Start () {
		_pivotPoint = transform.parent;
		if (_pivotPoint == null) {
				
			Debug.Log("Could not find Parent");
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (fallen) {
			//We've already fallen, so don't worry about all this stuff.
			return;
		}

		if (obj == null) {
			//There's no support - fall!

			StartCoroutine(FallCoroutine());
			Debug.Log("I should be falling");
//			falling = true;
			fallen = true;
		}
	
	}

	IEnumerator FallCoroutine(){
		playFallingSound();
		while (Mathf.Abs(targetRotate) < Mathf.Abs(Angle)){
			targetRotate += (Angle / FallTime) * Time.deltaTime;
			_pivotPoint.transform.Rotate(new Vector3(0, 0, (Angle / FallTime) * Time.deltaTime));
			yield return null;
		}
		
		playCrashSound();
		yield return null;
	}

	private void playFallingSound() {
		if (FallingSound != null && FallingSound.SoundFile != null) {
			SoundInstance s = FallingSound.CreateSoundInstance(this.gameObject);
			s.Play();
		} else {
			Debug.LogWarning("Sound file missing for relic room pillar");
		}
	}

	private void playCrashSound() {
		if (CrashSound != null && CrashSound.SoundFile != null) {
			SoundInstance s = CrashSound.CreateSoundInstance(this.gameObject);
			s.Play();
		} else {
			Debug.LogWarning("Sound file missing for relic room pillar");
		}
	}


}
