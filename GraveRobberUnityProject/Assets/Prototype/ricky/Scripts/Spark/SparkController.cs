using UnityEngine;
using System.Collections;

public class SparkController : MonoBehaviour {
//	//Public variables
//	public float MoveSpeed = 4.0f;
//	public float Range = 5.0f;
//	public float ReturnSpeed = 5.0f;
//	public float MoveThreshold = 0.2f;
//	public float SnapThreshold = 0.2f;
//	public float StuckTime = 2.0f;
//
//	public float XOffset = 1.0f;
//	public float YOffset = 0.5f;
//	public float ZOffset = -1.0f;
//
//	public float BobbleSpeed = 0.3f;
//	public float BobbleRange = 1.0f;
//
//	//Components
//	private CharacterController _charController;
//	private Transform _sparkTransform;
//
//	//Player
//	private GameObject _player;
//	private PlayerController _playerController;
//
//	//Misc
//	private Vector3 playerOffset;
//	private bool withPlayer = true;
//	
//	private StateMachine stateMachine;
//	
//	// Use this for initialization /////////////////////////////////////////////////////////////////
//	void Start () {
//		_charController = GetComponent<CharacterController>();
//		_sparkTransform = GetComponent<Transform> ();
//
//		playerOffset = new Vector3(XOffset, YOffset, ZOffset);
//
//		_player = GameObject.FindWithTag ("Player");
//		_playerController = this.GetPlayer ().GetComponent<PlayerController>();
//		
//		InitializeState ();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		//Update state machine
//		if (_playerController == null) {
//			return;
//		}
//
//		stateMachine.Update ();
//	}
//
//	public StateMachine getStateMachine(){
//		return stateMachine;
//	}
//
//	private void InitializeState()
//	{
//		stateMachine = new StateMachine((string stateName) => {
//			//Debug.Log ("Current State: " + stateName);
//			switch (stateName)
//			{
//			case "start":
//				withPlayer = true;
//				return new SparkIdleState(this);
//			case "idle":
//				withPlayer = true;
//				return new SparkIdleState(this);
//			case "move":
//				withPlayer = false;
//				return new SparkMoveState(this);
//			case "return":
//				withPlayer = false;
//				return new SparkReturnState(this);
//			case "stationed":
//				withPlayer = false;
//				return new SparkStationedState(this);
//			case "follow":
//				withPlayer = true;
//				return new SparkFollowState(this);
//			case "menu":
//				return new SparkMenuState(this);
//			case "action":
//				return new SparkActionState(this);
//			}
//			return null;
//		});
//	}
//	
//	public CharacterController GetCharController(){
//		return _charController;
//	}
//	
//	public Transform GetSparkTransform(){
//		return _sparkTransform;
//	}
//
//	public GameObject GetPlayer(){
//		return _player;
//	}
//
//	public Vector3 GetPlayerOffset(){
//		return playerOffset;
//	}
//
//	public bool IsWithPlayer(){
//		return withPlayer;
//	}
}
