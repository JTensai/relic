using UnityEngine;
using System.Collections;

public class DamageState : IState {
	
	private Transform _characterTransform;
	private Animator _characterAnimator;
	private PlayerController _pController;
	private MovementComponent _movement;
	private float damageTime;
	private float damageSpeed;
	private float damageTimer;
	private float gravity;
//	private float rotationSpeed;
	
	public EffectBase DamageFX;
	private EffectBase DamageNewInstance;
	
    //private GameObject player;
    //private PlayerController playerController;
	
	public DamageState(PlayerController pController)
	{
		_characterTransform = pController.transform;
		_characterAnimator = pController.GetCharAnimator ();
		_pController = pController;
		_movement = pController.GetMoveComponent ();
		
		damageTime = pController.DamageTime;
		damageSpeed = pController.DamageSpeed;
		gravity = pController.Gravity;
//		rotationSpeed = pController.RotationSpeed;
		DamageFX = pController.DamageFX;

        //player = GameObject.FindWithTag("Player");
        //playerController = player.GetComponent<PlayerController> ();
	}
	
	public void BeginState(StateMachine stateMachine)
	{
		_characterAnimator.SetBool ("Damage", true);
		damageTimer = 0.0f;
		_pController.DropPickedUpItem();
		//GameObject damVin = GameObject.Find("DamageVignette");
		//if (damVin != null){
		//	damVin.GetComponent<DamageVignette>().Activate();
		//}
	}
	
	public void Update(StateMachine stateMachine)
	{
		if(DamageFX != null && DamageNewInstance == null)
		{
			Vector3 pForward = _characterTransform.forward;
			DamageNewInstance = DamageFX.GetInstance (_characterTransform.position + (pForward * 0.5f));
			DamageNewInstance.PlayEffect ();
		}
		//Debug.Log ("Damage");
		damageTimer += Time.deltaTime;
		
		Vector3 moveDirection = PlayerUtils.getInputDirection();
		float moveMag = PlayerUtils.getMoveMagnitude();

		//If jump button down
		if (Input.GetAxisRaw ("Jump") < -0.1f && _pController.JumpReady() && _pController.IsGrounded) {
			stateMachine.SetNextState("jump");
			return;
		}

		//Still in damage state
		if (damageTimer < damageTime) {
			if (moveDirection.magnitude > 0.05f) {
				_characterTransform.rotation = Quaternion.LookRotation(moveDirection);
				//Take camera angle into account
				//Quaternion camAngle = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, new Vector3(0, 1, 0));
				//moveDirection = camAngle * moveDirection;
				
				//Rotate player
				//_characterTransform.rotation = Quaternion.RotateTowards (_characterTransform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);
				
				//Move Character
				moveDirection = moveDirection * damageSpeed;
				_characterAnimator.SetFloat ("Speed", moveDirection.magnitude * moveMag);
				moveDirection.y -= gravity;
				_movement.Move (0, moveDirection * Time.deltaTime * moveMag);
			}
			return;
		}
		
		//Damage state over
		if (moveMag > 0.05f) {
			stateMachine.SetNextState ("walk");
			return;
		}
		stateMachine.SetNextState ("idle");
	}
	
	public void EndState(StateMachine stateMachine)
	{
		_characterAnimator.SetBool ("Damage", false);
	}
}
