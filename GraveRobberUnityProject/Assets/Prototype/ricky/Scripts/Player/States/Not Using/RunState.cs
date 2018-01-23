/*using UnityEngine;
using System.Collections;

public class RunState : IState {

	private Transform _characterTransform;
	private Animator _characterAnimator;
	private MovementComponent _movement;
	private float gravity;
	private float runSpeed;
	private bool jumpReady;
	private float rotationSpeed;
	private float secondsToMax;
	private float runTimer;
	
	public RunState(PlayerController pController)
	{
		runSpeed = pController.RunSpeed;
		gravity = pController.Gravity;
		jumpReady = pController.JumpReady ();
		_characterTransform = pController.transform;
		_characterAnimator = pController.GetCharAnimator ();
		_movement = pController.GetMoveComponent ();
		secondsToMax = pController.SecondsToMaxRun;
		rotationSpeed = pController.RotationSpeed;
	}
	
	public void BeginState(StateMachine stateMachine)
	{
		float currSpeedPercent = _characterAnimator.GetFloat ("Speed") / runSpeed;
		if (currSpeedPercent > 1.0f)
			currSpeedPercent = 1.0f;
		runTimer = secondsToMax*currSpeedPercent;
	}
	
	public void Update(StateMachine stateMachine)
	{
		//Debug.Log ("Run");
		if (Input.GetAxisRaw ("Jump") > -1.0f) {
			jumpReady = true;
		}

		//Get Controller Input
		Vector3 moveDirection = new Vector3( Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		moveDirection.Normalize ();

		//If no longer moving
		if (moveDirection.magnitude < 0.05f) {
			stateMachine.SetNextState("idle");
			return;
		}
		//If jump button down
		else if (Input.GetAxisRaw ("Jump") < -0.1f && jumpReady && _movement.IsGrounded) {
			stateMachine.SetNextState("jump");
			return;
		}
		//If moving but not running
		else if (Input.GetAxisRaw ("Run") < 0.05f) {
			stateMachine.SetNextState("walk");
			return;
		}
		else if (Input.GetAxisRaw ("Action") < -0.1f || Input.GetAxisRaw ("Pull") < -0.1f) {
			stateMachine.SetNextState("action");
			return;
		}

		//Still running///////

		//ease into max run
		runTimer += Time.deltaTime;
		if (runTimer > secondsToMax)
			runTimer = secondsToMax;

		//Take camera angle into account
		Quaternion camAngle = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, new Vector3(0, 1, 0));
		moveDirection = camAngle * moveDirection;
		
		//Rotate player
		_characterTransform.rotation = Quaternion.RotateTowards (_characterTransform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);
		
		//Move Character
		moveDirection = moveDirection * runSpeed;
		_characterAnimator.SetFloat ("Speed", moveDirection.magnitude);
		moveDirection.y -= gravity;
		_movement.Move(0, moveDirection * Time.deltaTime);
		//_characterController.Move(moveDirection * Time.deltaTime);

		
	}
	
	public void EndState(StateMachine stateMachine)
	{
		
	}
}*/
