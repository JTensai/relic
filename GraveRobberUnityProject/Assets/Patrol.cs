using UnityEngine;
using System.Collections;


public class Patrol : MonoBehaviour {


	// USE THIS SCRIPT ON ANY MONSTER TO MAKE THEM WANDER ABOUT

	VisionArc outerVision;
	private float timerMax = 2;
	private float timer = 0;
	private Vector3 destination;
	private MovementComponent mover;
	private float state =0;
	public float speed = 1;
	public float wanderDistance = 6;
	Vector3 origin;
	// Use this for initialization

	// Finds the visionArc the greatest Distance and uses that.
	void Start () {
		Random.seed = System.Environment.TickCount;

		origin = gameObject.transform.position;
		mover = gameObject.GetComponent<MovementComponent>();
		VisionArc temp = (VisionArc)VisionBase.GetVisionByVariant (VisionEnum.Variant2, gameObject);
		if(temp != null)
			{outerVision = temp;}

		temp = (VisionArc)VisionBase.GetVisionByVariant (VisionEnum.Variant3, gameObject);


		if(temp !=null){
			if(outerVision == null)
				{outerVision = temp;}
			else if(temp.Distance >outerVision.Distance)
			{outerVision = temp;}
		
		}

			

	}
	
	// Update is called once per frame
	void Update () {
		bool wander = true;
		if (outerVision != null) {
						foreach (GameObject obj in outerVision.PlayersInVision()) {
								if (obj.GetComponent<HealthComponent> () != null) {
										wander = false;
								}
						}
				}

		timer  += Time.deltaTime;
		if (timer > timerMax) {

					timerMax = Random.Range(2,5);
					timer = 0;
					state = Random.Range (1, 3);
			if(Vector3.Distance(origin, destination) > wanderDistance)
				{destination = origin;
				state = 1;
				}
			else
				{

				destination = new Vector3(gameObject.transform.position.x + Random.Range(-5, 5), origin.y, gameObject.transform.position.z +Random.Range(-5, 5)).normalized;
			}
			}
			
		if (wander) {
					if (state == 1) {
							this.patrol ();
						} 
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
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);


		mover.Move(0,((gameObject.transform.forward) * Time.deltaTime * speed)/2);

		}





}
