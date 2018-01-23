using UnityEngine;
using System.Collections;

public class JumpState : IState {

	private Transform _characterTransform;
	private Animator _characterAnimator;
	private MovementComponent _movement;
	private PlayerController _pController;
	private float gravity;
	private float rotationSpeed;

	public EffectBase JumpFX;
	public EffectBase JumpNewInstance;
	private float jumpForce;
	private float jumpSpeed;
	private float maxFallSpeed;

    //private float currHeight;
	private float currVert;
	private bool leftGround;

	private Vector3 startingPosition;

	public JumpState(PlayerController pController)
	{
		jumpForce = pController.JumpForce;
		jumpSpeed = pController.JumpSpeed;
		maxFallSpeed = -20.0f;
		gravity = pController.Gravity;
		rotationSpeed = pController.RotationSpeed;
		_characterTransform = pController.transform;
		_characterAnimator = pController.GetCharAnimator ();
		_movement = pController.GetMoveComponent ();
		_pController = pController;
	}
	
	public void BeginState(StateMachine stateMachine)
	{
		//currHeight = 0.0f;
		_pController.SetJumping (true);
		_pController.playJumpSFX();
		currVert = jumpForce;
		leftGround = false;
		if (_characterAnimator.GetBool ("Damage") == false) {
			_characterAnimator.SetBool ("Jump", true);
		}
		startingPosition = _characterTransform.position;
	}
	
	public void Update(StateMachine stateMachine)
	{
		//Debug.Log ("Jump");
		if(JumpFX != null && JumpNewInstance == null)
		{
			Vector3 pForward = _pController.transform.forward;
			JumpNewInstance = JumpFX.GetInstance (_pController.transform.position + (pForward * 0.1f));
			
			JumpNewInstance.transform.rotation = _pController.transform.rotation;
			JumpNewInstance.transform.Rotate(new Vector3(0,0,0));
			JumpNewInstance.PlayEffect ();
		}
		else if(JumpNewInstance != null) 
		{
			JumpNewInstance.transform.position = _pController.transform.position;
		}
		//Check if close to ground
		RaycastHit hit;
		Vector3 dir = new Vector3 (0.0f, -1.0f, 0.0f);
		if((Physics.Raycast(this._characterTransform.position, dir,out hit, 0.4f)) && (currVert <= 9.6f)){
			//Debug.Log ("Fall");
			_characterAnimator.SetBool ("Jump",false);
		}

		//Horizontal movement
		Vector3 horizontalMovement = PlayerUtils.getInputDirection();
		float moveMag = PlayerUtils.getMoveMagnitude();
		horizontalMovement.Normalize ();
		horizontalMovement *= jumpSpeed;

		//Rotate player
		if(moveMag > 0.1f)
			_characterTransform.rotation = Quaternion.RotateTowards (_characterTransform.rotation, Quaternion.LookRotation(horizontalMovement), rotationSpeed * Time.deltaTime);

		//Vertical movement
		if (!_pController.IsGrounded || !leftGround) {
			//Debug.Log ("Jumping");
			leftGround = true;

			currVert = currVert - (gravity * Time.deltaTime);
			if (currVert < maxFallSpeed){
				currVert = maxFallSpeed;
			}
			Vector3 verticalMovement = new Vector3(0.0f,currVert,0.0f);
			_movement.Move(0,(verticalMovement+horizontalMovement)*Time.deltaTime);
		}
		else if(!_characterTransform.position.Equals(startingPosition)){
			_characterAnimator.SetBool("Jump",false);
			//Debug.Log ("Grounded");
			if (moveMag > 0.05f) {
				stateMachine.SetNextState ("walk");
				return;
			}
			else{
				stateMachine.SetNextState("idle");
				return;
			}
		}
	}
	
	public void EndState(StateMachine stateMachine)
	{
		//TODO: this triggers after the player has already landed.
		//Fix the delay.

		_pController.SetJumping (false);
		_characterAnimator.SetBool ("Jump",false);

		//TODO: play sound effect (once timing is better)
		_pController.playLandSFX();
	}
}
