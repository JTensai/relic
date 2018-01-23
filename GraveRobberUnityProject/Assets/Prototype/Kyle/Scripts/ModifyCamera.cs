using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TriggerDetector))]
public class ModifyCamera : MonoBehaviour {

	public CameraFollow cam;

	public Vector2 max;
	public Vector2 min;

	public GameObject player;
	public GameObject target;

	private TriggerDetector detectorScript;

	// Use this for initialization
	void Start () {
		detectorScript = GetComponent<TriggerDetector>();	
		detectorScript.PlayerTriggered =true;

	}
	
	// Update is called once per frame
	void Update () {
		//when entering tile
		if ( detectorScript.TriggeredOn() ) {
			player.transform.position = target.transform.position;
			cam.maxXAndZ = max;
			cam.minXAndZ = min;
			cam.transform.position = new Vector3 (-16, 9, 0);
		}
	}
}
