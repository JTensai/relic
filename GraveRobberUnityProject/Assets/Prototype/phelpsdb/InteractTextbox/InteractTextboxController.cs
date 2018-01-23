using UnityEngine;
using System.Collections;

public class InteractTextboxController : MonoBehaviour {

	public string text;
	public float lifetime;
	public GUIText textObject;

	void Start () {
	
	}
	
	void Update () {
		lifetime -= Time.deltaTime;
		if(lifetime <= 0) {
			Destroy (this.gameObject);
		}
	}
}
