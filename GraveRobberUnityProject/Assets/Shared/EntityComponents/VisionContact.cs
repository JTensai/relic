using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionContact : VisionBase {

	private HashSet<GameObject> seenColliders = new HashSet<GameObject>();
	private Dictionary<GameObject, int> seenControllers = new Dictionary<GameObject, int>();

	private static readonly int controllerPersistTime = 3;

	// Use this for initialization
	void Start () {
	
	}

	void Update() {
		Dictionary<GameObject, int> nextSeenControllers = new Dictionary<GameObject, int>();

		foreach (GameObject controlerObject in seenControllers.Keys)
		{
			if (seenControllers[controlerObject] != 1)
			{
				nextSeenControllers[controlerObject] = seenControllers[controlerObject] - 1;
			}
		}

		seenControllers = nextSeenControllers;
	}

	public void OnCollisionStay(Collision collision)
	{
		seenColliders.Add(collision.gameObject);
	}

	public void OnCollisionEnter(Collision collision)
	{
		seenColliders.Add(collision.gameObject);
	}

	public void OnExtendedControllerHitStay(ExtendedControllerColliderHit hit)
	{
		seenControllers[hit.Controller.gameObject] = controllerPersistTime;
	}

	#region implemented abstract members of VisionBase
	
	public override void DrawGizmos ()
	{

	}
	
	public override GameObject[] ObjectsInVision ()
	{
		HashSet<GameObject> collisions = new HashSet<GameObject>();
		collisions.UnionWith(seenColliders);
		collisions.UnionWith(seenControllers.Keys);

		GameObject[] objects = new GameObject[collisions.Count];
		int counter = 0;
		foreach (GameObject c in collisions) {
			// Debug.Log("I have seen " + c.name);
			objects[counter] = c;
			counter++;
		}
		return objects;
	}

	public override void ObjectsInVisionV2 ( HashSet<GameObject> allInVision)
	{

	}

	public override HashSet<GameObject> ObjectsInVisionV3( ){
		Debug.Log("Unimplemented");
		return null;
	}

	public override HashSet<GameObject> ObjectsInVisionV4 ()
	{
		return new HashSet<GameObject>();
	}

	public override HashSet<GameObject> LastFrameObjectsInVision ()
	{
		return new HashSet<GameObject>();
	}

	public override HashSet<GameObject> ObjectsLeftVision ()
	{
		return new HashSet<GameObject>();
	}
	#endregion
}
