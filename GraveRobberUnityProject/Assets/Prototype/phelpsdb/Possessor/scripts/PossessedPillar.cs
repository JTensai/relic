using UnityEngine;
using System.Collections;

public class PossessedPillar : MonoBehaviour {

	public float tipForce = 3000f;
	private VisionArc fallVision;
	private Rigidbody rbCollider;
	private bool tipped = false;

	// Use this for initialization
	void Start () {
		fallVision = gameObject.AddComponent<VisionArc>();
		fallVision.VisionVariant = VisionEnum.Variant1;
		fallVision.ArcSize = 360f;
		fallVision.Distance = 5f;
		fallVision.CanSeeThroughWalls = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(tipped) return;
		foreach(GameObject obj in fallVision.PlayersInVision()) {
			Vector3 toPlayer = obj.transform.position - gameObject.transform.position;
			toPlayer.y = 0f;
			toPlayer.Normalize();
			toPlayer *= tipForce;
			gameObject.rigidbody.AddForce(toPlayer);
			tipped = true;
		}
	}
}
