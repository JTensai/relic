using UnityEngine;
using System.Collections;

public class PillarBreakNew : MonoBehaviour {

	public Vector3 kickableOffset = Vector3.up * 0.1f;
	public GameObject[] PrefabList = new GameObject[4];//for kickable parts
	private CapsuleCollider cylCollider; //= GetComponentInChildren<CapsuleCollider>();
	private GameObject cylinder;
	
	// Use this for initialization
	void Start () {
		cylCollider = GetComponentInChildren<CapsuleCollider>();
		cylinder = transform.FindChild("Pivot").FindChild("Cylinder").gameObject;
	}
	
	public void Break(){
		Instantiate (PrefabList[0], cylinder.transform.position+kickableOffset, cylinder.transform.rotation);
		//Instantiate (PrefabList[1], collider.bounds.center+kickableOffset, transform.rotation);
		Instantiate (PrefabList[1], cylCollider.bounds.center+kickableOffset, cylinder.transform.rotation);
		Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}
