using UnityEngine;
using System.Collections;

public class WanderingInteractor : MonsterBase {

	public float interactionDelay = 1.0f;
	public float moveSpeed = 2.0f;
	public float turnRate = 90.0f;
	public bool turnRight = true;

	private MovementComponent movement;
	private GameObject lastHit = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnEnable()
	{
		movement = GetComponent<MovementComponent>();
		StartCoroutine(Wander());
	}

	private IEnumerator Wander()
	{
		while (true)
		{
			while (lastHit == null)
			{
				movement.Move(0, transform.TransformDirection(Vector3.forward) * moveSpeed * Time.deltaTime);
				yield return null;
			}

			InteractableComponent interactable = lastHit.GetComponentInParent<InteractableComponent>();

			if (interactable != null)
			{
				yield return new WaitForSeconds(interactionDelay);

				interactable.NotifyInteraction(new InteractableInteractEventData(gameObject, false, Vector3.Distance(interactable.transform.position, transform.position)));
			}

			Quaternion targetRotation = Quaternion.AngleAxis(turnRight ? 90.0f : -90.0f, Vector3.up) * transform.localRotation;

			Vector3 targetForward = targetRotation * Vector3.forward;
			targetForward.y = 0.0f;

			if (Mathf.Abs(targetForward.x) > Mathf.Abs(targetForward.z))
			{
				targetForward.z = 0.0f;
			}
			else
			{
				targetForward.x = 0.0f;
			}

			targetRotation = Quaternion.LookRotation(targetForward, Vector3.up);

			Quaternion currentRotation = transform.localRotation;

			while (targetRotation != currentRotation)
			{
				currentRotation = Quaternion.RotateTowards(currentRotation, targetRotation, turnRate * Time.deltaTime);
				transform.localRotation = currentRotation;
				yield return null;
			}

			lastHit = null;
		}
	}
	
	public void OnExtendedControllerColliderHit(ExtendedControllerColliderHit hit)
	{
		if (Mathf.Abs(hit.normal.y) < 0.5f)
		{
			lastHit = hit.gameObject;
		}
	}
}
