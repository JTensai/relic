using UnityEngine;
using System.Collections;

public class PlayerDeath : DeathScript {

	
    //bool deathMessage = false;
	//GUIStyle largeFont;
	
/*	// Use this for initialization
	void Start () {
		//	largeFont = new GUIStyle();
		//	largeFont.alignment = TextAnchor.UpperCenter;
		//	largeFont.fontSize = 50;
		//	largeFont.normal.textColor = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/
	
	public override void OnDeath() {
		print("Character Has Died");
        //deathMessage = true;
		StartCoroutine(RestartLevel());
		
	}
	
	IEnumerator RestartLevel() {
		yield return new WaitForSeconds(2f);
        //deathMessage = false;
		GameObject.Destroy(gameObject);
		print("Character Has Died 2 seconds passed");
		Application.LoadLevel(Application.loadedLevel);
	}
	
//	void OnGUI ( )
//	{
//		if ( deathMessage )
//		{
//			GUIStyle largeFont = new GUIStyle();
//			largeFont.alignment = TextAnchor.UpperCenter;
//			largeFont.fontSize = 50;
//			largeFont.normal.textColor = Color.red;
//			GUI.Label(new Rect (Screen.width/2-250, Screen.height/2-25, 500, 50), "You have failed :(", largeFont);
//		}
//	}
}
