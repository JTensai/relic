using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DebugLinePoint
{
	public Vector3 position;
	public Color color;

	public DebugLinePoint(Vector3 position)
	{
		this.position = position;
		this.color = Gizmos.color;
	}
}

public class DebugLinePlot : MonoBehaviour
{
	public List<DebugLinePoint> data = new List<DebugLinePoint>();

	public void AppendPoint(Vector3 position)
	{
		data.Add(new DebugLinePoint(position));
	}

	public void OnDrawGizmos()
	{
		Color lastColor = Gizmos.color;

		for (int i = 1; i < data.Count; ++i)
		{
			Gizmos.color = data[i - 1].color;
			Gizmos.DrawLine(data[i - 1].position, data[i].position);
		}

		Gizmos.color = lastColor;
	}
}
