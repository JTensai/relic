using UnityEngine;
using System.Collections;

public class sinkScript : MonoBehaviour 
{

	private Vector3 startPosition;

	public int objectThreshold = 1;
	public float speed = 1.0F;
	private int objectCount;

	void Start() 
	{
		startPosition = transform.position;
		objectCount = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float weight = objectCount - objectThreshold;
		Debug.Log(objectCount);
		if(weight > 0)
		{
			transform.position -= new Vector3(0, speed*weight*Time.deltaTime, 0);
		}
		else if(weight < 0)
		{
			if(transform.position.y < startPosition.y)
			{
				transform.position -= new Vector3(0, speed*weight*Time.deltaTime, 0);
			}
		}

	}

	void FixedUpdate()
	{
		objectCount = 0;
	}

	void OnTriggerStay(Collider collider)
	{
		objectCount++;
	}
}