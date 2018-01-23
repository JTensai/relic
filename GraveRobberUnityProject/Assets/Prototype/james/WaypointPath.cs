using UnityEngine;
using System.Collections;

public class WaypointPath : MonoBehaviour {

	[SerializeField]
	// in local coordinates
	private Vector3[] waypointPositions = new Vector3[]{
		new Vector3(-1.0f, 0.0f, -1.0f),
		new Vector3(1.0f, 0.0f, -1.0f),
		new Vector3(1.0f, 0.0f, 1.0f),
		new Vector3(-1.0f, 0.0f, 1.0f)};

	[SerializeField]
	// if true, the waypoint is a circular path
	private bool isClosedLoop = true;

	private float[] segmentLengths;
	private float pathLength;

	private void RecalcLength()
	{
		segmentLengths = new float[isClosedLoop ? waypointPositions.Length : (waypointPositions.Length - 1)];

		pathLength = 0.0f;

		for (int i = 0; i < segmentLengths.Length; ++i)
		{
			float segmentLength = Vector3.Distance(waypointPositions[i], waypointPositions[(i + 1) % waypointPositions.Length]);
			segmentLengths[i] = segmentLength;
			pathLength += segmentLength;
		}
	}

	public void Start()
	{
		CheckRecalcLength();
	}

	private void CheckRecalcLength()
	{
		if (segmentLengths == null)
		{
			RecalcLength();
		}
	}

	public int WaypointCount
	{
		get
		{
			return waypointPositions == null ? 0 : waypointPositions.Length;
		}

		set
		{
			Vector3[] newArray = new Vector3[value];

			for (int i = 0; i < newArray.Length && waypointPositions != null && i < waypointPositions.Length; ++i)
			{
				newArray[i] = waypointPositions[i];
			}

			waypointPositions = newArray;
			RecalcLength();
		}
	}

	public Vector3 GetWaypoint(int index)
	{
		return transform.TransformPoint(waypointPositions[index]);
	}

	public void SetWaypoint(int index, Vector3 value)
	{
		waypointPositions[index] = transform.InverseTransformPoint(value);
		RecalcLength();
	}

	public float DistanceAtIndex(float index)
	{
		CheckRecalcLength();

		index = NormalizeIndex(index);

		float result = 0.0f;
		int i = 0;

		while (index > 1.0f)
		{
			result += segmentLengths[i];
			++i;
		}

		result += segmentLengths[i] * index;

		return result;
	}

	public float IndexAtDistance(float distance)
	{
		CheckRecalcLength();

		float index = 0.0f;
		
		if (isClosedLoop)
		{
			distance %= pathLength;
			if (distance < 0.0f)
			{
				distance += pathLength;
			}
		}
		else
		{
			distance %= pathLength * 2.0f;
			if (distance < 0.0f)
			{
				distance += pathLength * 2.0f;
			}

			if (distance > pathLength)
			{
				distance = pathLength * 2.0f - distance;
			}
		}   
		
		int segmentIndex = 0;
			    
		while (segmentIndex < segmentLengths.Length && distance >= segmentLengths[segmentIndex])
		{
			index += 1.0f;
			distance -= segmentLengths[segmentIndex];
			++segmentIndex;
		}

		if (segmentIndex < segmentLengths.Length)
		{
			index += distance / segmentLengths[segmentIndex];
		}

		return index;
	}

	private float NormalizeIndex(float index)
	{
		if (isClosedLoop)
		{
			index = index % (float)waypointPositions.Length;
			
			if (index < 0.0f)
			{
				index += waypointPositions.Length;
			}
		}
		else
		{
			float doubleLength = 2.0f * (waypointPositions.Length - 1.0f);
			
			index = index % doubleLength;
			
			if (index < 0.0f)
			{
				index += doubleLength;
			}
			
			if (index >= waypointPositions.Length - 1.0f)
			{
				index = doubleLength - index;
			}
		}

		return index;
	}
	
	public Vector3 GetPositionAtDistance(float distance)
	{
		return GetPositionAtIndex(IndexAtDistance(distance));
	}

	public Vector3 GetPositionAtIndex(float index)
	{
		index = NormalizeIndex(index);

		return transform.TransformPoint(Vector3.Lerp(
			waypointPositions[Mathf.FloorToInt(index)],
			waypointPositions[Mathf.CeilToInt(index) % waypointPositions.Length],
			index % 1.0f));
	}

	public float DistanceAtPosition(Vector3 position)
	{
		return DistanceAtIndex(IndexAtPosition(position));
	}

	public float IndexAtPosition(Vector3 position)
	{
		CheckRecalcLength();

		float nearestDistance = float.MaxValue;
		float result = 0.0f;

		position = transform.InverseTransformPoint(position);

		for (int i = 0; i < waypointPositions.Length; ++i)
		{
			float pointDistance = (position - waypointPositions[i]).sqrMagnitude;

			if (pointDistance < nearestDistance)
			{
				result = (float)i;
				nearestDistance = pointDistance;
			}

			if (i < segmentLengths.Length)
			{
				Vector3 a = waypointPositions[i];
				Vector3 b = waypointPositions[(i + 1) % waypointPositions.Length];

				Vector3 edge = b - a;

				float t = Vector3.Dot(position - a, edge) / edge.sqrMagnitude;

				if (t > 0.0f && t < 1.0f)
				{
					Vector3 projectedPoint = t * edge + a;

					float projectedDistance = (position - projectedPoint).sqrMagnitude;

					if (projectedDistance < nearestDistance)
					{
						nearestDistance = projectedDistance;
						result = i + t;
					}
				}
			}
		}

		return result;
	}

	public void OnDrawGizmos()
	{
		if (waypointPositions != null && waypointPositions.Length > 1)
		{
			for (int i = 1; i < waypointPositions.Length; ++i)
			{
				Gizmos.DrawLine(transform.TransformPoint(waypointPositions[i - 1]), transform.TransformPoint(waypointPositions[i]));
			}

			if (isClosedLoop)
			{
				Gizmos.DrawLine(transform.TransformPoint(waypointPositions[waypointPositions.Length - 1]), transform.TransformPoint(waypointPositions[0]));
			}
		}
	}
}
