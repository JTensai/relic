using UnityEngine;
using System.Collections;

public class Spotter : MonsterBase {
	public float bounceSpeed = 15f;
	public float bounceHeight = 0.2f;
	public float bounceOffset = 5f;

	[SerializeField]
	private GameObject sweeperVisual;

	private VisionArc vision;
	private Vector3 originalPosition;
	private Vector3 lastSeenPlayerPosition;
	private bool playerInSight;

	private float sweepTimer = 0.0f;
	private float sweepPeriod = 1.0f;

	// Use this for initialization
	void Start () {
		vision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Default, gameObject);
		playerInSight = false;
		originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInSight) {
			Vector3 pos = transform.position;
			pos.y = originalPosition.y + (Mathf.Sin (Time.time * bounceSpeed) + bounceOffset) * bounceHeight;
			transform.position = pos;

			Vector3 offset = lastSeenPlayerPosition - sweeperVisual.transform.position;
			offset.y = 0.0f;

			sweeperVisual.transform.rotation = Quaternion.LookRotation(offset, Vector3.up);
		} else {
			transform.position = originalPosition;

			sweepTimer += Time.deltaTime;

			sweeperVisual.transform.localRotation = Quaternion.AngleAxis(Mathf.Sin(Mathf.PI * 2.0f * sweepTimer / sweepPeriod) * vision.ArcSize * 0.5f, Vector3.up);
		}
	}

	public Transform GetPlayerTransform() {
		GameObject[] seenObjects = vision.PlayersInVision();
		foreach(GameObject obj in seenObjects) {
			if(obj.GetComponent<PlayerBase>() != null) {
				playerInSight = true;
				lastSeenPlayerPosition = obj.transform.position;
				return obj.transform;
			}
		}
		playerInSight = false;
		return null; 
	}
}
