using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class FMOD_TEMP_TEST : MonoBehaviour {
	private EventInstance ev;
	private ParameterInstance param;
	float tolerance;
	// Use this for initialization
	void Start () {
		ev = FMOD_StudioSystem.instance.GetEvent("event:/Sliding Doors/OOH, that's a door");
		EventDescription desc;
		ev.getDescription(out desc);
		PARAMETER_DESCRIPTION paramDesc;
		desc.getParameterByIndex(0, out paramDesc);
		Debug.Log ("NAME IS: " + paramDesc.name);
		ev.getParameterByIndex(0, out param);
		ev.getParameter("Cheeseburger", out param);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			ev.start();
		}

		if(Input.GetKey(KeyCode.LeftArrow)){
			tolerance-=0.1f;
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			tolerance+=0.1f;
		}
		param.setValue(tolerance);
		ev.setParameterValueByIndex(0, tolerance);
		Debug.Log(tolerance);
	}
}
