using UnityEngine;
using System.Collections;

public class ThrowerScript : MonoBehaviour {

	public GameObject Projectile;
	private VisionArc RangeVision;
	private VisionArc SightVision;
	private VisionArc runVision;
	public MovementComponent mover;

 	public float bombSpeed = 3;				//This determines how fast the bombs fly
	private float maxAttackDelay = 3;  // change this to change the rate at which the bombs are thrown
	private float attackDelayTimer = 0;
	private GameObject attackTarget;
	

	public float speed = 2;     // Change this to change his movement speed
	
	
	void Start () {

		RangeVision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);
		SightVision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant2, gameObject);
		runVision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant3, gameObject);

		attackDelayTimer = maxAttackDelay;
		mover = gameObject.GetComponent<MovementComponent>();
	}


	// Update is called once per frame
	void Update () {
		
		attackDelayTimer += Time.deltaTime;


	// MOVE TOWARDS PLAYER IF THEY ARE IN OUTER SIGHT RANGE
		foreach (GameObject obj in SightVision.PlayersInVision()) 
		{
			bool approach= true;
			foreach (GameObject check in RangeVision.PlayersInVision())
			{
				if (check.GetComponent<PlayerBehavior> () != null || check.GetComponent<HealthComponent> () != null)
				{
					Vector3 destination = (obj.transform.position - transform.position).normalized;

					Quaternion targetRotation  = Quaternion.LookRotation(destination); 

					targetRotation.z=0; 

					targetRotation.x=0; 
					transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*6);


					approach = false;
				}
			}


			if(approach)
			{
				if (obj.GetComponent<PlayerBehavior> () != null || obj.GetComponent<HealthComponent> () != null)
				{
					mover.Move(0, (obj.transform.position - gameObject.transform.position).normalized * Time.deltaTime * speed); 
					return;
				}
			}
		}


		foreach (GameObject ob in runVision.PlayersInVision())
		{
			if (ob.GetComponent<PlayerBehavior> () != null || ob.GetComponent<HealthComponent> () != null)
			{
				mover.Move(0, (gameObject.transform.position - ob.transform.position ).normalized * Time.deltaTime * speed);
			}
			return;
		}

		// THROW BOMB AT PLAYER IN INNER RANGE
		foreach (GameObject obj in RangeVision.PlayersInVision())
		{
			if (obj.GetComponent<HealthComponent> () != null)
			{
				if (attackDelayTimer >= maxAttackDelay)
				{
					attackDelayTimer = 0;
					Vector3 spawnLoc = new Vector3 (this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
					GameObject instanceRock = (GameObject)Instantiate (Projectile, spawnLoc, Quaternion.identity);

					Vector3 playerLocation  = obj.transform.position;

					Vector3 throwingDirection = playerLocation - instanceRock.transform.position;
					throwingDirection.y+=(Vector3.Distance(playerLocation, instanceRock.transform.position))/4;
					instanceRock.rigidbody.AddForce(throwingDirection.normalized * bombSpeed * 200);
				}
				return;
			}
		}		
	}

}
