using UnityEngine;
using System.Collections;

public class ArrowProjectileLauncher : MonoBehaviour {

	public ArrowProjectile projectilePrefab;
	public float speed = 4.0f;
	public float acceleration = 0.0f;
	public float range = 10.0f;

	public Transform fireOrigin;

	private Vector3 velocity;
	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		if (fireOrigin == null)
		{
			fireOrigin = transform;
		}
	}

	private IEnumerator Fly()
	{

		yield return null;
	}

	public void Fire(Vector3 target)
	{

	}

	public void Fire()
	{
		Fire(fireOrigin.TransformPoint(Vector3.forward * range));
	}
}
