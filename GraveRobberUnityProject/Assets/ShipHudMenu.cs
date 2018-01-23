using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ShipHudMenu : MonoBehaviour {
	
	private GameHUDManager GameHUD = null;
	
	// Use this for initialization
	void Start () {
		GameHUD = this.gameObject.GetComponentInParent<GameHUDManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void MainMenu()
	{
		// Load Main Menu
		// Application.loa
		
		Application.LoadLevel ("MainMenu");
	}
	
	public void PlayAgain()
	{GameHUD.HideVictoryScene ();

	}
	
	public void Resume()
	{
		GameHUD.ResumeGame ();
	}
	
	public void MissionObjective()
	{
		GameHUD.ShowMissionObjective ();
	}
	
	public void Options()
	{
		Debug.Log ("Options Button is unimplemented");
	}
	
	public void Quit()
	{Debug.Log("Quit");
		if (Application.isEditor){
			#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
			#endif
		}
		else{
			//No special controller, so just quit now.
			Application.Quit();
		}		
	}
}
