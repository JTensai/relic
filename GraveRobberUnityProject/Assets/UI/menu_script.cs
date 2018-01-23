using UnityEngine;
using System.Collections;

public class menu_script : MonoBehaviour {

	private MainMenuController _mainMenuController;

//	public int lvl_index = 0;
/*	private string[] levels = new string[]{
		"MainMenuScene",
		"LevelSelectScene",
		"VictoryScene",
		"SmallCompilation",
		"Compilation", 
		"TempleRun", 
		"RelicRoomBridgePuzzle2" , 
		"PusherPullerAwesome",
	};*/

	public string[,] levelData;

	// Use this for initialization
	void Start () {
		levelData = readInLevelList.getLevelList ();
		_mainMenuController = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void jumpLevel () {
	//	Debug.Log (lvl_index);
	//	Debug.Log (levels.Length);

//		MainMenuController controller = getController();
//		if (controller != null) {
//			controller.LoadLevel(levelData[lvl_index,1]);
//		} else {
//			Application.LoadLevel(levelData[lvl_index,1]);
//		}
	}

	public void exit_game() {
		MainMenuController controller = getController();
		if (controller != null) {
			controller.Quit();
		} else {
			//No special controller, so just quit now.
			Application.Quit();
		}
	}

	public MainMenuController getController() {
		if (_mainMenuController == null){
			MainMenuController findTry = GameObject.FindObjectOfType<MainMenuController>();
			if (findTry != null) {
				_mainMenuController = findTry;
			} else {
				Debug.LogWarning("Cannot locate MainMenuController script");
			}
		}

		return _mainMenuController;
	}
	
}
