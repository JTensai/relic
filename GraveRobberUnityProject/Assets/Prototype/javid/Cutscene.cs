using UnityEngine;
using System.Collections;

public class Cutscene : MonoBehaviour {

	public GameObject target;
	// Zoom in?
	//public float cutsceneFOV = 8;
	// Time to focus on object
	public float cutsceneTime = 3;
	public float cutsceneHoldTime = 2f;
	// adjust focus.
//	public Vector3 focusAdjust;
	private PlayerController _pController;
	private MovementComponent _pMovement;
	private InteractableComponent ic;
	private bool alreadyPlayed = false;


	// Use this for initialization
	void Start () {
		ic = GetComponent<InteractableComponent> ();
		if (ic != null) {
			ic.OnInteract += HandleOnInteract;
		}
		_pController = GameObject.Find ("Tesla").GetComponent<PlayerController> ();//.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
		_pMovement = _pController.GetComponent<MovementComponent> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void HandleOnInteract (InteractableInteractEventData data)
	{
		StartCoroutine(startCutscene());
	}

	void OnTriggerEnter(Collider c){
		if (!alreadyPlayed){
			if (c.gameObject.CompareTag ("Player")) {
				StartCoroutine (startCutscene());
			}
		}
	}

	public IEnumerator startCutscene (){
		alreadyPlayed = true;
		_pController.GetCharAnimator ().SetFloat ("Speed", 0);
		_pController.GetCharAnimator ().SetBool ("Jump", false);
		_pMovement.stun (cutsceneTime + cutsceneHoldTime + cutsceneTime);
		CameraManagerScript cms = GameObject.Find("CameraManager").GetComponent<CameraManagerScript>();
		
		//cms.getCurrentCamera();
		//cms.setCutsceneFOV (cutsceneFOV);

		cms.setCutsceneTarget (target, cutsceneTime, cutsceneHoldTime);
		yield return new WaitForSeconds(cutsceneTime + cutsceneHoldTime);
		//cms.setCutsceneTarget (null, 0f, 0f);
	}



}
