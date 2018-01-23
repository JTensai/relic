using UnityEngine;
using System.Collections;

public class NewPlayerPrototype : PlayerBase {
	public float MoveSpeed = 2f;
	public float FullJumpMinimumSpeed = 0.1f;
	public float JumpChargeRate = 0.4f;

	private float jumpChargePercent;
	private MovementComponent _move;
	// Use this for initialization
	void Start () {
		_move = GetComponent<MovementComponent>();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");


		float effectiveMoveSpeed = MoveSpeed;

		if(Input.GetButton("Jump")){
			jumpChargePercent += JumpChargeRate * Time.deltaTime;
		}
		else{
			jumpChargePercent = 0f;
		}
		effectiveMoveSpeed = effectiveMoveSpeed * (1 - jumpChargePercent);
		Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
		Quaternion camAngle = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, new Vector3(0, 1, 0));
		moveDirection = camAngle * moveDirection;
		moveDirection.Normalize();
		_move.Move(0, new Vector3(horizontal, 0, vertical) * effectiveMoveSpeed * Time.deltaTime);
	}
}
