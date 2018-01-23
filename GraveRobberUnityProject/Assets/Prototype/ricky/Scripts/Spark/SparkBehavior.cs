using UnityEngine;
using System.Collections;

public class SparkBehavior : MonoBehaviour {

	public float XOffset = 1.0f;
	public float YOffset = 0.5f;
	public float ZOffset = 0.0f;
	public float MoveSpeed = 3.0f;
	public float ReturnSpeed = 8.0f;
	public float SnapDistance = 0.5f;
	public float Range = 15.0f;
	public float BobbleRange = 1.0f;
	public float BobbleSpeed = 0.01f;

	private CharacterController _sparkController;
	private GameObject _player;
	private Vector3 playerPosition;
	private Vector3 playerOffset;

	private float heightOffset = 0.0f;
	private bool ascending = true;

	// Use this for initialization
	void Start () {
		_sparkController = GetComponent<CharacterController>();
		_player = GameObject.FindWithTag ("Player");
		playerOffset = new Vector3(XOffset, YOffset, ZOffset);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 sparkDirection = new Vector3 (Input.GetAxis ("SparkHorizontal"), 0, Input.GetAxis("SparkVertical"));
		//If moving spark
		if (sparkDirection.magnitude > .1) {
			sparkDirection = sparkDirection.normalized;
			sparkDirection = GetCameraRotation() * sparkDirection;
			sparkDirection = sparkDirection * Time.deltaTime * MoveSpeed;

			Vector3 sparkDistance = this.transform.position - _player.transform.position;
			sparkDistance = sparkDistance + sparkDirection;
			if(sparkDistance.magnitude >= Range){

			}else{
				_sparkController.Move(sparkDirection);
			}

		}
		else {
			//If not in position
			Vector3 targetOffset = (GetPlayerRotation() * playerOffset) + _player.transform.position;
			if(this.transform.position != targetOffset){
				//Move back towards offset
				Vector3 playerDirection = targetOffset - this.transform.position;
				playerDirection.y = 0.0f;

				//If within range
				if(playerDirection.magnitude < SnapDistance)
				{
					//Snap to position
					_sparkController.Move (playerDirection);
				}
				else{
					playerDirection = playerDirection.normalized;
					Vector3 sparkMove = playerDirection * Time.deltaTime * ReturnSpeed;
					_sparkController.Move (sparkMove);
				}
			}
		}

		UpdateBobble();
	}

	void UpdateBobble(){
		Vector3 bobbleVector = new Vector3 (0.0f, BobbleSpeed * Time.deltaTime, 0.0f);
		if (ascending) {
			heightOffset += (BobbleSpeed*Time.deltaTime);
			_sparkController.Move(bobbleVector);
			if(heightOffset > BobbleRange){
				ascending = false;
			}
		} else {
			heightOffset -= (BobbleSpeed*Time.deltaTime);
			_sparkController.Move(bobbleVector*-1.0f);
			if((heightOffset * -1.0) > BobbleRange){
				ascending = true;
			}
		}

	}

	//MISC/////////////////////////////////////////////////////////////////
	Quaternion GetCameraRotation(){
		Transform cameraRot = Camera.main.transform;
		return Quaternion.AngleAxis(cameraRot.eulerAngles.y, new Vector3(0, 1, 0));
	}

	Quaternion GetPlayerRotation(){
		Transform playerRot = _player.transform;
		return Quaternion.AngleAxis(playerRot.eulerAngles.y, new Vector3(0, 1, 0));
	}
}
