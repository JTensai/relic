using UnityEngine;
using System.Collections;

public class SparkActionState : IState
{
//	
//	private CharacterController _charController;
//	private float moveThreshold;
//	private float moveSpeed;
//	private bool charging = false;
//	private float chargeRange = 5f;
//	private float autoTargetAngleTolerance = 22.5f;
//	private Vector3 chargeTarget;
//	private InteractableComponent targetIC = null;
//	private float distanceMag;
//	
//	public SparkActionState (SparkController sController)
//	{
//		_charController = sController.GetCharController ();
//		moveThreshold = sController.MoveThreshold;
//		moveSpeed = sController.MoveSpeed;
//	}
//	
	public void BeginState (StateMachine stateMachine)
	{
//		Debug.Log ("Begin Action State");
	}
//	
	public void Update (StateMachine stateMachine)
	{
//		if (!charging) {
//			//Debug.Log ("SparkAction");
//			//Get Controller Input
//			if (Input.GetAxisRaw ("SparkAction") < -0.1f) {
//				stateMachine.SetNextState ("stationed");
//				return;
//			}
//			Vector3 moveDirection = new Vector3 (Input.GetAxisRaw ("SparkHorizontal"), 0, Input.GetAxisRaw ("SparkVertical"));
//			if (moveDirection.magnitude < moveThreshold) {
//				stateMachine.SetNextState ("return");
//				return;
//			}
//		
//			//Move Spark
//			moveDirection = GetCameraRotation () * moveDirection.normalized * Time.deltaTime * moveSpeed;
//			//	_charController.Move (moveDirection);
//
//			chargeTarget = (moveDirection.normalized * chargeRange) + _charController.transform.position;
//		
//
//			//ACTION
//			Collider[] hitColliders = Physics.OverlapSphere (_charController.transform.position, 5f);
//			int i = 0;
//			InteractableComponent closestIC = null;
//			while (i < hitColliders.Length) {
//				InteractableComponent currIC = (InteractableComponent)hitColliders [i].gameObject.GetComponent<InteractableComponent> ();
//				if (currIC == null) {
//					if (hitColliders [i].transform.parent != null)
//						currIC = hitColliders [i].transform.parent.GetComponent<InteractableComponent> ();
//				}
//				if (currIC != null) {
//					//Debug.Log("moveDirection.normalized" + moveDirection.normalized);
//					//Debug.Log("chargeTarget " +chargeTarget);
//
//					//Debug.Log(Vector3.Angle(						Vector3.Scale(new Vector3(1f,0f,1f), currIC.gameObject.transform.position - _charController.transform.position),						Vector3.Scale(new Vector3(1f,0f,1f), chargeTarget - _charController.transform.position)					        ) );
//					if (Vector3.Angle(
//						Vector3.Scale(new Vector3(1f,0f,1f), currIC.gameObject.transform.position - _charController.transform.position),
//						Vector3.Scale(new Vector3(1f,0f,1f), chargeTarget - _charController.transform.position)
//						)< autoTargetAngleTolerance) {
//						//Debug.Log("OK");
//						/*	if(ActionFX != null && ActionNewInstance == null)
//					{
//						Vector3 pForward = _characterTransform.forward;
//						ActionNewInstance = ActionFX.GetInstance (_characterTransform.position + (pForward * 0.5f));
//						ActionNewInstance.PlayEffect ();
//					}*/
//						if (closestIC == null) {
//							closestIC = currIC;
//						} else {
//							float currDistance = Vector3.Distance (_charController.transform.position, currIC.transform.position);
//							float oldDistance = Vector3.Distance (_charController.transform.position, closestIC.transform.position);
//							if (oldDistance > currDistance)
//								closestIC = currIC;
//						}
//					}
//				}
//				i++;
//			}
//			
//			if (closestIC != null) {
//				//Debug.Log(closestIC.ToString());
//				Vector3 distance = chargeTarget - closestIC.gameObject.transform.position;
//				chargeTarget = closestIC.gameObject.transform.position;
//				if(closestIC.gameObject.name.Contains("Pillar")){
//					chargeTarget += new  Vector3(0f,2f,0f);
//				}
//				//closestIC.NotifyInteraction (new InteractableInteractEventData (_charController.gameObject, true, distance.magnitude));
//				targetIC = closestIC;
//				distanceMag = distance.magnitude;
//			}
//
//
//
//
//
//
//			charging = true;
//		} else {
//		//	Debug.Log (chargeTarget);
//		//	Debug.Log ("_charController.transform.position " + _charController.transform.position);
//
//			_charController.transform.position = Vector3.Lerp (_charController.transform.position, chargeTarget, Time.deltaTime * 8f);
//			if ((_charController.transform.position - chargeTarget).magnitude < 1f) {
//
//				if (targetIC != null) {
//					targetIC.NotifyInteraction (new InteractableInteractEventData (_charController.gameObject, false, distanceMag));
//				}
//				targetIC = null;
//				charging = false;
//				stateMachine.SetNextState ("return");
//					
//			}
//		}
	}
	
	public void EndState (StateMachine stateMachine)
	{
		
	}
//	
//	//MISC/////////////////////////////////////////////////////////////////
//	Quaternion GetCameraRotation ()
//	{
//		Transform cameraRot = Camera.main.transform;
//		return Quaternion.AngleAxis (cameraRot.eulerAngles.y, new Vector3 (0, 1, 0));
//	}
}
