using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour {

	private VisionBase vision;
	private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		vision = GetComponent<VisionBase>();
		lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		HashSet<MovementComponent> movedThisFrame = new HashSet<MovementComponent>();

		Vector3 offset = transform.position - lastPosition;

		foreach (GameObject gameObject in vision.ObjectsInVision())
		{
			MovementComponent moveComponent = gameObject.GetComponentInParent<MovementComponent>();

			if (moveComponent != null && !movedThisFrame.Contains(moveComponent))
			{
				moveComponent.Move(0, offset);
				movedThisFrame.Add(moveComponent);
			}
		}

		lastPosition = transform.position;
	}
}
