using UnityEngine;
using System.Collections;

public class BlindMonster : PathingMonsterBase {
	public Spotter spotter;
	public float attackSpeed = 5;
	public float idleSpeed = 1;
	public float rotationSpeed = 0.5f;
	public float turnRate = 360;
	//private CharacterController controller;
	private MovementComponent mover;
	private GameObject lastPlayerPosition;
	private Vector3 idleTargetDir; 
	private Vector3 idleCurrentDir; 

	// Use this for initialization
	new void Start () {
		base.Awake ();
		//controller = gameObject.GetComponent<CharacterController>();
		idleTargetDir = GetNewTargetIdleDir();
		idleCurrentDir = new Vector3(1f, 0f, 0f); // should probably be initialized as a normal vector given how we use it in Idle()
		mover = gameObject.GetComponent<MovementComponent>();
		lastPlayerPosition = new GameObject();
		base.Start ();
	}

	// Get a new random direction for the monster to idle toward for a while
	private Vector3 GetNewTargetIdleDir() {
		Vector3 targetDir = new Vector3(0f, 0f, 0f);
		Vector2 temp = Random.insideUnitCircle;
		targetDir.x = temp.x;
		targetDir.z = temp.y;
		targetDir.Normalize();
		return targetDir;
	}
	
	// Update is called once per frame
	new void Update () {
		Transform player_pos = spotter.GetPlayerTransform();
		if(player_pos != null) {// player is in view of the spotter.
			lastPlayerPosition.transform.position = player_pos.position;
			this.target = lastPlayerPosition.transform;
		}
		else if (this.target == null) { // no clue where the player is or was.
			Idle();
			return;
		}
		Vector3 vecToTarget = lastPlayerPosition.transform.position - gameObject.transform.position;
		if (vecToTarget.magnitude < 1f) {
			this.target = null;
		} else {
			PathingMove(attackSpeed);
			//SimpleMove((Vector3)vecToTarget.normalized * attackSpeed * Time.deltaTime);
		}
		/* // waiting until pathfinding is re-implemented.
		FindPath(transform.position, lastPlayerPosition);
		if(Path.Count > 0){
			NewMove ();
		}
		else {
			lastPlayerPosition = cantSeePlayer;
		}
		*/

		AttemptAttack();

	}

	void PathingMove(float speedFactor) {
		// Inherited magical pathfinding voodoo
		this.speed = speedFactor;
		Vector3 dir = CalculateVelocity(GetFeetPosition());
		RotateTowards(targetDirection);
		dir.y = 0;
		mover.Move(0, dir * Time.deltaTime);
	}

	// Used in lieu of Pathfinding
	void SimpleMove(Vector3 pos) {
		pos.y = 0;
		mover.Move (0, pos);
		gameObject.transform.rotation = Quaternion.LookRotation (pos, Vector3.up);
	}

	//A test move function, can easily be replaced
	/* Disabled until pathfinding works
	public void NewMove()
	{
		if (Path.Count > 0)
		{
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(Path[0].x, transform.position.y, Path[0].z), Time.deltaTime*attackSpeed);// * 30F);
			if (Vector3.Distance(transform.position, Path[0]) < 0.4F)
			{
				Path.RemoveAt(0);
			}
		}
	}*/

	void AttemptAttack() {
		VisionArc attackVision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);
		GameObject[] playersInVision = attackVision.PlayersInVision();
		foreach(GameObject g in playersInVision){
			AttackBase attack = AttackBase.GetAttackByVariant(AttackEnum.Default, this.gameObject);
			attack.Attack(g.transform);
		}
	}

	public void Idle() {
		idleCurrentDir = Vector3.MoveTowards(idleCurrentDir, idleTargetDir, Time.deltaTime * rotationSpeed);
		// if the two vectors are almost pointing the same direction, the dot product of two identical normal vectors is 1
		if(Vector3.Dot(idleCurrentDir, idleTargetDir) >= 0.9f) { 
			idleTargetDir = GetNewTargetIdleDir();
		}
		/*
		if(Vector3.Cross (transform.rotation, idleTargetDir).y > 0) {
			this.transform.eulerAngles.y += 1;
		}
		else if(Vector3.Cross (transform.rotation, idleTargetDir).y < 0) {
			this.transform.eulerAngles.y -= 1;
		}
*/
		//CharacterController charCont = gameObject.GetComponent<CharacterController>(); 
		//charCont.Move(idleCurrentDir * Time.deltaTime*idleSpeed);// * 30F);
		mover.Move(0, idleCurrentDir * Time.deltaTime * idleSpeed);
		RotateTowards(idleCurrentDir);
		//gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.LookRotation(idleCurrentDir), turnRate * Time.deltaTime);
		
		AttemptAttack();
	}

	public void OnDrawGizmos() {
		Debug.DrawLine (transform.position, spotter.transform.position);
	}
}
