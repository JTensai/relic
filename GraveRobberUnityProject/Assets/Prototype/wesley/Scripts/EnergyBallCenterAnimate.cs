using UnityEngine;
using System.Collections;

public class EnergyBallCenterAnimate : MonoBehaviour {

	private int direction = 1;
	private float dist;
	private float distPerFrame;
	private Vector3 oldScale;
	public Vector2 scaleMaxMin;
	public float framesPerSecond;
	public float phase;

	void OnEnable() {
		StartCoroutine(UpdateScale());
	}

	private IEnumerator UpdateScale()
	{
		dist = scaleMaxMin [1] - scaleMaxMin [0];
		distPerFrame = dist / phase;
		oldScale = transform.localScale;
		while (true) {
			if (direction == 1 && (oldScale [0] + distPerFrame) > scaleMaxMin [1]) {
					direction = -1;
			} else if (direction == -1 && (oldScale [0] - distPerFrame) < scaleMaxMin [0]) {
					direction = 1;
			}
			float deltaScale = distPerFrame * direction;
			transform.localScale = oldScale + new Vector3 (deltaScale, deltaScale, deltaScale);
			oldScale = transform.localScale;
			yield return new WaitForSeconds (1f / framesPerSecond);
		}
	}
}
