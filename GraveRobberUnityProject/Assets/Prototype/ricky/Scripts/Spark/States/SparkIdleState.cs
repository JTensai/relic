using UnityEngine;
using System.Collections;

public class SparkIdleState : IState {
//	
//	private CharacterController _charController;
//	private float moveThreshold;
//
//	private float bobbleSpeed;
//	private float bobbleRange;
//
//	private PlayerController _playerController;
//
//	private float currentHeightOffset;
//	private bool ascending;
//	
//	public SparkIdleState(SparkController sController)
//	{
//		_charController = sController.GetCharController ();
//		moveThreshold = sController.MoveThreshold;
//
//		bobbleRange = sController.BobbleRange;
//		bobbleSpeed = sController.BobbleSpeed;
//
//		_playerController = sController.GetPlayer().GetComponent<PlayerController>();
//	}
//	
	public void BeginState(StateMachine stateMachine)
	{
//		currentHeightOffset = 0.0f;
//		ascending = true;
	}
//	
	public void Update(StateMachine stateMachine)
	{
//		//Debug.Log ("SparkIdle");
//		//Get Controller Input
//		Vector3 moveDirection = new Vector3( Input.GetAxisRaw("SparkHorizontal"), 0, Input.GetAxisRaw("SparkVertical"));
//		if (moveDirection.magnitude > moveThreshold) {
//			stateMachine.SetNextState ("move");
//			return;
//		} else if (_playerController.IsMoving ()) {
//			stateMachine.SetNextState("follow");
//			//stateMachine.SetNextState("return");
//			return;
//		}
//
//		//Idle Stuff
//		//Bobble
//		Vector3 bobbleVector = new Vector3 (0.0f, bobbleSpeed * Time.deltaTime, 0.0f);
//		if (ascending) {
//			currentHeightOffset += (bobbleSpeed*Time.deltaTime);
//			_charController.Move(bobbleVector);
//			if(currentHeightOffset > bobbleRange){
//				ascending = false;
//			}
//		} else {
//			currentHeightOffset -= (bobbleSpeed*Time.deltaTime);
//			_charController.Move(bobbleVector*-1.0f);
//			if((currentHeightOffset * -1.0) > bobbleRange){
//				ascending = true;
//			}
//		}
//
	}
	
	public void EndState(StateMachine stateMachine)
	{
		
	}
}

