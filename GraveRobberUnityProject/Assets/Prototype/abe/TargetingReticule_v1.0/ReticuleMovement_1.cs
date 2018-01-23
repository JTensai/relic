using UnityEngine;
using System.Collections;

public class ReticuleMovement_1 : MonoBehaviour 
{
	public float reticuleMovementSpeed;
	public Material[] materials;

	private float xMin = -2.479499f;
	private float xMax = 2.4885f;
	private float yMin = 3.125876f;
	private float yMax = 4.992599f;
	public Camera cameraRenamed;
	private bool moved;
	private float prevSide;
	private float prevUp;
	private Vector3 targetPoint;
	private GameObject targetObject;

	// Use this for initialization
	void Start () 
	{
		Screen.showCursor = false;
		//camera = GameObject.Find("Main Camera").camera;
		moved = false;
		targetPoint = new Vector3(-10000f, -10000f, -10000f);
		targetObject = null;
	}

	// Update is called once per frame
	void Update () 
	{
		Screen.showCursor = false;
		MoveReticule();

		if(moved)
			FindIntersectionPoint();
	}

	void MoveReticule()
	{
		//Get mouse movements
		float newSide = Input.GetAxis ("Mouse X");
		float newUp = Input.GetAxis ("Mouse Y");
		float side = Input.GetAxis ("Mouse X") * reticuleMovementSpeed;
		float up = Input.GetAxis ("Mouse Y") * reticuleMovementSpeed;

		if(Mathf.Abs(newSide-prevSide) > 0.0f || Mathf.Abs(newUp-prevUp) > 0.0f)
			moved = true;
		prevUp = newUp;
		prevSide = newSide;

		//Check to keep movements within the screen
		if(transform.position.x+side <= xMin)
			side =  xMin - transform.position.x;
		else if(transform.position.x+side >= xMax)
			side = xMax - transform.position.x;
		if(transform.position.y+up <= yMin)
			up = yMin - transform.position.y;
		else if(transform.position.y+up >= yMax)
			up = yMax - transform.position.y;

		//Move cursor
		transform.Translate(side, 0.0f, up);
	}

	//Cast ray, find both the intersected object and the point of intersection on that object...ONLY CALL WHEN MOUSE MOVED ACCORDING TO SOME MIN?
	void FindIntersectionPoint()
	{
		moved = false;
		Vector3 direction = transform.position - cameraRenamed.transform.position;
		RaycastHit hit;
		if(Physics.Raycast(transform.position, direction, out hit))
		{
			if(hit.collider.name.Contains("Enemy"))
				renderer.sharedMaterial = materials[1];
			else if (hit.collider.name.Contains("walls"))
				renderer.sharedMaterial = materials[0];
			else
				renderer.sharedMaterial = materials[2];

			targetPoint = hit.point;
			targetObject = hit.collider.gameObject;
			Debug.Log (targetObject.name + ":" + targetPoint);

		}
		else
		{
			targetPoint = new Vector3(-10000f, -10000f, -10000f);
			targetObject = null;
			renderer.sharedMaterial = materials[2];
		}
	}

	void OnGUI() 
	{
		GUI.contentColor = Color.yellow;
		GUI.Label(new Rect(10f, 10f, 150f, 200f), "<size=20>" + targetObject.name + "\n" + targetPoint + "</size>");
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
