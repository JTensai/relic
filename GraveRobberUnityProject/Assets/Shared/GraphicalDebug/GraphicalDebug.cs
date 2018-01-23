using UnityEngine;
using System.Collections;

public static class GraphicalDebug
{
	private static GameObject globalDebugOut;

	private static GameObject GlobalDebugOut
	{
		get
		{
			if (globalDebugOut == null)
			{
				globalDebugOut = new GameObject("GlobalDebugOut");
			}

			return globalDebugOut;
		}
	}

	public static void AppendDebugLine(Vector3 position, GameObject targetGameObject = null)
	{
		if (targetGameObject == null)
		{
			targetGameObject = GlobalDebugOut;
		}

		DebugLinePlot linePlot = targetGameObject.GetComponent<DebugLinePlot>();

		if (linePlot == null)
		{
			linePlot = targetGameObject.AddComponent<DebugLinePlot>();
		}

		linePlot.AppendPoint(position);
	}
}
