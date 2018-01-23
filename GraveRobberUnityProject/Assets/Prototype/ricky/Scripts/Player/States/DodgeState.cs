using UnityEngine;
//using UnityEditor;
using System.Collections;

public class DodgeState : IState {

	private Transform _characterTransform;
	private Animator _characterAnimator;
	private float currDodgeDist;
	private Vector3 dodgeDirection;
	private PlayerController _pController;
	private MovementComponent _movement;
	private float dodgeSpeed;
	private float dodgeDistance;
	private float distanceMag;

	private InteractableComponent targetIC = null;

	private Vector3 chargeTarget;

	private bool charging = false;

	public EffectBase DodgeFX;
	private EffectBase DodgeNewInstance;

//	private float dodgeAutoAimDegrees;
	private float momentumMultiplier;

	public DodgeState(PlayerController pController)
	{
		_pController = pController;
		_characterTransform = pController.transform;
		_movement = pController.GetMoveComponent ();
		_characterAnimator = pController.GetCharAnimator ();
		dodgeSpeed = pController.DodgeSpeed;
		dodgeDistance = pController.DodgeDistance;
//		dodgeAutoAimDegrees = pController.DodgeAutoAimDegrees;
		momentumMultiplier = pController.MomentumMultiplier;
	}
	
	public void BeginState(StateMachine stateMachine)
	{
		CameraManagerScript.CurrentCameraManager.HorizontalMoveMultiplier = 3f;
		dodgeDirection = PlayerUtils.getInputDirection();
//		GameUI.UpdateDashBar (_pController.DodgeDelay);
		//If you are not moving, dodge forward.
		if (dodgeDirection.magnitude < 0.1f) 
		{
			dodgeDirection = _characterTransform.forward;
		}

		//Make sure that you're facing the direction you're dodging. I have no idea why you wouldn't be, but it couldn't hurt.
		_characterTransform.forward = dodgeDirection;

		currDodgeDist = 0.0f;
		_characterAnimator.SetBool ("Dodge", true);

		_pController.playDodgeSFX();
	}
	
	public void Update(StateMachine stateMachine)
	{

	/*	Vector3 newDodgeDirection = PlayerUtils.getInputDirection();

		if (newDodgeDirection != Vector3.zero)
		{
			dodgeDirection = newDodgeDirection;
			_characterTransform.forward = dodgeDirection;
		}
		if (currDodgeDist >= dodgeDistance) 
		{
			stateMachine.SetNextState ("walk");
			DodgeNewInstance = null;
			return;
		}

		currDodgeDist += dodgeSpeed * Time.deltaTime;

		_movement.Move(0, dodgeDirection * dodgeSpeed * Time.deltaTime);

		if(DodgeFX != null && DodgeNewInstance == null)
		{
			Vector3 pForward = _pController.transform.forward;
			DodgeNewInstance = DodgeFX.GetInstance (_pController.transform.position + (pForward * 0.1f));
			
			DodgeNewInstance.transform.rotation = _pController.transform.rotation;
			DodgeNewInstance.transform.Rotate(new Vector3(0,0,0));
			DodgeNewInstance.PlayEffect ();
		}
		else if(DodgeNewInstance != null) 
		{
			DodgeNewInstance.transform.position = _pController.transform.position;
		}
*/
		if (!charging) {
			Vector3 newDodgeDirection = PlayerUtils.getInputDirection();

            // if the player is not moving, dash forward
			if(newDodgeDirection.magnitude == 0){
				newDodgeDirection = _characterTransform.forward;
			}
            // chargeTarget is a transform.position of where the player should end up after dashing
			chargeTarget = (newDodgeDirection.normalized * dodgeDistance) + _pController.transform.position;			
			
            // Finds the closest object and sets it to closest
			InteractableComponent closestIC = null;
			GameObject closestObject = _pController.getClosestTarget(_pController.GetProximityViewedObjects(), out closestIC);
			if (closestObject == null){
				closestObject = _pController.getClosestTarget(_pController.GetLargeProximityViewedObjects(), out closestIC);
			}
			// we now know what the closest object is, grab the interactable component on it
			if (closestIC != null){
				Vector3 distance = chargeTarget - closestIC.gameObject.transform.position;
				chargeTarget = closestIC.gameObject.transform.position;
				targetIC = closestIC;
				distanceMag = distance.magnitude;
				dodgeDirection = chargeTarget - _pController.transform.position;

			}
			charging = true;
		}
        else {
            // we are already charging and have a target location and target interactable

            // move the player towards the dash target location
			currDodgeDist += dodgeSpeed * Time.deltaTime;
//			Vector3 newDir = PlayerUtils.getInputDirection();
//			_movement.Move(0, newDir.normalized * dodgeSpeed * Time.deltaTime);
			_movement.Move(0, dodgeDirection.normalized * dodgeSpeed * Time.deltaTime);


			// Hit an object that does not have an interactable component, but does have a rigid body
			Collider[] hitColliders = Physics.OverlapSphere(_pController.transform.position, 0.5f);
			foreach (Collider c in hitColliders) 
			{
				if (c.rigidbody != null && c.rigidbody.GetComponent<PlayerController>() == null){
					c.rigidbody.AddForce ((c.transform.position - _pController.transform.position )*50);
					if(c.rigidbody.isKinematic){
						if(c.rigidbody.GetComponent<ChargingMummy>()!=null){
							MovementComponent m = c.rigidbody.GetComponent<MovementComponent>();
			
							// Add momentum to Enemy (Enemy momentum dampened by MovementComponent.momentumDampener)
							m.AddMomentum(_pController.transform.forward * momentumMultiplier);

							// Stop the charge
							currDodgeDist = dodgeDistance;

							// Add momentum to player scaled by knockback multiplier
							_movement.AddMomentum(_pController.transform.forward* -_pController.MomentumKnockbackMultiplier * momentumMultiplier);
						}
					}
				}
			}



			//	Debug.Log (chargeTarget);
			//	Debug.Log ("_charController.transform.position " + _charController.transform.position);
			//Vector3 a =  Vector3.Lerp (_pController.transform.position, chargeTarget, Time.deltaTime * 8f);
			//_pController.transform.position = Vector3.Lerp (_pController.transform.position, chargeTarget, Time.deltaTime * 8f);



            // makes sure the player is close to the interactable object before interacting
            if (targetIC != null)
            {
                if ((_pController.transform.position - chargeTarget).magnitude < targetIC.dashCollisionSize)
                {
//                    Debug.Log("Dashed at " + targetIC.name + " and tried to call OnDashed for it");
                    targetIC.NotifyDashed(new InteractableInteractEventData(_pController.gameObject, true, distanceMag));

                    CameraShakeManager.CameraShakeData shakeData = new CameraShakeManager.CameraShakeData(new Vector3(0.12f, 0, 0.12f), 0.5f, 10);
                    CameraShakeManager.Shake(shakeData);
                    _pController.GetComponent<MovementComponent>().stun(0.3f);
                    //EditorApplication.isPaused = true;
                    targetIC = null;
                    charging = false;
                    stateMachine.SetNextState("walk");
                }
            }

			if (currDodgeDist >= dodgeDistance) 
			{
				stateMachine.SetNextState ("walk");
				DodgeNewInstance = null;
				return;
			}


		}


		if(DodgeFX != null && DodgeNewInstance == null)
		{
			Vector3 pForward = _pController.transform.forward;
			DodgeNewInstance = DodgeFX.GetInstance (_pController.transform.position + (pForward * 0.1f));
			
			DodgeNewInstance.transform.rotation = _pController.transform.rotation;
			DodgeNewInstance.transform.Rotate(new Vector3(0,0,0));
			DodgeNewInstance.PlayEffect ();
		}
		else if(DodgeNewInstance != null) 
		{
			DodgeNewInstance.transform.position = _pController.transform.position;
		}





	}

	
	public void EndState(StateMachine stateMachine)
	{
		CameraManagerScript.CurrentCameraManager.HorizontalMoveMultiplier = 1f;
		_characterAnimator.SetBool ("Dodge", false);
		_pController.ResetDodgeTimer ();
	}

}
