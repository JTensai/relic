using UnityEngine;
using System.Collections;

public class PillarBreak : MonoBehaviour {
	
	//private GameObject kickablet = null;
	public Vector3 kickableOffset = Vector3.up * 0.1f;
	public GameObject[] PrefabList = new GameObject[4];//for kickable parts
	
	// Use this for initialization
	void Start () {

	}
	
	public void Break(){
		Instantiate (PrefabList[0], transform.position+kickableOffset, transform.rotation);
		Instantiate (PrefabList[1], collider.bounds.center+kickableOffset, transform.rotation);
		//Instantiate (PrefabList[1], cylCollider.bounds.center+kickableOffset, cylinder.transform.rotation);
		Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}
