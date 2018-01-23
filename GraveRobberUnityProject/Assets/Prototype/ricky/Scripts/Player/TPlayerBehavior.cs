using UnityEngine;
using System.Collections;

public class TPlayerBehavior : MonoBehaviour {

	public float maxSpeed = 16f;
	public float sprintingSpeed = 12f;
	public float minSpeed = 8f;
	public float sprintingToMaxSecs = 4f;
	public float momentumToSprintingSecs = 1f;
	public float maxRotationSpeed = 240f;
	public float gravity = 10f;

	public bool allowWallPushing = true;
	public float timingWindow = 0.1f;
	private float timer = 0f;
	private float hitTimer = -1f;
	private Vector3 prevVelocity;
	private Vector3 prevPrevVelocity;

	private Vector3 velocity = Vector3.zero;

	private CharacterController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		//gravity
		controller.Move(new Vector3(0, - gravity * Time.deltaTime, 0));

		timer += Time.deltaTime;
		while (timer >= timingWindow) {
			timer -= timingWindow;
			hitTimer -= timingWindow;
			prevPrevVelocity = prevVelocity;
			prevVelocity = Vector3.zero;
		}

		var direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		if (direction == Vector3.zero) {
			velocity = Vector3.zero;
			return;
		}
		
		float length = direction.magnitude;
		direction /= length;
		if (length < 1) {
			// For analog sticks make the center less sensitive
			length = length * length * length;
			direction *= length;
		}
		else length = 1;
		direction = Camera.main.transform.rotation * direction; // align direction with camera
		direction = (Quaternion.FromToRotation(-Camera.main.transform.forward, transform.up) * direction); // make direction orthogonal to character's up

		// Rotate to face
		if (direction.sqrMagnitude >= 0.01) {
			if (velocity.magnitude >= minSpeed) {
				Vector3 newForward = Vector3.Slerp(transform.forward, direction, Mathf.Min(1, (maxRotationSpeed * Time.deltaTime)/Vector3.Angle(transform.forward, direction)));
				newForward -= Vector3.Project(newForward, transform.up); // Project onto plane
				transform.rotation = Quaternion.LookRotation(newForward, transform.up);
			}
			else {
				transform.rotation = Quaternion.LookRotation((direction - Vector3.Project(direction, transform.up)));
			}
		}

		if (allowWallPushing && hitTimer >= 0f) {
			velocity = direction * Mathf.Max(prevVelocity.magnitude, prevPrevVelocity.magnitude);
		}
		else {
			float similarity = Vector3.Dot(transform.forward.normalized, direction);
			
			if (similarity <= 0) {
				velocity = direction * minSpeed;
			}
			else {
				//curSpeed = sprintingSpeed;
				velocity = direction * Mathf.Clamp(((1.0f-similarity)*minSpeed + similarity * (velocity.magnitude + Time.deltaTime * ((sprintingSpeed - minSpeed)/momentumToSprintingSecs))), minSpeed, maxSpeed);
			}
		}
		controller.Move(velocity * Time.deltaTime);

		if (velocity.magnitude > prevVelocity.magnitude) {
			prevVelocity = velocity;
		}

		/*if (velocity.magnitude < minSpeed) {
			controller.Move(direction * Time.deltaTime * startMomentum);
			velocity = direction * startMomentum;
		}
		else {
			velocity += 
			momentum += direction * Time.deltaTime * ((sprintingSpeed - startMomentum)/momentumToSprintingSecs);
			controller.Move(momentum * Time.de);
		}*/

		GameObject camMan = GameObject.Find ("CameraManager");
		CameraManagerScript camScript = camMan.GetComponent<CameraManagerScript> ();
		camScript.CameraUpdate ();
	}

	public void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.normal.y < 0.5f) {
			timer = 0f;
			hitTimer = 2f * timingWindow;
		}
	}
}
