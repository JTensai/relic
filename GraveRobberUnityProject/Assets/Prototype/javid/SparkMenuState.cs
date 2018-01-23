using UnityEngine;
using System.Collections;

public class SparkMenuState : IState {

//	private CharacterController _charController;
//	private Vector3 adjust = new Vector3 (-.2f,-.4f,.2f);
//	private Quaternion rotation = /*new Quaternion (0f,0f,0f,1f);*/ Quaternion.Euler(0, 45, 20);
//
//	public SparkMenuState(SparkController sController)
//	{
//		_charController = sController.GetCharController ();
//	}

	public void BeginState(StateMachine stateMachine)
	{
//		Debug.Log ("SparkMenuState");
	}
	
	public void Update(StateMachine stateMachine)
	{
//		Camera c = GameObject.Find ("CameraManager").GetComponent<CameraManagerScript> ().getCurrentCamera();
//		//Debug.Log (c.transform.position);
//		//Debug.Log (c.transform.localPosition);
//		//Debug.Log (c.name);
//		//float x = Mathf.Lerp (_charController.transform.position.x, c.transform.position.x, Time.deltaTime);
//		//float y = Mathf.Lerp (_charController.transform.position.y, c.transform.position.y, Time.deltaTime);
//		//float z = Mathf.Lerp (_charController.transform.position.z, c.transform.position.z, Time.deltaTime);
//
//		_charController.transform.position = Vector3.Lerp (_charController.transform.position, c.transform.position+adjust, Time.deltaTime * 5f);
//		_charController.transform.localRotation = Quaternion.Lerp (_charController.transform.localRotation, rotation, Time.deltaTime * 4f);
//	//	_charController.transform.localRotation.SetFromToRotation (new Vector3 (0, 0, 0), c.transform.position);
		//_charController.Move (new Vector3(x, y, z));
		//_charController.Mo
	}
	
	public void EndState(StateMachine stateMachine)
	{
		
	}
}
