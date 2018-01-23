using UnityEngine;
using System.Collections;

public class DamageSpecialState : IState {
	
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
	
	public DamageSpecialState(PlayerController pController)
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
			Debug.Log("Damage state to jump state");
			stateMachine.SetNextState("jump");
			return;
		}
		
		//Still in damage state
		if (damageTimer < damageTime) {
			if (moveDirection.magnitude > 0.05f) {
				_characterTransform.rotation = Quaternion.LookRotation(moveDirection);

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
