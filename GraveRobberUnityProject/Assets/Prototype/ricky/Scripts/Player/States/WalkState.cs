using UnityEngine;
using System.Collections;

public class WalkState : IState {
	
	private float gravity;
    //private bool jumpReady;

	public EffectBase Dust;
	public EffectBase Splash;
	public EffectBase DustNewInstance;
	public EffectBase SplashNewInstance;

	//Cached variabled drawn from controller, here for convinience.
	private float _walkSpeed;
	private float _walkMaxRadiansDelta;
	private Transform _characterTransform;
	private Animator _characterAnimator;
	private MovementComponent _movement;
	private PlayerController _pController;

	//Removed variables.
	//private float _rotationSpeed;
	//private float _walkTimer;
	//private float _secondsToMax;

	public WalkState(PlayerController pController)
	{
		_pController = pController;
		_walkSpeed = pController.WalkSpeed;
		_walkMaxRadiansDelta = pController.WalkMaxRadiansDelta;
		gravity = pController.Gravity;
        //jumpReady = pController.JumpReady ();
		_characterTransform = pController.transform;
		_characterAnimator = pController.GetCharAnimator ();
		_movement = pController.GetMoveComponent ();
		//_secondsToMax = pController.SecondsToMaxWalk;
		//_rotationSpeed = pController.RotationSpeed;
		this.Splash = pController.SplashFX;
	}
	
	public void BeginState(StateMachine stateMachine)
	{

	}
	
	public void Update(StateMachine stateMachine)
	{

		//Find out what "Up" and "Right" really mean.
//		Vector3 directionOfUp = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
//		Vector3 directionOfRight = new Vector3(Camera.main.transform.forward.z, 0, -Camera.main.transform.forward.x).normalized;

		//Figure out where the player is trying to move by multiplying their "up" by up and their "Right" by right.
		Vector3 moveDirection = PlayerUtils.getInputDirection();

		//There used to be a check here to see if the player was still moving, but now that moving and idle are the same state, we're taking it out.

		//If jump button down
		/*else*/ if (Input.GetAxisRaw ("Jump") < -0.1f && _pController.JumpReady() && _movement.IsGrounded) {
			stateMachine.SetNextState("jump");
			return;
		}

		//If action button down
		else if (Input.GetButtonDown("Action")) {
			stateMachine.SetNextState("action");
			return;
		}

		//If dodge button down
		else if (Input.GetAxisRaw ("Dodge") < -0.1f && _pController.DodgeReady()) {
			stateMachine.SetNextState("dodge");
			return;
		}


        //The bastion guy doesn't ease into a walk. So at darren's request, I removed this.
        float currSpeed = _walkSpeed;


		// if large turn angle, rotate in place, no move.
		bool rotateInPlace = false;
		if (Vector3.Angle (moveDirection, _pController.transform.forward) > 5f) {
			rotateInPlace = true;
			moveDirection = Vector3.RotateTowards (_pController.transform.forward, moveDirection, _walkMaxRadiansDelta * Time.deltaTime, 0f);
		}

		Vector3 mag = PlayerUtils.getMoveValues();
	
		if(Mathf.Sqrt(mag.x*mag.x+mag.z*mag.z) < .3f){
			moveDirection = Vector3.zero;
		}

		//Taking camera angle into account is longer needed; its built into the moveDirection.
		
		//Rotate player instantly, like bastion.
		if (moveDirection.magnitude!= 0)
		{
			_characterTransform.rotation = Quaternion.LookRotation(moveDirection);
			Vector3 pForward = _pController.transform.forward;
			if(!_pController.IsWalkingInWater)
			{
				if(Dust != null && DustNewInstance == null)
				{
					DustNewInstance = Dust.GetInstance (_pController.transform.position + (pForward * 0.1f));
					
					DustNewInstance.transform.rotation = _pController.transform.rotation;
					DustNewInstance.transform.Rotate(new Vector3(0,0,0));
					DustNewInstance.PlayEffect ();
				}
				else if(DustNewInstance != null) 
				{
					DustNewInstance.transform.position = _pController.transform.position;
				}
			}
			else
			{
				if(Splash != null && SplashNewInstance == null)
				{
					SplashNewInstance = Splash.GetInstance(_pController.transform.position);
					SplashNewInstance.PlayEffect();
				}
				else if(SplashNewInstance != null)
				{
					SplashNewInstance.transform.position = _pController.transform.position;
				}
			}
		}
		else
		{
            if (DustNewInstance != null)
            {
                DustNewInstance.StopEffect();
            }
			if(SplashNewInstance != null)
			{
				SplashNewInstance.StopEffect();
			}
		}
		
		//Move Character
		moveDirection = PlayerUtils.getInputDirection() * currSpeed;
		_characterAnimator.SetFloat ("Speed", moveDirection.magnitude);

		//This jump is wrong, because it is a constant downward movement instead of an accelarative force! TODO: Fix.
		moveDirection.y -= gravity;

		_movement.Move (0, moveDirection * Time.deltaTime);
	}
	
	public void EndState(StateMachine stateMachine)
	{
		if(DustNewInstance != null)
			DustNewInstance.StopEffect();
		if(SplashNewInstance != null)
			SplashNewInstance.StopEffect ();
	}
}
