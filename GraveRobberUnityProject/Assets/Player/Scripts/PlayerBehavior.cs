using UnityEngine;
using System.Collections;

public class PlayerBehavior : PlayerBase {
	public float MovementSpeed = 1f;
	private CharacterController _characterController;
	// Use this for initialization
	void Start () {
		_characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W)){
			_characterController.Move(new Vector3(0, 0, 1) * Time.deltaTime * MovementSpeed);
		}
		if(Input.GetKey(KeyCode.S)){
			_characterController.Move(new Vector3(0, 0, -1) * Time.deltaTime * MovementSpeed);
		}
		if(Input.GetKey(KeyCode.A)){
			_characterController.Move(new Vector3(-1, 0, 0) * Time.deltaTime * MovementSpeed);
		}
		if(Input.GetKey(KeyCode.D)){
			_characterController.Move(new Vector3(1, 0, 0) * Time.deltaTime * MovementSpeed);
		}

		_characterController.Move(new Vector3(0, -1f, 0));
	}
	
}
