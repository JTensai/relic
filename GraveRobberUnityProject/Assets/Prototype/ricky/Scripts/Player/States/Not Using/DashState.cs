/*using UnityEngine;
using System.Collections;

public class DashState : IState {
	
	private CharacterController _characterController;
	private Transform _characterTransform;
	private Animator _characterAnimator;
	private float gravity;
	private float dashSpeed;
	private float dashDistance;
	private float distanceTraveled;
	private Vector3 moveDirection;
	
	public DashState(PlayerController pController)
	{
		dashSpeed = pController.DashSpeed;
		dashDistance = pController.DashDistance;
		gravity = pController.Gravity;
		_characterController = pController.GetCharController ();
		_characterTransform = pController.transform;
		_characterAnimator = pController.GetCharAnimator ();

		_characterAnimator.SetBool ("Dash", true);
	}
	
	public void BeginState(StateMachine stateMachine)
	{
		moveDirection = new Vector3( Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		moveDirection.Normalize ();
		Quaternion camAngle = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, new Vector3(0, 1, 0));
		moveDirection = camAngle * moveDirection;
		_characterTransform.LookAt (_characterTransform.position + moveDirection);
		distanceTraveled = 0.0f;
	}
	
	public void Update(StateMachine stateMachine)
	{
		//Debug.Log ("Dash");
		//Get Controller Input

		if (distanceTraveled < dashDistance) {
			float distance = Time.deltaTime * dashSpeed;
			if((distanceTraveled+distance) > dashDistance){
				distance = dashDistance - distanceTraveled;
				distanceTraveled = dashDistance;
				_characterController.Move(moveDirection * distance);
			}
			else{
				distanceTraveled += distance;
				_characterController.Move(moveDirection * Time.deltaTime * dashSpeed);
			}
		}
		else {
			Vector3 moveDirection = new Vector3( Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
			//If no longer moving
			if (moveDirection.magnitude < 0.05f) {
				stateMachine.SetNextState ("idle");
				return;
			}
			else if (Input.GetAxisRaw ("Run") > 0.05f) {
				stateMachine.SetNextState("run");
				return;
			}
			else if (moveDirection.magnitude > 0.05f) {
				stateMachine.SetNextState ("walk");
				return;
			}
		}
	}
	
	public void EndState(StateMachine stateMachine)
	{
		_characterAnimator.SetBool ("Dash", false);
	}
}
*/