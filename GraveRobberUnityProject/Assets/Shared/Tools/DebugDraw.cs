using UnityEngine;
using System.Collections;

public class DebugDraw {

	public static void DrawArc(Vector3 startPosition, float startAngle, float length, float arcAngle, Color lineColor){

		float STEPS_PER_DEGREE = 0.2f;

		Vector3 forward = Quaternion.Euler(new Vector3(0, startAngle, 0)) * Vector3.forward;

		Vector3 left = Quaternion.Euler(new Vector3(0, -arcAngle/2f, 0)) * forward;
		Vector3 right = Quaternion.Euler(new Vector3(0, arcAngle/2f, 0)) * forward;

		int numSteps = (int)(arcAngle * STEPS_PER_DEGREE);
		float stepSize = arcAngle / numSteps;

		Vector3 prevEndPoint = Vector3.zero;
		bool prevEndPointEmpty = true;

		Debug.DrawLine(startPosition, startPosition + (length * left), lineColor);
		for(int i = 0; i < numSteps; i++){
			Vector3 newArc = Quaternion.Euler(new Vector3(0, stepSize * i, 0)) * left;
			Vector3 newSize = newArc * length;
			//Debug.DrawLine(startPosition, startPosition + newSize);
			if(!prevEndPointEmpty){
				Debug.DrawLine(prevEndPoint, startPosition + newSize, lineColor);
			}
			prevEndPoint = startPosition + newSize;
			prevEndPointEmpty = false;
		}

		Debug.DrawLine(startPosition, startPosition + (length * right), lineColor);
		if(!prevEndPointEmpty){
			Debug.DrawLine(prevEndPoint, startPosition + (length * right), lineColor);
		}
	}
}
