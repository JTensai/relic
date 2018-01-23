using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TriggerDetector))]
public class CameraCommand : Triggerable {

	public float AngleOnEnter= 80;
	public float AngleOnExit = 60;
	public float time = 1f;
	public float yMod = 0;
	public float zMod = 0;

	private Camera mainCam;
	private TriggerDetector detectorScript;
	
	// Use this for initialization
	void Start () {
		detectorScript = GetComponent<TriggerDetector>();
		detectorScript.PlayerTriggered = true;
		mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		//when touching switch
		if ( detectorScript.TriggeredOn() ) {
			iTween.RotateTo(mainCam.gameObject, new Vector3(AngleOnEnter, 0, 0), time);
			CameraFollow cf = mainCam.GetComponentInParent<CameraFollow>();
			cf.minXAndZ.y += 1;
			cf.maxXAndZ.y += 1;
			Vector3 pos = mainCam.transform.position;
			pos.y -= yMod;
			pos.z -= zMod;
			iTween.MoveTo(mainCam.gameObject, pos, time);
		}
		//when nothing on switch
		if ( detectorScript.TriggeredOff() ) {
			iTween.RotateTo(mainCam.gameObject, new Vector3(AngleOnExit, 0, 0), time);
			CameraFollow cf = mainCam.GetComponentInParent<CameraFollow>();
			cf.minXAndZ.y -= 1;
			cf.maxXAndZ.y -= 1;
			Vector3 pos = mainCam.transform.position;
			pos.y += yMod;
			pos.z -= zMod;
			iTween.MoveTo(mainCam.gameObject, pos, time);
		}
	}
	
}