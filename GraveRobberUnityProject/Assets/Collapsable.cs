using UnityEngine;
using System.Collections;

public class Collapsable : MonoBehaviour {

	private InteractableComponent interact;
	public GameObject supportObject;
	public float FallTime = 1f;
	public float Angle = -30f;
	
	public SoundInformation StartFallingSoundEffect;
	public SoundInformation CrashSoundEffect;

	private BoxCollider FinishedFallTrigger;
//	private bool enabled;
	private bool falling;
	private float timer;

	private Transform _pivotPoint;
	private float targetRotate;

	// Use this for initialization
	void Start () {
		if (supportObject != null) {
						interact = supportObject.GetComponent<InteractableComponent> ();
				}
		_pivotPoint = transform.parent;
//		FinishedFallTrigger = GetComponent<BoxCollider> ();
//		if (FinishedFallTrigger != null && !FinishedFallTrigger.isTrigger) 
//		{
//			FinishedFallTrigger = GetComponent<BoxCollider>();
//		}
//
//		if (FinishedFallTrigger == null)
//		{
//			FinishedFallTrigger = gameObject.AddComponent<BoxCollider> ();
//			FinishedFallTrigger.center = new Vector3 (0f, -0.7f, -3f);
//			FinishedFallTrigger.size = new Vector3 (5.28f, 0.3f, 3f);
//			FinishedFallTrigger.isTrigger = true;
//		}
		enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
//		if (falling) {
//			timer += Time.deltaTime;
//			if(timer > .9f)
//			{
//				enabled = false;
//				PlayCrashSoundEffect ();
//				Debug.Log("disabled rgidbody");
//				rigidbody.isKinematic = true;
//				falling = false;
//			}
//
//		}
//
//
//		if (interact != null){
//			if (!interact.IsInteractable) {
//				this.rigidbody.useGravity = true;	
//				PlayStartFallSoundEffect();
//				falling = true;
//
//			}
//		}

		if (falling) {

			
		}
		
		
		if (interact != null){
			if (!interact.IsInteractable && !falling) {
				PlayStartFallSoundEffect();
				StartCoroutine(FallCoroutine());
				falling = true;
			}
		}
	}

	IEnumerator FallCoroutine(){
		PlayStartFallSoundEffect();
		while (Mathf.Abs(targetRotate) < Mathf.Abs(Angle)){
			targetRotate += (Angle / FallTime) * Time.deltaTime;
			_pivotPoint.transform.Rotate(new Vector3(0, 0, (Angle / FallTime) * Time.deltaTime));
			yield return null;
		}

		PlayCrashSoundEffect();
		yield return null;
	}
	

	private void PlayStartFallSoundEffect()
	{
		Debug.Log ("Collabsible Balcony: PlayStartFallSoundEffect");
		if (StartFallingSoundEffect != null && StartFallingSoundEffect.SoundFile != null) {
			SoundInstance s = StartFallingSoundEffect.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}

	private void PlayCrashSoundEffect()
	{
		Debug.Log ("Collabsible Balcony: PlayCrashSoundEffect");
		if (CrashSoundEffect != null && CrashSoundEffect.SoundFile != null) {
			SoundInstance s = CrashSoundEffect.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}
}
