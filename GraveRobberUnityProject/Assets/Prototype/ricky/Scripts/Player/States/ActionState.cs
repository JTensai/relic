using UnityEngine;
using System.Collections;

public class ActionState : IState
{

	private Transform _characterTransform;
	private Animator _characterAnimator;
//	private MovementComponent _movement;
	private float actionTimer;
//	private float actionTime;
//	private float moveSpeed;
//	private float gravity;
//	private float rotationSpeed;
	public EffectBase ActionFX;
	private EffectBase ActionNewInstance;
	private GameObject player;
	private PlayerController playerController;
	
	public ActionState (PlayerController pController)
	{
		_characterTransform = pController.transform;
		_characterAnimator = pController.GetCharAnimator ();
//		_movement = pController.GetMoveComponent ();

//		actionTime = pController.ActionTime;
//		moveSpeed = 3.0f;
//		gravity = pController.Gravity;
//		rotationSpeed = pController.RotationSpeed;

		player = GameObject.FindWithTag ("Player");
		playerController = player.GetComponent<PlayerController> ();
	}
	
	public void BeginState (StateMachine stateMachine)
	{
		//_characterAnimator.SetBool ("Action", true);
		actionTimer = 0.0f;
	}
	
	public void Update (StateMachine stateMachine)
	{
		/*if(playerController._thingPickedUp = true && playerController._theThingThatIsPickedUp != null){
			playerController._theThingThatIsPickedUp.transform.parent = null;
			playerController._theThingThatIsPickedUp.Throw(playerController.transform.position);
			playerController._thingPickedUp = false;
			playerController._theThingThatIsPickedUp = null;
			stateMachine.SetNextState ("idle");
			return;
		}*/

		bool stateMachineBugFix = false;

		//Debug.Log ("Action");
		actionTimer += Time.deltaTime;

		if (playerController._theThingThatIsPickedUp == null) {
//			Collider[] hitColliders = Physics.OverlapSphere (this._characterTransform.position, playerController.ActionRadius);
//			int i = 0;
			InteractableComponent closestIC = null;
//			while (i < hitColliders.Length) {
			foreach (GameObject g in playerController.getProximityArea().ObjectsInVision()){
				InteractableComponent currIC = g.GetComponent<InteractableComponent> ();
				if (currIC == null)  {
					currIC = g.GetComponentInParent<InteractableComponent>();
				}

				if (currIC != null) {
					if (ActionFX != null && ActionNewInstance == null) {
						if(g.GetComponent<Pillar>())
							{Vector3 pForward = _characterTransform.forward;
							ActionNewInstance = ActionFX.GetInstance (_characterTransform.position + (pForward * 0.5f));
							ActionNewInstance.PlayEffect ();}
					}
//					if (!currIC.gameObject.name.Contains ("Pillar")) {
						if (closestIC == null) {
							closestIC = currIC;
						} else {
							float currDistance = Vector3.Distance (_characterTransform.position, currIC.transform.position);
							float oldDistance = Vector3.Distance (_characterTransform.position, closestIC.transform.position);
							if (oldDistance > currDistance)
								closestIC = currIC;
						}
//					}
				}
//				i++;
			}
			
			if (closestIC != null) {
				//Debug.Log(closestIC.ToString());
//				Vector3 distance = this._characterTransform.position - closestIC.gameObject.transform.position;
				//	if(playerController.ActionReady()){
				_characterAnimator.SetBool ("Action", true);

				closestIC.NotifyInteraction (new InteractableInteractEventData (player, true, 0));
				Debug.Log ("Stuck calling NotifyInteraction");
//				closestIC = null;
				if(closestIC.GetComponent<ThrowablePrefab>()!=null){
					stateMachineBugFix = true;
				}
				if(closestIC.GetComponent<RubblePile>()!= null){
					stateMachine.SetNextState("hold");
					return;
				}
//				stateMachineBugFix = true; // this was actually CAUSING a bug where if you couldn't interact with something we got stuck here
				//	}
			}


//			InteractableComponent closestIC = null;
			//			GameObject closest = playerController.getClosestTarget(playerController.getProximityArea(), out closestIC, false);
			//
			////			Debug.Log("Trying to interact with: " + closest.name);
			//			if (closestIC != null) {
			//				Vector3 pForward = _characterTransform.forward;
			//				ActionNewInstance = ActionFX.GetInstance (_characterTransform.position + (pForward * 0.5f));
			//				ActionNewInstance.PlayEffect ();
			//				closestIC.NotifyInteraction (new InteractableInteractEventData (player, true, 0));
			//				stateMachineBugFix = true;
			//			}
			//			else{
			//				Debug.Log("closestIC seems to be null in ActionState");
			//			}
		}


//		Vector3 moveDirection = PlayerUtils.getInputDirection();
		float moveMag = PlayerUtils.getMoveMagnitude();


		if (!stateMachineBugFix) {
			//Action over
			if (moveMag > 0.05f) {
				stateMachine.SetNextState ("walk");
				return;
			}
			stateMachine.SetNextState ("idle");
		}
	}
	
	public void EndState (StateMachine stateMachine)
	{
		_characterAnimator.SetBool ("Action", false);
	}
}
