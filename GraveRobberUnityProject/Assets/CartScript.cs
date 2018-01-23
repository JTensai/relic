using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CartScript : MonoBehaviour {



	VisionArc vision;
	bool activated;
	GameObject destination;


	Dictionary<GameObject,bool> wayPoints = new Dictionary<GameObject,bool>();
	// Use this for initialization
	void Start () {

		vision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);
		foreach (GameObject targ in vision.ObjectsInVision()) {
				if(targ.GetComponent<WaypointPath>() != null)
					{
					wayPoints.Add(targ,false);

			}
		}


	}
	
	// Update is called once per frame
	void Update () {








	
	}
}
