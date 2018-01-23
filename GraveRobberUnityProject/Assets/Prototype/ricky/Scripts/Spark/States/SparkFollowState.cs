using UnityEngine;
using System.Collections;

public class SparkFollowState : IState {
//	
//	private CharacterController _charController;
//	private float moveThreshold;
//    //private float snapThreshold;
//	private float bobbleSpeed;
//	private float bobbleRange;
//	private float currentHeightOffset;
//	private bool ascending;
//	
//	private Vector3 playerOffset;
//	
//	private PlayerController _playerController;
//	private Transform _sTransform;
//
//	private int betweenSpin;
//	private Quaternion prevRot;
//
//
//	private Vector3 currentSpeed;
//	
//	public SparkFollowState(SparkController sController)
//	{
//		_charController = sController.GetCharController ();
//		moveThreshold = sController.MoveThreshold;
//        //snapThreshold = sController.SnapThreshold;
//		bobbleRange = sController.BobbleRange;
//		bobbleSpeed = sController.BobbleSpeed;
//		playerOffset = sController.GetPlayerOffset ();
//		
//		_playerController = sController.GetPlayer ().GetComponent<PlayerController>();
//		_sTransform = sController.GetSparkTransform ();
//	}
//	
	public void BeginState(StateMachine stateMachine)
	{
//		currentHeightOffset = 0.0f;
//		ascending = true;
//		betweenSpin = 3;
//		prevRot = _charController.gameObject.transform.rotation;
//
//		currentSpeed = new Vector3(0f,0f,0f);
	}
//	
	public void Update(StateMachine stateMachine)
	{
//		//Debug.Log ("SparkFollow");
//		//Get Controller Input
//		if (Input.GetAxisRaw ("SparkAction") < -0.1f) {
//			stateMachine.SetNextState("stationed");
//			return;
//		}
////		Vector3 moveDirection = new Vector3( Input.GetAxisRaw("SparkHorizontal"), 0, Input.GetAxisRaw("SparkVertical"));
////		if (moveDirection.magnitude > moveThreshold) {
////			stateMachine.SetNextState ("action");
////			//stateMachine.SetNextState ("move");
////			return;
////		} else if (!_playerController.IsMoving ()) {
////			stateMachine.SetNextState("idle");
////			return;
////		}
//	/*	
//		//Move spark towards player
//		Quaternion pAngle = Quaternion.AngleAxis( _playerController.transform.eulerAngles.y, new Vector3(0, 1, 0));
//		//Vector3 playerxzOffset = playerOffset;
//		//playerxzOffset.y = 1.0f;
//		Vector3 targetPos = (pAngle * playerOffset) + _playerController.transform.position;
//		Vector3 returnDirection = targetPos - _sTransform.position;//_charController.gameObject.transform.position;//_sTransform.position;
//*/
//		/*if (returnDirection.magnitude > /*snapThreshold*0.5f && returnDirection.magnitude<=1.0f) {
//			//targetPos = /*(pAngle * playerOffset) + _playerController.transform.position;
//			//returnDirection = new Vector3(0,0,0);//targetPos - _sTransform.position;
//			Debug.Log("WHY?!");
//			_charController.Move (returnDirection);
//		}*/
//		/*
//		if (returnDirection.magnitude > 1.0f) {
//			stateMachine.SetNextState("return");
//			return;
//		}
//
//
//
//		_charController.Move (returnDirection);
//
//
//		*/
//
//		// max speed
//		// current speed
//		// acceleration
//		// acceleration in tangent only?
//
//
//		Quaternion pAngle = Quaternion.AngleAxis( _playerController.transform.eulerAngles.y, new Vector3(0, 1, 0));
//		Vector3 targetPos = (pAngle * playerOffset) + _playerController.transform.position;
//		Vector3 returnDirection = targetPos - _sTransform.position;
//
//
//
//		//currentSpeed += Vector3.Scale(.01f * returnDirection.normalized, new Vector3(1f,0f,1f));
//		currentSpeed += .01f * returnDirection.normalized;
//
//	
//
//		if (returnDirection.magnitude > 2f) {
//			currentSpeed += .03f * returnDirection.normalized;
//			//Debug.Log ("re" + returnDirection.magnitude);
//				}
//
//	//	Debug.Log (currentSpeed);
//
//		if (currentSpeed.magnitude > .1) {
//			currentSpeed = currentSpeed.normalized * .1f;
//				}
//
//		if(returnDirection.magnitude < .3f){
//			currentSpeed = currentSpeed*.7f;;
//
//		}
//
//
//		_charController.Move (currentSpeed);
//
//
//
//
//
///*
//		//Bobble
//		Vector3 bobbleVector = new Vector3 (0.0f, currentHeightOffset, 0.0f);
//		_charController.Move(bobbleVector);
//		if (ascending) {
//			if(betweenSpin == 0)
//			{
//				currentHeightOffset += (2*bobbleSpeed*Time.deltaTime);
//			}
//			else
//				currentHeightOffset += (bobbleSpeed*Time.deltaTime);
//			if(currentHeightOffset > bobbleRange){
//				ascending = false;
//				if(betweenSpin == 0)
//				{
//					betweenSpin = 3;
//					_charController.gameObject.transform.rotation = prevRot;
//				}
//			}
//		} else {
//			currentHeightOffset -= (bobbleSpeed*Time.deltaTime);
//			if((currentHeightOffset * -1.0) > bobbleRange){
//				ascending = true;
//				betweenSpin--;
//				if(betweenSpin == 0)
//					_charController.gameObject.transform.rotation = new Quaternion(0f,0f,0f,1f);
//			}
//		}*/
//
//		//Spin
//		//Vector3 SpinVector = new Vector3 (0.2f * Time.deltaTime,0.0f, 0.0f);
//		Vector3 SpinVector = Vector3.up*30;//+Vector3.up;
//		//Vector3 SpinVector = new Vector3 (
//		if (betweenSpin == 0) {
//			_charController.gameObject.transform.Rotate(SpinVector,Space.World);
//			//_charController;
//		}
	}
	
	public void EndState(StateMachine stateMachine)
	{
		
	}
}
