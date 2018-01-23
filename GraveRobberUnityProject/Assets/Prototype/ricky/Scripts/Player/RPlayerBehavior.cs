using UnityEngine;
using System.Collections;

public class RPlayerBehavior : PlayerBase {

	//Components
	private CharacterController _characterController;
	private Animator _animator;
	private float buttonTheshold = 0.1f;
	
	public float WalkSpeed = 5.5f;
	public float DashSpeed = 20f;
	public float DashDistance = 3.0f;
	public float DashPause = .05f;
	public float DashHiatus = .2f;
	
	//States
	enum State{
		Free,
		Dash,
		PostDash,
		Aim,
		Item,
		Action,
		Damage,
	}
	private State curState;

	//Dash State Variables
	private Vector3 dashDirection;
	private float distanceTraveled;
	private float startingTime;
	private bool canDash;

	//Items
	enum Items{
		Hookshot,

	}

	//Item Variables
	public float HookshotRange = 15f;
	public float HookshotSpeed = 20f;
	
	// Use this for initialization/////////////////////////////////////////////////////////////////
	void Start () {
		_characterController = GetComponent<CharacterController>();
		_animator = GetComponent<Animator> ();

		curState = State.Free;
		startingTime = Time.time;

		canDash = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (curState == State.Free) {
			Free ();
		} else if (curState == State.Dash) {
			//Dash ();
		} else if (curState == State.PostDash) {
			PostDash ();
		} else if (curState == State.Aim) {
			Aim();
		} else if (curState == State.Item) {
			Item();
		}

		if (Input.GetAxisRaw ("Dash") == 0)
			canDash = true;

		//Update camera manager
		GameObject camMan = GameObject.Find ("CameraManager");
		CameraManagerScript camScript = camMan.GetComponent<CameraManagerScript> ();
		camScript.CameraUpdate ();

		//TODO this is random code!
		InteractableComponent[] inters = GameObject.FindObjectsOfType<InteractableComponent>();
		foreach(InteractableComponent i in inters){
			if(Vector3.Distance(transform.position, i.transform.position) < 3f){
				i.NotifyProximity(new InteractableNotifyEventData(gameObject, true, 1f, 1f));
			}

		}
	}

	void FixedUpdate() {

		if (curState == State.Dash) {
			Dash ();
		}
	}

	
	//FREE STATE/////////////////////////////////////////////////////////////////
	void Free(){
		//Get Controller Input
		Vector3 moveDirection = new Vector3( Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		moveDirection = GetCameraRotation() * moveDirection;
		_animator.SetFloat ("Speed", moveDirection.magnitude);

		//Rotate player
		if (moveDirection.magnitude > 0.05f)
			transform.LookAt (transform.position + moveDirection);

		//Move Character
		moveDirection.y -= 1.0f;
		_characterController.Move(moveDirection * Time.deltaTime * WalkSpeed);

		//Check for Dash action
		if ((Input.GetAxisRaw("Dash")>buttonTheshold) && (Time.time > startingTime + DashHiatus) && canDash) {
			canDash = false;
			curState = State.Dash;
			_animator.SetBool("Dash",true);
			moveDirection.y = 0.0f;
			dashDirection = moveDirection.normalized;
			startingTime = Time.time;
			distanceTraveled = 0.0f;
		}
		//Check for Aim action
		else if(Input.GetAxisRaw("Aim")>buttonTheshold){
			curState = State.Aim;
			_animator.SetBool("Aim",true);
		}

	}

	//AIM STATE/////////////////////////////////////////////////////////////////
	void Aim(){
		if(Input.GetAxisRaw("Aim") > buttonTheshold){
			//Get Controller Input
			Vector3 moveDirection = new Vector3( Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
			moveDirection = GetCameraRotation() * moveDirection;

			//Rotate player
			if (moveDirection.magnitude > 0.05f)
				transform.LookAt (transform.position + moveDirection);

			//Raycast
			RaycastHit[] hits;
			hits = Physics.RaycastAll (transform.position, transform.forward, 100.0f);
			if(hits.Length > 0)
				Debug.DrawLine(transform.position, hits[0].point,Color.red);
			
			//Check for Dash action
			if ((Input.GetAxisRaw("Dash")>buttonTheshold) && (Time.time > startingTime + DashHiatus) && canDash) {
				canDash = false;
				curState = State.Dash;
				_animator.SetBool("Dash",true);
				moveDirection.y = 0.0f;
				dashDirection = moveDirection.normalized;
				startingTime = Time.time;
				distanceTraveled = 0.0f;
			}
			
			//Check for Item action
			if (Input.GetAxisRaw("Item")>buttonTheshold) {
				
			}
		}
		else {
			curState = State.Free;
			_animator.SetBool("Aim",false);
		}
		
	}

	//DASH STATE/////////////////////////////////////////////////////////////////
	void Dash(){
		if (distanceTraveled < DashDistance) {
			float distance = Time.deltaTime * DashSpeed;
			if((distanceTraveled+distance) > DashDistance){
				distance = DashDistance - distanceTraveled;
				distanceTraveled = DashDistance;
				_characterController.Move(dashDirection * distance);
			}
			else{
				distanceTraveled += distance;
				_characterController.Move(dashDirection * Time.deltaTime * DashSpeed);
			}
		}
		else {
			curState = State.PostDash;
			_animator.SetBool("Dash",false);
			startingTime = Time.time;
		}
	}
	
	//POST-DASH STATE/////////////////////////////////////////////////////////////////
	void PostDash(){
		if (Time.time < startingTime + DashPause) {

		}
		else {
			if(Input.GetAxisRaw("Aim")>buttonTheshold){
				curState = State.Aim;
				_animator.SetBool("Aim",true);
				startingTime = Time.time;
			}
			else{
				curState = State.Free;
				_animator.SetBool("Aim",false);
				startingTime = Time.time;
			}
		}
	}

	//ITEM STATE/////////////////////////////////////////////////////////////////
	void Item(){



	}

	//MISC/////////////////////////////////////////////////////////////////
	Quaternion GetCameraRotation(){
		Transform cameraRot = Camera.main.transform;
		return Quaternion.AngleAxis(cameraRot.eulerAngles.y, new Vector3(0, 1, 0));
	}

}
