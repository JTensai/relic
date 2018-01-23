using UnityEngine;
using System.Collections;

public class SparkReturnState : IState {
//	
//	private CharacterController _charController;
//	private float moveThreshold;
//	private float snapThreshold;
//	private float returnSpeed;
//
//	private Vector3 playerOffset;
//
//	private PlayerController _playerController;
//	private Transform _sTransform;
//
//	private float maxDistance;
//	private float collideTimer;
//	private float collideTime;
//	
//	public SparkReturnState(SparkController sController)
//	{
//		_charController = sController.GetCharController ();
//		moveThreshold = sController.MoveThreshold;
//		snapThreshold = sController.SnapThreshold;
//		returnSpeed = sController.ReturnSpeed;
//		collideTime = sController.StuckTime;
//		maxDistance = sController.Range;
//		playerOffset = sController.GetPlayerOffset ();
//
//		_playerController = sController.GetPlayer ().GetComponent<PlayerController>();
//		_sTransform = sController.GetSparkTransform ();
//	}
	
	public void BeginState(StateMachine stateMachine)
	{
//		collideTimer = 0.0f;
	}

	public void Update(StateMachine stateMachine)
	{
//		//Debug.Log ("SparkReturn");
//		Quaternion pAngle = Quaternion.AngleAxis( _playerController.transform.eulerAngles.y, new Vector3(0, 1, 0));
//		Vector3 targetPos = (pAngle * playerOffset) + _playerController.transform.position;
//		Vector3 returnDirection = targetPos - _sTransform.position;
//
//		//Handle spark getting stuck
//		if ((_charController.collisionFlags & CollisionFlags.Sides) != 0) {
//			collideTimer += Time.deltaTime;
//		} else {
//			collideTimer = 0.0f;
//		}
//		if (collideTimer >= collideTime || returnDirection.magnitude > maxDistance) {
//			_sTransform.position = targetPos;
//		}
//
//		//Check for actions
//		if (Input.GetAxisRaw ("SparkAction") < -0.1f) {
//			stateMachine.SetNextState("stationed");
//			return;
//		}
//		Vector3 moveDirection = new Vector3( Input.GetAxisRaw("SparkHorizontal"), 0, Input.GetAxisRaw("SparkVertical"));
//		if (moveDirection.magnitude > moveThreshold) {
//			stateMachine.SetNextState ("move");
//			return;
//		}
//
//		//Move spark towards player
//		_charController.Move (returnDirection.normalized * Time.deltaTime * returnSpeed);
//
//		//Check if close to target position
//		returnDirection = targetPos - _sTransform.position;
//		if (returnDirection.magnitude < snapThreshold) {
//			stateMachine.SetNextState("idle");
//			return;
//		}

	}
	
	public void EndState(StateMachine stateMachine)
	{
		
	}
}
