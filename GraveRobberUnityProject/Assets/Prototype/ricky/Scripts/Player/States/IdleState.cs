using UnityEngine;
using System.Collections;

public class IdleState : IState {

	private Animator _characterAnimator;
	private PlayerController _pController;
	private float gravity;
	private bool jumpReady;
	
	public IdleState(PlayerController pController)
	{
		gravity = pController.Gravity;
		_characterAnimator = pController.GetCharAnimator ();
		_pController = pController;
	}
	
	public void BeginState(StateMachine stateMachine)
	{
		_characterAnimator.SetFloat ("Speed", 0.0f);
	}
	
	public void Update(StateMachine stateMachine)
	{
		if(_pController.GetMoveComponent().isStunned()){
			return;
		}
		//Debug.Log ("Idle");
		//Get Controller Input
		//Check for movement
		Vector3 moveDirection = PlayerUtils.getInputDirection();
//		float moveMag = PlayerUtils.getMoveMagnitude();
		if (moveDirection.magnitude > 0.05f) {
			stateMachine.SetNextState ("walk");
			return;
		}
		//If jump button down
		else if (Input.GetAxisRaw ("Jump") < -0.1f && _pController.JumpReady() && _pController.IsGrounded) {
			stateMachine.SetNextState("jump");
			return;
		}
		//Check for action button
		else if (Input.GetButtonDown("Action")) {
			stateMachine.SetNextState("action");
			return;
		}
		//If dodge button down
		else if (Input.GetAxisRaw ("Dodge") < -0.1f && _pController.DodgeReady()) {
			stateMachine.SetNextState("dodge");
			return;
		}

		Vector3 gravityVec = new Vector3 (0, gravity*-1.0f, 0);
		//_pController.GetMoveComponent().Move (0, gravityVec * Time.deltaTime);

	}
	
	public void EndState(StateMachine stateMachine)
	{
		
	}
}
