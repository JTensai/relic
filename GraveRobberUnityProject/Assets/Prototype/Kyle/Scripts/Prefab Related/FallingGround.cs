using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(TriggerDetector))]
public class FallingGround : Triggerable {
	//how many times can this be 'activated' before it triggers
	public float steps = 1;
	private float stepsCnt;
	//who can 'activate' this
	public enum triggeringActors{Player, Enemy, Both, None};
	public triggeringActors triggeredBy;
	//what method 'activates' this
	public enum activationMethods{OnEnter, OnExit, OnStay, OnEnterStartFall};
	public activationMethods on;
	//how many seconds does one have to stay in bounds to activate
	//only used if activationMethods on == OnStay
	public float secondsPerStep = 1;
	private float secondCnt;
	//seconds until the block returns, 0 = never
	public float respawnTime = 3;

	private bool Triggered = false;

//	private GameObject switchObj;
	private TriggerDetector detectorScript;
	public SoundInformation CrumblingSoundEffect;

	private Vector3 startingPos;
	private bool falling = false;

	private GraphUpdateScene gus;

	// Use this for initialization
	void Start () {
		stepsCnt = steps;
		startingPos = transform.position;
		detectorScript = GetComponent<TriggerDetector>();
		detectorScript.PlayerTriggered =false;
		detectorScript.EnemyTriggered = false;
		if (triggeredBy == triggeringActors.Player || triggeredBy == triggeringActors.Both) {
			detectorScript.PlayerTriggered = true;
		}
		if (triggeredBy == triggeringActors.Enemy || triggeredBy == triggeringActors.Both) {
			detectorScript.EnemyTriggered = true;
		}
		secondCnt = secondsPerStep;
		gus = GetComponent <GraphUpdateScene>();
	}

	private void SelfTriggeredActions() {
	
		if (Triggered)
			return;
		//play animation
		Vector3 targetPos = startingPos;
		targetPos.y = targetPos.y - 0.4f + 0.4f * stepsCnt / steps;
		transform.position = targetPos;
		
		//fall and remove
		if( stepsCnt <= 0 || secondCnt <= 0 ) {
			Triggered = true;
			NotifyTargets(true);
			targetPos.y -= 16;
			iTween.MoveTo(gameObject, targetPos, 8f);
			Invoke("rescan",2f);
			if (respawnTime == 0f){
				//Invoke("rescan",2f);
				Destroy (gameObject,4f);
			}
			else{
				this.collider.enabled = false;
				this.renderer.enabled = false;
			}
			Invoke("delay", 1f);
			stepsCnt = steps;
			secondCnt = secondsPerStep;
			falling = false;
		}

	}

	private void rescan(){
		gus.Apply();
	}

	private void delay() {
		setSelf (false);
		if (respawnTime > 0)
			StartCoroutine (blink());
	}

	private void setSelf(bool state) {
		GetComponent<TriggerDetector>().enabled = state;
		renderer.enabled = state;
		Collider[] temp = GetComponents<Collider>();
		foreach (Collider t in temp) {
			t.enabled = state;
		}
		gus.Apply ();
	}

	private IEnumerator  blink() {
		yield return new WaitForSeconds(respawnTime);		
		this.collider.enabled = false;
		this.renderer.enabled = false;
		transform.position = startingPos;
		for (int i = 5; i < 10; i++) {
			setSelf (true);
			yield return new WaitForSeconds(0.5f/i);
			setSelf (false);
			yield return new WaitForSeconds(0.5f/i);
		}
		setSelf (true);
		Triggered = false;
	}

	//called when this is the target of something that was triggered
	public override void TriggeredActions(bool t) {
			TriggeredActions();
	}
	public override void TriggeredActions() {
		stepsCnt = 0;
		SelfTriggeredActions();
	}


	// Update is called once per frame
	void Update () {
		if (falling) {
		
			if(secondCnt > 0){			
				secondCnt -= Time.deltaTime/0.9f;
				SelfTriggeredActions();
			}		
		}
		else{
			//when entering tile
			if ( detectorScript.TriggeredOn() && on == activationMethods.OnEnter) {
				falling = true;
				stepsCnt -= 1;
				SelfTriggeredActions();
				playSoundEffect();	//TODO: We might want this to be somewhere else
			}
			//when exiting tile
			if ( detectorScript.TriggeredOff() && on == activationMethods.OnExit ) {
				stepsCnt -= 1;
				SelfTriggeredActions();
			}
			//when on tile
			if ( detectorScript.ThingInBounds() && on == activationMethods.OnStay ) {
				if (secondCnt == secondsPerStep){
					stepsCnt -= 1;
					SelfTriggeredActions();
				}
				else{
					secondCnt -= Time.deltaTime;
					if (secondCnt <= 0) {
						secondCnt = secondsPerStep;
						stepsCnt -= 1;
						SelfTriggeredActions();
					}
				}
			}		
			if ( detectorScript.TriggeredOn() && on == activationMethods.OnEnterStartFall) {
				falling = true;
				stepsCnt -= 1;
				SelfTriggeredActions();
			}
		}
	}

	private void playSoundEffect() {
		Debug.Log ("Play falling ground SFX");
		if (CrumblingSoundEffect.SoundFile != null){
			SoundInstance s = CrumblingSoundEffect.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}



}
