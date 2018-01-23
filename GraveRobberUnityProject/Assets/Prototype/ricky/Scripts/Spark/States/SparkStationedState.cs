using UnityEngine;
using System.Collections;

public class SparkStationedState : IState {

//	private float moveThreshold;
//	
//	public SparkStationedState(SparkController sController)
//	{
//		moveThreshold = sController.MoveThreshold;
//	}
	
	public void BeginState(StateMachine stateMachine)
	{
		
	}
	
	public void Update(StateMachine stateMachine)
	{
//		//Debug.Log ("SparkStationed");
//		//Get Controller Input
//		if (Input.GetAxisRaw ("SparkAction") < -0.1f) {
//			return;
//		}
//		Vector3 moveDirection = new Vector3( Input.GetAxisRaw("SparkHorizontal"), 0, Input.GetAxisRaw("SparkVertical"));
//		if (moveDirection.magnitude > moveThreshold) {
//			stateMachine.SetNextState ("move");
//			return;
//		} else {
//			stateMachine.SetNextState("return");
//			return;
//		}
//		
	}
	
	public void EndState(StateMachine stateMachine)
	{
		
	}
}

