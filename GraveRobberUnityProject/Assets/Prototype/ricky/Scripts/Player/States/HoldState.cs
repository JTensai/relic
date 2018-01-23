using UnityEngine;
using System.Collections;

public class HoldState : IState
{
	
	private float gravity;
	private bool throwReady;
	public EffectBase Dust;
	public EffectBase Hold;
	public EffectBase HoldNewInstance;
	
	//Cached variabled drawn from controller, here for convinience.
	private float _walkSpeed;
	private float _holdDistance;
	private Transform _characterTransform;
	private Animator _characterAnimator;
	private MovementComponent _movement;
	private PlayerController _pController;
	private ThrowablePrefab _throwable;
//	private float throwAutoAimDegrees;
	private float throwDistance;
	private bool pivot;
	
	public HoldState (PlayerController pController)
	{
		_pController = pController;
		_walkSpeed = pController.WalkSpeed / 2;
		_holdDistance = pController.HoldDistance;
		gravity = pController.Gravity;
		_characterTransform = pController.transform;
		_characterAnimator = pController.GetCharAnimator ();
		_movement = pController.GetMoveComponent ();
		_throwable = pController._theThingThatIsPickedUp;
//		throwAutoAimDegrees = pController.ThrowAutoAimDegrees;
		throwDistance = pController.ThrowDistance;
	}
	
	public void BeginState (StateMachine stateMachine)
	{
		if (Input.GetAxisRaw ("Action") > -0.1f) {
			throwReady = true;
		}
		throwReady = false;
		pivot = false;
		_characterAnimator.SetBool ("Hold", true);
	}
	
	public void Update (StateMachine stateMachine)
	{
		//	Debug.Log ("HoldState");

		if (Hold != null && HoldNewInstance == null) {
			Vector3 pForward = _pController.transform.forward;
			Vector3 newPosition = _pController.transform.position + (pForward * 0.5f);
			HoldNewInstance = Hold.GetInstance (newPosition);
			HoldNewInstance.transform.rotation = _pController.transform.rotation;
			
			//HoldNewInstance.transform.rotation = _pController.transform.rotation;
			//HoldNewInstance.transform.Rotate(new Vector3(0,0,0));
			HoldNewInstance.PlayEffect ();
		} else if (HoldNewInstance != null) {
			Vector3 pForward = _pController.transform.forward;
			HoldNewInstance.transform.rotation = _pController.transform.rotation;
			HoldNewInstance.transform.position = _pController.transform.position + (pForward * 0.5f);
		}

		//Figure out where the player is trying to move by multiplying their "up" by up and their "Right" by right.
		Vector3 moveDirection = PlayerUtils.getInputDirection ();
	//	Debug.Log ("AmoveDirection " + moveDirection + " " + Time.frameCount);
	//	moveDirection = Vector3.MoveTowards(_pController.transform.forward, moveDirection,.2f);
		if (pivot){
			moveDirection = Vector3.RotateTowards (_pController.transform.forward, moveDirection, .05f, 0f);
		}
		else{
			moveDirection = Vector3.RotateTowards (_pController.transform.forward, moveDirection, .7f, 0f);
		}
	//	Debug.Log ("BmoveDirection " + moveDirection + " " + Time.frameCount);

		Vector3 mag = PlayerUtils.getMoveValues();

		//float x = Input.GetAxis("Horizontal");
	//	float y = Input.GetAxis("Vertical");
	//	Debug.Log ("x1 " + x);
	//	Debug.Log ("y1 " + y);

	//	Debug.Log ("x " + mag.x);
	//	Debug.Log ("z " + mag.z);

		if(Mathf.Sqrt(mag.x*mag.x+mag.z*mag.z) < .3f){
			moveDirection = Vector3.zero;
		}

		Vector3 offset = new Vector3 (0.1f, 1.2f, _holdDistance);
		Quaternion playerAngle = Quaternion.AngleAxis (this._pController.transform.eulerAngles.y, new Vector3 (0, 1, 0));

		_throwable.transform.position = this._pController.transform.position + (playerAngle * offset);
		_throwable.transform.rotation = this._pController.transform.rotation * _throwable.initialRotation;

		if (Input.GetAxisRaw ("Action") > -0.1f && !pivot) {
			throwReady = true;
		}		
		//If action button down -> throw object
		else if (Input.GetAxisRaw ("Action") < -0.1f && throwReady) {
			pivot = true;
		}
		else if(Input.GetAxisRaw ("Action") > -0.1f && throwReady && pivot){
			// get default throw location
			Vector3 throwTarget = (PlayerUtils.getInputDirection () * throwDistance) + _pController.transform.position;
			if(PlayerUtils.getInputDirection ().Equals(Vector3.zero)){
				throwTarget = _pController.transform.position + (_characterTransform.forward*throwDistance);
			}
			
			// checks for vision for autotargeting (Monsters and pillars.)
			InteractableComponent dummyObject = null;
			GameObject closestObject = _pController.getClosestTarget(_pController.getThrowingRange().ObjectsInVision(), out dummyObject);
			if (closestObject != null) {

				throwTarget = closestObject.transform.position;
			}

			// Calculate direction
			Vector3 throwDirection = throwTarget - _pController.transform.position;
			throwDirection.Normalize ();
			_throwable.Throw (throwDirection);
			_pController._theThingThatIsPickedUp = null;
			stateMachine.SetNextState ("idle");
			return;
		}

		//Movement stuff
		float currSpeed = _walkSpeed;
		if (pivot) {
			_walkSpeed = 0.0f;
		}

		bool rotateInPlace = false;
	//	Debug.Log (moveDirection.x + " " + moveDirection.z);
	//	Debug.Log (_pController.transform.forward.x + " " + _pController.transform.forward.z);
		if (Vector3.Angle (moveDirection, _pController.transform.forward) > 5f) {
			rotateInPlace = true;
	//		Debug.Log("rotateInplace" + Vector3.Angle (moveDirection, _pController.transform.forward) );
		}

		//Rotate player instantly, like bastion.
		if (moveDirection.magnitude != 0) {
			_characterTransform.rotation = Quaternion.LookRotation (moveDirection);
			
		}
		//Move Character
		moveDirection = moveDirection * currSpeed;
		_characterAnimator.SetFloat ("Speed", moveDirection.magnitude);
		//This jump is wrong, because it is a constant downward movement instead of an accelarative force! TODO: Fix.
		moveDirection.y -= gravity;

		if (!rotateInPlace) {
			_movement.Move (0, moveDirection * Time.deltaTime);
		}
	}

	public void EndState (StateMachine stateMachine)
	{
		_characterAnimator.SetBool ("Hold", false);
		if (HoldNewInstance != null)
			HoldNewInstance.StopEffectImmediately ();
	}

}
