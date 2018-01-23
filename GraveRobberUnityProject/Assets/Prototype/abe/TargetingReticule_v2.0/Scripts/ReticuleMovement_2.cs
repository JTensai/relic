using UnityEngine;
using System.Collections;

public class ReticuleMovement_2 : MonoBehaviour 
{
	public Material[] materials;
	public GameObject player;
	
	private Vector3 targetPoint;
	private GameObject targetObject;

	// Use this for initialization
	void Start () 
	{
		targetPoint = new Vector3(-10000f, -10000f, -10000f);
		targetObject = null;
	}

	// Update is called once per frame
	void Update () 
	{
		Screen.showCursor = false;
		FindIntersectionPoint();
	}


	//Cast ray, find both the intersected object and the point of intersection on that object...ONLY CALL WHEN MOUSE MOVED ACCORDING TO SOME MIN?
	void FindIntersectionPoint()
	{
		Vector3 direction = player.transform.forward;
		RaycastHit hit;
		if(Physics.Raycast(player.transform.position, direction, out hit))
		{
			if (hit.collider.name.Contains("Batch_3"))
			{
				renderer.sharedMaterial = materials[2];
				GetComponent<Light>().color = Color.red;
			}
			else
			{
				renderer.sharedMaterial = materials[1];
				GetComponent<Light>().color = Color.green;
			}

			targetPoint = hit.point;
			targetObject = hit.collider.gameObject;
		}
		else
		{
			targetPoint = new Vector3(-10000f, -10000f, -10000f);
			targetObject = null;
			renderer.sharedMaterial = materials[0];
			GetComponent<Light>().intensity = 0;
		}
		transform.position = targetPoint;
	}

	void OnGUI() 
	{
		GUI.contentColor = Color.yellow;
		if(targetObject != null)
			GUI.Label(new Rect(10f, 10f, 250f, 200f), "<size=20>" + targetObject.name + "\n" + targetPoint + "</size>");
	}

	Vector3 GetTargetPoint()
	{
		return targetPoint;
	}

	GameObject GetTargetObject()
	{
		return targetObject;
	}
}
