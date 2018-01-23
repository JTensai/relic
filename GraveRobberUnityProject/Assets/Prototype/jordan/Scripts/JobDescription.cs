using UnityEngine;
using System.Collections;

public class JobDescription : MonoBehaviour {

	public GameObject relicModel;
	public GameObject relicLight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowRelicExample(){
		relicModel.GetComponent<MeshRenderer> ().enabled = true;
		relicLight.GetComponent<Light> ().enabled = true;
	}
	
	public void HideRelicExample(){
		relicModel.GetComponent<MeshRenderer> ().enabled = false;
		relicLight.GetComponent<Light> ().enabled = false;
	}
}
