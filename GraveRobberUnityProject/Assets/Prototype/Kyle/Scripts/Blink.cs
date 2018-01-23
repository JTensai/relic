using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {

	private int cnt = 30;
	public GameObject particles;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton("Fire1") && cnt >= 30) {
			cnt = 0;
			GameObject g = (GameObject)Instantiate(particles, new Vector3(transform.position.x, 1, transform.position.z), transform.rotation);
			transform.position = new Vector3(transform.position.x, 1, transform.position.z) + transform.forward * 2;
			Destroy(g,1f);
		}
		cnt = Mathf.Clamp(cnt+1, 0, 30);
	}

}
