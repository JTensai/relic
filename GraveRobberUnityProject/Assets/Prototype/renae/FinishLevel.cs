using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FinishLevel : MonoBehaviour {

	private BoxCollider finishBox;
	private GameHUDManager GameHUD;

	// Use this for initialization
	void Start () {
		this.renderer.enabled = false;
		if (GameObject.FindGameObjectWithTag ("GameHUD") != null)
		{
			GameHUD = GameObject.FindGameObjectWithTag ("GameHUD").GetComponent<GameHUDManager> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") == true)
        {
			if (GameHUD != null){
				GameHUD.ShowVictoryScreen();
			}
			else{
				// should never actually get here
				if (Application.isEditor){
#if UNITY_EDITOR
					EditorApplication.isPlaying = false;
#endif
				}
				else{
					//No special controller, so just quit now.
					Application.Quit();
				}
				Application.LoadLevel("VictoryScene");
			}
        }
	}

	public void Activate(){
		Debug.Log ("FinishLevel is now active"); 
		finishBox = (BoxCollider)this.gameObject.AddComponent ("BoxCollider");
		finishBox.enabled = true;
		finishBox.isTrigger = true;
	}
}
