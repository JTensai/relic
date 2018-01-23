using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MovementComponent : MonoBehaviour {
	public bool affectedByGravity = true;
	public int gravityPriority = 10;

	// Per frame momentum dampener
	public float momentumDampener = .05f;

	private float currentYVelocity = 0.0f;
	private Vector3 currentMoveAmount = Vector3.zero;
	private int currentPriority = int.MinValue;
	private Vector3 momentum = Vector3.zero;
	
	private float groundAngleTolerance = 0.0f;

	private CharacterController _controller;
	private ExtendedCharacterController _extendedController;
	private RootEntityBase _entityBase;

    private bool stunned = false;
	private float elapsedStunTime = 0f;
	private float targetStunTime = 0f;

	private Vector3 previousVelocity;

	// Use this for initialization
	void Start () {

		_extendedController = GetComponent<ExtendedCharacterController>();
		_entityBase = GetComponent<RootEntityBase>();

		if (_extendedController == null)
		{
			_controller = GetComponent<CharacterController>();
		}

		if (_controller != null)
		{
			groundAngleTolerance = Mathf.Sin(Mathf.Deg2Rad * _controller.slopeLimit);
		}
	}

	void Update(){
		if(isStunned()){
			elapsedStunTime += Time.deltaTime;
			if(elapsedStunTime >= targetStunTime){
				unstun();
				elapsedStunTime = 0f;
				targetStunTime = 0f;
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(_entityBase == null || (_entityBase != null && _entityBase.IsActivated)){

			// Dampen the current momentum and add it to the move vector.
			momentum = Vector3.MoveTowards(momentum, Vector3.zero, momentumDampener);
			currentMoveAmount += momentum;
			bool grounded = false;
			if (_extendedController != null)
			{
				grounded = _extendedController.IsGrounded;
			}
			else
			{
				grounded = _controller.isGrounded;
			}

			if(affectedByGravity && !grounded && gravityPriority >= currentPriority){
				currentMoveAmount += currentYVelocity * Vector3.up * Time.deltaTime;
				currentYVelocity += Physics.gravity.y * Time.deltaTime;
			}

			Vector3 positionBeforeMove = transform.position;
			if(!currentMoveAmount.Equals(Vector3.zero)){
				if (_extendedController != null)
				{
					_extendedController.Move(currentMoveAmount);
				}
				else
				{
					_controller.Move(currentMoveAmount);
				}
			}
			
			previousVelocity = (transform.position - positionBeforeMove) * (1.0f / Time.deltaTime);
			
			currentMoveAmount = Vector3.zero;
			currentPriority = int.MinValue;
		}

	}

	public void AddMomentum(Vector3 amount){
		this.momentum += amount;
//		Debug.Log ("momentum" + momentum);
	}

	public void Move(int priority, Vector3 amount) {
        if (!stunned)
        {
            if (_entityBase == null || (_entityBase != null && _entityBase.IsActivated))
            {
                if (priority > currentPriority)
                {
                    currentPriority = priority;
                    currentMoveAmount = Vector3.zero;
                }

                if (priority == currentPriority)
                {
                    currentMoveAmount += amount;
                }
            }
        }

	}

	public void ResetVelocity() {
		currentMoveAmount = Vector3.zero;
		currentYVelocity = 0;
	}

	private void HandleHit(Vector3 normal)
	{
		if (affectedByGravity && normal.y > groundAngleTolerance)
		{
			currentYVelocity = 0.0f;
		}
	}

	public void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (_extendedController == null)
		{
			HandleHit(hit.normal);
		}
	}
	
	public void OnExtendedControllerColliderHit(ExtendedControllerColliderHit hit)
	{
		HandleHit(hit.normal);
	}

	public bool AffectedByGravity
	{
		get
		{
			return affectedByGravity;
		}

		set
		{
			affectedByGravity = value;
			currentYVelocity = 0.0f;
		}
	}

	public bool IsGrounded
	{
		get
		{
			if (_extendedController != null)
			{
				return _extendedController.IsGrounded;
			}
			else 
			{
				return _controller.isGrounded;
			}
		}
	}

	public Vector3 Velocity
	{
		get
		{
			return previousVelocity;
		}
	}

    public void stun(float duration)
    {
        //needs to freeze animation and controller input here
        stunned = true;
		elapsedStunTime = 0f;
		targetStunTime = duration;
    }

    public void unstun()
    {
        //needs to unfreeze animation and controller input here
        stunned = false;
		elapsedStunTime = 0f;
		targetStunTime = 0f;
    }

    public bool isStunned()
    {
        return stunned;
    }
}
