using UnityEngine;
using System.Collections;

public class SlidingCrate : MonoBehaviour {

	private MovementComponent movement;
	private AttackBase attack;
	private bool isSliding;

	public Vector3 size = Vector3.one;
	public float slideSpeed = 5.0f;
	public float minPushSpeed = 1.0f;

	private Vector3 velocity;

	// Use this for initialization
	void Start () {
		movement = GetComponent<MovementComponent>();

		InteractableComponent interactable = GetComponent<InteractableComponent>();
		interactable.OnInteract += HandleOnInteract;
		interactable.OnNotify += HandleOnNotify;

		attack = GetComponent<AttackBase>();

		IsSliding = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isSliding)
		{
			movement.Move(0, velocity * Time.deltaTime);
		}
		else
		{
			Vector3 horizontalVelocity = movement.Velocity;
			horizontalVelocity.y = 0.0f;

			if (horizontalVelocity.sqrMagnitude > minPushSpeed * minPushSpeed)
			{
				Push(horizontalVelocity);
			}
		}
	}

	public bool IsSliding
	{
		get
		{
			return isSliding;
		}

		set
		{
			isSliding = value;
		}
	}

	
	public void OnExtendedControllerColliderHit(ExtendedControllerColliderHit hit)
	{
		if (Mathf.Abs(hit.normal.y) < 0.5f)
		{	
			HealthComponent health = hit.gameObject.GetComponentInParent<HealthComponent>();
			
			if (health != null)
			{
				health.Damage(attack.Damage);
				
				if (!HealthComponent.IsDead(health))
				{
					IsSliding = false;
					hit.MovementAfterCollision = Vector3.zero;
				}
			}
			else
			{
				IsSliding = false;
			}
		}
	}

	public void Push(Vector3 direction)
	{
		direction.y = 0.0f;

		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
		{
			direction.z = 0.0f;
		}
		else
		{
			direction.x = 0.0f;
		}

		direction.Normalize();

		velocity = direction * slideSpeed;
		IsSliding = true;
	}
	
	private void HandleOnNotify(InteractableNotifyEventData data)
	{

	}

	private void HandleOnInteract(InteractableInteractEventData data)
	{
		if (!isSliding)
		{
			Push(transform.position - data.Source.transform.position);
		}
	}
}
