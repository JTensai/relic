using UnityEngine;
using System.Collections;

public class WaypointCar : MonoBehaviour {

	[SerializeField]
	private WaypointPath path;

	[SerializeField]
	private float moveSpeed = 2.0f;

	private float distanceOnPath;

	// Use this for initialization
	void Start () {
		if (path != null)
		{
			distanceOnPath = path.DistanceAtPosition(transform.position);
		}
	}
	
	// Update is called once per frame
	void Update () {
		DistanceOnPath += moveSpeed * Time.deltaTime;
	}

	public float DistanceOnPath
	{
		get
		{
			return distanceOnPath;
		}

		set
		{
			distanceOnPath = value;
			transform.position = path.GetPositionAtDistance(value);
		}
	}
}
