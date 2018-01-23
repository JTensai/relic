/// <summary>
/// We should 'borrow' form the unity camera manager. It uses a margin to see if the player moved 
/// enough for the camera to even care.
/// </summary>

using UnityEngine;
using System.Collections;

public class CameraManagerScript : MonoBehaviour {

//	public Camera IsometricDiagonal;
//	public Camera IsometricTopDown;
//	public Camera PerspectiveDiagonal;
//	public Camera PerspectiveTopDown;
//	public Camera PerspectiveThreeFourths;
	public Camera PerspectiveDiagonalBetter;

	public GameObject ObjectToFollow;

	public float HorizontalDeadZone = 0f;
	public float VerticalDeadZone = 0f;
	public AnimationCurve HorizontalEasing;
	public float MaxHorizontalDistance = 2;
	public float HorizontalMoveSpeed = 1;
	public float HorizontalMoveMultiplier = 1;
	public AnimationCurve VerticalEasing;
	public float MaxVerticalDistance = 2;
	public float VerticalMoveSpeed;
	public float VerticalMoveMultiplier = 1;

	public float CameraSuperSpeedDistance = 6f;
	public float CameraSuperSpeedMultiplier = 10f;
	public float CameraSuperSpeedActivationTime = 1f;
	public AnimationCurve CameraSuperSpeedCurve;

	public float CameraLeadAmount = 4f;
	public bool DoCameraLead = true;

	public float CameraLeadMovementSpeed = 1f;

	private float _currentCameraSuperSpeedMultiplier;
	private float _currentCameraSuperSpeedElapsed;


	// rate at which camera should move towards player , 1 = default
	public float cameraSpeed = 1f;
	// distance at which to not care about 'the spark', higher is better.
	public float sparkRange = 8f;
//	private float targetFOV = 8f;

	private int CameraType = 3;

	// For Cutscenes
	private GameObject cutsceneTarget = null;
	private float elapsedCutsceneTime = 0f;
	private float targetCutsceneTime = 0f;
	private float targetHoldTime = 0f;
	//private float elapsedHoldTime = 0f;

	private Vector3 cutsceneStartPosition = Vector3.zero;
    //private float cutsceneFOV;

	// Adjust up from feet
	private Vector3 cameraFollowAdjust = new Vector3(3,0,-3);
	private Vector3 cameraLeadCurrent = Vector3.zero;

	public Vector3 CurrentForward {get;private set;}
	public static CameraManagerScript CurrentCameraManager{get;private set;}
	private Camera _currentCamera;

	public float BaseRespawnTravelTime = 1f;
//	public float RespawnMoveSpeed = 50f;
	public AnimationCurve RespawnAcceleration;
	[ReadOnlyAttribute]
	public bool playerDied = false;
	private bool snappingToPlayer = false;
	private PlayerController _player;

	public Quaternion GetCameraRotation(){
		return Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, new Vector3(0, 1, 0));
	}
	
	// Use this for initialization
	void Start () {
//		IsometricDiagonal.enabled = true;

//		IsometricTopDown.enabled = false;
//		PerspectiveTopDown.enabled = false;
//		PerspectiveDiagonal.enabled = false;
//		PerspectiveThreeFourths.enabled = false;

		if(ObjectToFollow == null){
			//set references
			ObjectToFollow = GameObject.FindGameObjectWithTag("Player");
			_player = ObjectToFollow.GetComponent<PlayerController>();
		}
		RenderSettings.fog = true;

		CameraManagerScript.CurrentCameraManager = this;
		_currentCameraSuperSpeedElapsed = 0f;
		//set camera start position
		this.transform.position = ObjectToFollow.transform.position;
		CheckCameraType();
	}

	// Update is called once per frame
	public void CameraUpdate() {
		//Check if camera type needs to be switched
		CheckCameraType();

		//Move camera
		CheckCameraMovement ();

 		/* 
 		 * The problem with doing this method is that the static meshes are 
		 * all baked together and if you modify the shader for part, you modify
		 * it for the entire thing....gg
		 */

//		Vector3 dir = ObjectToFollow.transform.position - PerspectiveDiagonalBetter.transform.position;
//		Ray ray = new Ray(PerspectiveDiagonalBetter.transform.position, dir);
//		Debug.DrawRay (PerspectiveDiagonalBetter.transform.position, dir, Color.green);
////		int layers = (1 << LayerMask.NameToLayer ("Static Environment"));
//
//		RaycastHit[] hits = Physics.RaycastAll(ray, dir.magnitude);
//		foreach(RaycastHit h in hits){
//			GameObject obj = h.transform.gameObject;
//			Renderer rend = obj.GetComponentInChildren<Renderer>();
//			if (rend != null)
//			{
//				rend.enabled = false;
////				rend.material.shader = Shader.Find("Transparent/Diffuse");
////				rend.material.color.a = 0.3F;
//			}
//			Debug.Log(h.transform.gameObject.name + " is obstructing the camera.");
//		}
	}



	void CheckCameraMovement (){

		Vector3 targetPosition = ObjectToFollow.transform.position + cameraFollowAdjust;//new Vector3(1,0,-1);
		if (!playerDied){
			if(DoCameraLead){
				Vector3 inp = PlayerUtils.getInputDirection();
				Vector3 leadTarget = inp * CameraLeadAmount;
				if(!cameraLeadCurrent.Equals(leadTarget)){
					Vector3 dir = (leadTarget - cameraLeadCurrent).normalized;
					if(Vector3.Distance(leadTarget, cameraLeadCurrent) < (CameraLeadMovementSpeed * Time.deltaTime)){
						cameraLeadCurrent = leadTarget;
					}
					else{
						cameraLeadCurrent += (dir * CameraLeadMovementSpeed * Time.deltaTime);
					}

				}
 				targetPosition += cameraLeadCurrent;
			}

			if (cutsceneTarget == null) {
				Vector2 targetHorizontal = new Vector2(targetPosition.x, targetPosition.z);
				Vector2 curHorizontal = new Vector2(transform.position.x, transform.position.z);
				float horizDistance = Vector2.Distance(targetHorizontal, curHorizontal);
				Vector2 toMoveHorizontal = Vector2.zero;
				if(horizDistance > HorizontalDeadZone){
					float percentageDistance = Mathf.Min (1f, horizDistance / MaxHorizontalDistance);
					float val = HorizontalEasing.Evaluate(percentageDistance);
					toMoveHorizontal = (targetHorizontal - curHorizontal).normalized;
					float multiplier = val * HorizontalMoveSpeed * HorizontalMoveMultiplier * Time.deltaTime;
					toMoveHorizontal *= multiplier;
				}
				if(horizDistance > CameraSuperSpeedDistance){
					_currentCameraSuperSpeedElapsed += Time.deltaTime;
					_currentCameraSuperSpeedElapsed = Mathf.Clamp(_currentCameraSuperSpeedElapsed, 0, CameraSuperSpeedActivationTime);
					float perc = _currentCameraSuperSpeedElapsed / CameraSuperSpeedActivationTime;
					perc = CameraSuperSpeedCurve.Evaluate(perc);
					_currentCameraSuperSpeedMultiplier = (CameraSuperSpeedMultiplier - 1) * perc + 1;
					toMoveHorizontal *= _currentCameraSuperSpeedMultiplier;
				}
				else{
					_currentCameraSuperSpeedElapsed -= Time.deltaTime;
					_currentCameraSuperSpeedElapsed = Mathf.Clamp(_currentCameraSuperSpeedElapsed, 0, CameraSuperSpeedActivationTime);
					float perc = _currentCameraSuperSpeedElapsed / CameraSuperSpeedActivationTime;
					perc = CameraSuperSpeedCurve.Evaluate(perc);
					_currentCameraSuperSpeedMultiplier = (CameraSuperSpeedMultiplier - 1) * perc + 1;
					toMoveHorizontal *= _currentCameraSuperSpeedMultiplier;
				}

				float targetVertical = targetPosition.y;
				float curVertical = transform.position.y;
				float vertDistance = Mathf.Abs (targetVertical - curVertical);
				float toMoveVertical = 0f;
				if(vertDistance > VerticalDeadZone){
					float percentageDistance = Mathf.Min(1f, vertDistance / MaxVerticalDistance);
					float val = VerticalEasing.Evaluate(percentageDistance);
					toMoveVertical = (targetVertical - curVertical) < 0? -1:1;
					float multiplier = val * VerticalMoveSpeed * VerticalMoveMultiplier * Time.deltaTime;
					toMoveVertical *= multiplier;
				}

				Vector3 fullMove = new Vector3(toMoveHorizontal.x, toMoveVertical, toMoveHorizontal.y);
				this.transform.position += fullMove;
//				if(this.getCurrentCamera().fieldOfView != targetFOV){
//
//				}
			} else {
				elapsedCutsceneTime += Time.deltaTime;
				// During cutscene: move camera, zoom camera.
				this.transform.position = Vector3.Lerp (
					cutsceneStartPosition, cutsceneTarget.transform.position + cameraFollowAdjust, elapsedCutsceneTime / targetCutsceneTime);
				if (elapsedCutsceneTime > targetCutsceneTime + targetHoldTime){
					// cutscene ended, snap to player
					setCutsceneTarget (null, 0f, 0f);
					StartCoroutine("SnapToPlayer", 0f);
				}
				//this.getCurrentCamera().fieldOfView = Mathf.Lerp(this.getCurrentCamera().fieldOfView, cutsceneFOV, 1.0f * Time.deltaTime);
			}
		}
		else{
			// player died, move quickly back to character
			if (!snappingToPlayer)
				StartCoroutine("SnapToPlayer", 0f);
		}

		//float fov = this.getCurrentCamera ().fieldOfView;


		/*
		if ((player.transform.position - sparkPosition).magnitude > sparkRange && spark != null) {
			Vector3 targetPosition = (player.transform.position + sparkPosition) / 2;
			//should do something with Y position concideration
			this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * cameraSpeed);
		} else {
			//should do something with Y position concideration
			this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position, Time.deltaTime * cameraSpeed);
		}*/
	}

	void CheckCameraType(){
		//Check for input
		if (Input.GetKeyUp (KeyCode.Joystick1Button5) || Input.GetKeyUp(KeyCode.F)) {
			//CameraType++;
			//if (CameraType == 4)
				//CameraType = 1;
		} 

		//disable all cameras
//		IsometricDiagonal.enabled = false;
//		IsometricTopDown.enabled = false;
//		PerspectiveTopDown.enabled = false;
//		PerspectiveDiagonal.enabled = false;
//		PerspectiveThreeFourths.enabled = false;
//		PerspectiveDiagonalBetter.enabled = false;

//		if (CameraType == 1) {
//			IsometricDiagonal.enabled = true;
//			CurrentForward = IsometricDiagonal.transform.forward;
//			_currentCamera = IsometricDiagonal;
//		}
//		if (CameraType == 2) {
//			PerspectiveDiagonal.enabled = true;
//			CurrentForward = PerspectiveDiagonal.transform.forward;
//			_currentCamera = PerspectiveDiagonal;
//		}
		if (CameraType == 3) {
			PerspectiveDiagonalBetter.enabled = true;
			CurrentForward = PerspectiveDiagonalBetter.transform.forward;
			_currentCamera = PerspectiveDiagonalBetter;
		}

		// Switch to new active camera
//		if (CameraType == 1) {
//			IsometricDiagonal.enabled = true;
//			CurrentForward = IsometricDiagonal.transform.forward;
//		}
//		if (CameraType == 2) {
//			PerspectiveDiagonal.enabled = true;
//			CurrentForward = PerspectiveDiagonal.transform.forward;
//		}
//		if (CameraType == 3) {
//			IsometricTopDown.enabled = true;
//			CurrentForward = IsometricTopDown.transform.forward;
//		}
//		if (CameraType == 4) {
//			PerspectiveThreeFourths.enabled = true;
//			CurrentForward = PerspectiveThreeFourths.transform.forward;
//		}
//		if (CameraType == 5) {
//			PerspectiveTopDown.enabled = true;
//			CurrentForward = PerspectiveTopDown.transform.forward;
//		}
	}

	public Camera getCurrentCamera(){
		//return (CameraType == 1 ? IsometricDiagonal : PerspectiveDiagonal);
		return _currentCamera;
	}

//	public void setTargetFOV(float target){
//		this.targetFOV = target;
//	}

	/*public void setCutsceneTarget(GameObject target, float cutsceneTime){
		cutsceneTarget = target;
		elapsedCutsceneTime = 0f;
		this.targetCutsceneTime = cutsceneTime;
		cutsceneStartPosition = transform.position;
	}*/

	public void setCutsceneTarget(GameObject target, float cutsceneTime, float holdTime){
		cutsceneTarget = target;
		elapsedCutsceneTime = 0f;
		//elapsedHoldTime = 0f;
		this.targetCutsceneTime = cutsceneTime;
		this.targetHoldTime = holdTime;
		cutsceneStartPosition = transform.position;
	}

	public void setCutsceneFOV(float targetFOV){
        //cutsceneFOV = targetFOV;
	}

	public IEnumerator SnapToPlayer(float startDelay){
		Debug.Log ("Player died, snapping to player");
		_player = ObjectToFollow.GetComponent<PlayerController> ();
		_player.gameObject.GetComponent<MovementComponent> ().stun (10f);
		yield return new WaitForSeconds (startDelay);
		snappingToPlayer = true;
		Vector3 initialPosition = this.transform.position;
		Vector3 targetPosition = ObjectToFollow.transform.position + cameraFollowAdjust;
		float _elapsedTime = 0;
		float Duration = 0;
		float distance = Vector3.Distance (initialPosition, targetPosition);
		Duration = BaseRespawnTravelTime + (distance / 40);

		while(_elapsedTime < Duration){
			targetPosition = ObjectToFollow.transform.position + cameraFollowAdjust;
//			if (targetPosition != ObjectToFollow.transform.position + cameraFollowAdjust) break;
			_elapsedTime += Time.deltaTime;
			float delta = RespawnAcceleration.Evaluate(_elapsedTime / Duration);
			this.transform.position = Vector3.Lerp(initialPosition, targetPosition, delta);
			yield return null;
		}
		_player.gameObject.GetComponent<MovementComponent> ().unstun ();
//		Debug.Log("Done snapping to player");
		playerDied = false;
		snappingToPlayer = false;
	}
}
