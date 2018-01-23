using UnityEngine;
using System.Collections;

public class MinimapController : MonoBehaviour {
	
	public Texture aTexture;
	public Camera minimap;

	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<PlayerBase>().gameObject;

		//The minimap should not listen for audio.
		disableAudioListener();
	}
	
	// Update is called once per frame
	void Update () {
		minimap.transform.position = new Vector3(player.transform.position.x, 40, player.transform.position.z);
	}
 	
	public void disableAudioListener() {
		AudioListener aListener = this.GetComponent<AudioListener> ();
		if (aListener != null) {
			aListener.enabled = false;
		}
	}
}
