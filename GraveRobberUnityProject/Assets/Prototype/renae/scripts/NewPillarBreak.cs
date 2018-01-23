using UnityEngine;
using System.Collections;

public class NewPillarBreak : MonoBehaviour {
	
	public Vector3 kickableOffset = Vector3.up * 0.1f;
	public GameObject[] PrefabList = new GameObject[4];//for kickable parts
	private BoxCollider boxCollider; //= GetComponentInChildren<CapsuleCollider>();
	private GameObject box;
	
	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider>();
		box = transform.gameObject;
	}
	
	public void Break(){
		Instantiate (PrefabList[0], boxCollider.transform.position+kickableOffset, box.transform.rotation);
		//Instantiate (PrefabList[1], collider.bounds.center+kickableOffset, transform.rotation);
		Instantiate (PrefabList[1], boxCollider.bounds.center+kickableOffset, box.transform.rotation);
		Destroy(this.GetComponentInParent<WalkablePillarv3>().gameObject);
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}
