using UnityEngine;
using System.Collections;

public class Possessor : MonsterBase {
	private VisionArc outerVision;
	private VisionArc possessVision;
	public MovementComponent mover;
	public float retreatSpeed = 5;
	public GameObject possessTarget;
	
	// Use this for initialization
	void Start () {
		outerVision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);
		possessVision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant3, gameObject);
		mover = gameObject.GetComponent<MovementComponent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(possessTarget != null) {
			Possess();
			return;
		}
		//foreach(GameObject obj in innerVision.ObjectsInVision()) {
		//	if(obj.GetComponent<PillarTop>() != null && obj.GetComponent<PossessedPillar>() == null) {
		//		possessTarget = obj;
		//		return;
		//	}
		//}
		ArrayList evasiveManeuvers = new ArrayList(); // a list of vectors that point in the opposite direction of other characters
		foreach(GameObject obj in outerVision.ObjectsInVision()) {
			if(obj.GetComponent<PlayerBehavior>() != null || obj.GetComponent<HealthComponent>() != null) {
				evasiveManeuvers.Add(InvertDistanceIntensity(gameObject.transform.position - obj.transform.position));
			}
		}
		Vector3 finalVec = new Vector3();
		foreach(Vector3 vec in evasiveManeuvers) {
			finalVec += vec;
		}
		mover.Move(0, (finalVec).normalized * Time.deltaTime * retreatSpeed);
	}
	
	// Returns a higher vector for closer characters within the outerVision 
	// Returns a lower vector for further characters within the outerVision
	Vector3 InvertDistanceIntensity(Vector3 inputVec) {
		Vector3 outVec = new Vector3();
		outVec.x = (outerVision.Distance - Mathf.Abs(inputVec.x)) / outerVision.Distance;
		outVec.y = 0f; // not dealing with up/down right now.
		outVec.z = (outerVision.Distance - Mathf.Abs(inputVec.z)) / outerVision.Distance;
		outVec.x *= (inputVec.x < 0) ? -1.0f : 1.0f;
		outVec.z *= (inputVec.z < 0) ? -1.0f : 1.0f;
		return outVec;
	}
	
	void Possess() {
		if(!possessTarget) return; // if the attackTarget object is removed, don't attempt to kill it anymore
		mover.Move(0, (possessTarget.transform.position - gameObject.transform.position).normalized * Time.deltaTime * retreatSpeed);
		GameObject[] targets = possessVision.ObjectsInVision();
		foreach(GameObject targ in targets) {
			if(targ == possessTarget) {
				possessTarget.AddComponent<PossessedPillar>();
				gameObject.SetActive(false);
			}
		}
	}
	
}
