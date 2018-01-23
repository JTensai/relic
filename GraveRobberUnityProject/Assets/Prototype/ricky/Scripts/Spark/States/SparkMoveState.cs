using UnityEngine;
using System.Collections;

public class SparkMoveState : IState {
	
//	private CharacterController _charController;
//	private float moveThreshold;
//	private float moveSpeed;
//	
//	public SparkMoveState(SparkController sController)
//	{
//		_charController = sController.GetCharController ();
//		moveThreshold = sController.MoveThreshold;
//		moveSpeed = sController.MoveSpeed;
//	}
	
	public void BeginState(StateMachine stateMachine)
	{
		
	}
	
	public void Update(StateMachine stateMachine)
	{
//		//Debug.Log ("SparkMove");
//		//Get Controller Input
//		if (Input.GetAxisRaw ("SparkAction") < -0.1f) {
//			stateMachine.SetNextState("stationed");
//			return;
//		}
//		Vector3 moveDirection = new Vector3( Input.GetAxisRaw("SparkHorizontal"), 0, Input.GetAxisRaw("SparkVertical"));
//		if (moveDirection.magnitude < moveThreshold) {
//			stateMachine.SetNextState("return");
//			return;
//		}
//
//		//Move Spark
//		moveDirection = GetCameraRotation() * moveDirection.normalized * Time.deltaTime * moveSpeed;
//		_charController.Move(moveDirection);
		
	}
	
	public void EndState(StateMachine stateMachine)
	{
		
	}
	
	//MISC/////////////////////////////////////////////////////////////////
//	Quaternion GetCameraRotation(){
//		Transform cameraRot = Camera.main.transform;
//		return Quaternion.AngleAxis(cameraRot.eulerAngles.y, new Vector3(0, 1, 0));
//	}
}
