using UnityEngine;
using System.Collections;

public abstract class ProjectileBase : MonoBehaviour {
	public enum TargetType{None, Follow, Position}
	public TargetType CurrentTargetType;
	public AnimationCurve XMovement;
	public AnimationCurve YMovement;
	public AnimationCurve ZMovement;
	public float Velocity;
	public float DefaultDistance = 10f;

	public Transform FollowTarget{get;set;}
	public Vector3 PositionTarget{get;set;}

	private Vector3 _startPosition;
	private float _initialDistance;
	private float _currentDistance;
	private float _totalTravelledDistance;
	private Quaternion _initialForward;
	private float _elapsedTime;
	// Use this for initialization
	public virtual void Start () {

	}

	public void initialize(){
		_startPosition = transform.position;
		_initialForward = this.transform.rotation;
		if(CurrentTargetType == TargetType.Follow){
			_initialDistance = Vector3.Distance(_startPosition, FollowTarget.transform.position);
			_initialForward = Quaternion.LookRotation(FollowTarget.transform.position - this.transform.position);
		}
		else if(CurrentTargetType == TargetType.Position){
			_initialDistance = Vector3.Distance(_startPosition, PositionTarget);
			_initialForward = Quaternion.LookRotation(PositionTarget - this.transform.position);
		}
		else if(CurrentTargetType == TargetType.None || (FollowTarget == null && PositionTarget == Vector3.zero)){
			_initialDistance = DefaultDistance;
		}
		_currentDistance = _initialDistance;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if(CurrentTargetType == TargetType.Follow && FollowTarget != null){
			_initialForward = Quaternion.LookRotation(FollowTarget.transform.position - this.transform.position, Vector3.up);
			_currentDistance = Vector3.Distance(_startPosition, FollowTarget.transform.position);
		}
		float toTravel = Velocity * Time.deltaTime;
		_totalTravelledDistance += toTravel;
		float percentage = _totalTravelledDistance / _currentDistance;

		float xPos = _currentDistance * XMovement.Evaluate(percentage);
		float yPos = _currentDistance * YMovement.Evaluate(percentage);
		float zPos = _currentDistance * ZMovement.Evaluate(percentage);

		Vector3 moveDelta = new Vector3(xPos, yPos, zPos);
		moveDelta = _initialForward * moveDelta;
		moveDelta += _startPosition;

		this.rigidbody.MovePosition(moveDelta);

		if(_totalTravelledDistance > _currentDistance){
			GameObject.Destroy (gameObject);
		}
		//moveDelta = moveDelta - transform.position;
		//_move.Move(0, deltaMove);

	}

	void OnTriggerEnter(Collider col){
		HandleProjectileCollision(col);
	}

	void OnCollisionEnter(Collision col){
		HandleProjectileCollision(col.collider);
	}

	public abstract void HandleProjectileCollision(Collider col);
}
