using UnityEngine;
using System.Collections;

public class Swamper : MonsterBase {

	private VisionArc outerVision;
	private MovementComponent mover;
	Vector3 origin;

	private Vector3 destination;
	private float wanderTimer;
	private float timerMax = 2;
	private float wanderRange = 2;

	private float grabTimer;

	public float grabDelay = 1f;
	private float grabDelayTimer = 0;

	public float grabCooldown = 6;
	private float grabCooldownTimer = 0;
	private GameObject target; 
	private Vector3 targetInitPos;

	public float grabDuration = 0.5f;
	public AnimationCurve grabCurve;

	private ParticleSystem ps;
	private bool grabbing = false;
	private bool tryingToGrab = false;


	// Use this for initialization
	void Start () {
		outerVision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);
		mover = gameObject.GetComponent<MovementComponent>();
		origin = this.gameObject.transform.position;
		ps = gameObject.GetComponent<ParticleSystem> ();
		ps.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		if (IsActivated) {
			if (grabbing) {
				grabCooldownTimer = grabCooldown; // start cooldown on ability use
				spinAttack ();	
			}
			else{
				grabCooldownTimer -= Time.deltaTime;
				
				// attack is ready
				if (grabCooldownTimer < 0) {
					// acquire a target
					if (target == null){
						foreach (GameObject obj in outerVision.ObjectsInVision()) {
							if (obj.GetComponent<HealthComponent> () != null) {
								this.transform.LookAt (obj.transform.position);
								target = obj;
								targetInitPos = target.transform.position;
								tryingToGrab = true;
								grabTimer = 0;
								grabDelayTimer = grabDelay;
								ps.Play();
								break; // just needs one target
							}
						}
					}
					// has a target
					else{
						//wait for the delay while constantly looking at target, when delay is done, start grabbing
						if (grabDelayTimer > 0){
							grabDelayTimer -= Time.deltaTime;
							this.transform.LookAt (target.transform.position);
							Debug.Log("Lurker has acquired a target: " + grabDelayTimer);
						}
						else{		
							// check to see if target is still in sight range
							bool targetLocked = false;
							foreach (GameObject obj in outerVision.ObjectsInVision()) {
								if (obj == target) {
									targetLocked = true;
									break;
								}
							}
							if (targetLocked){
								// target is still in range, grab it
								ps.Stop();
								ps.Clear();
								grabbing = true;
							}
							else{
								// target escaped, reset
								tryingToGrab = false;
								grabDelayTimer = grabDelay;
								target = null;
								ps.Stop();
								ps.Clear();
							}
						}
					}
				}			



				// wandering (not attacking and attack is on cooldown)
				if (!tryingToGrab){
					wanderTimer += Time.deltaTime;
					if (wanderTimer > timerMax) {
						timerMax = Random.Range (3, 6);
						wanderTimer = 0;
						destination = new Vector3 (origin.x + Random.Range (-wanderRange, wanderRange), origin.y, origin.z + Random.Range (-wanderRange, wanderRange));
					}
					patrol ();
				}
			}
		}	
	}


	void spinAttack(){
		// Start spinning to notify that it pulled something, pull then clear target
		// probably end up doing somekind of Lerp movement here after we have the animation for the grab
		grabTimer += Time.deltaTime;
		this.transform.Rotate(Vector3.up, 500 * Time.deltaTime);
		if (target != null){
//			target.GetComponent<MovementComponent>().Move (0, (this.gameObject.transform.position - target.gameObject.transform.position) * speed);
			float delta = grabCurve.Evaluate(grabTimer / grabDuration);
			target.transform.position = Vector3.Lerp(targetInitPos, this.gameObject.transform.position, delta);
		}
		if (target.transform.position == this.gameObject.transform.position) {
			// target has been fully pulled in
//			Debug.Log ("Grab complete");
			target = null;	
			grabbing = false;
			tryingToGrab = false;
		}
	}

	void patrol()
	{
		
		Vector3 targetPosition=destination; 
		
		// calculate rotation to be done
		Quaternion targetRotation  = Quaternion.LookRotation(targetPosition - transform.position); 
		
		//NOTE :: If you don't want rotation along any axis you can set it to zero is as :-
		// Setting Rotation along z axis to zero
		targetRotation.z=0; 
		
		// Setting Rotation along x axis to zero
		targetRotation.x=0; 
		
		// Apply rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime/2);
		
		
		mover.Move(0,((gameObject.transform.forward) * Time.deltaTime ));
		
	}

}
