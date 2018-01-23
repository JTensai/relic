using UnityEngine;
using System.Collections;

public class generateMenuItems : MonoBehaviour {

	public string[,] levelData;

	// Use this for initialization
	void Start () {

		// path, name, Colorful name
		/*string[,] levelData = new string[,]
		{
			{"Assets/Scenes/MainMenuScene.unity","MainMenuScene", ""},
			{"Assets/Scenes/LevelSelectScene.unity","LevelSelectScene", ""},
			{"Assets/Scenes/VictoryScene.unity","VictoryScene", ""},
			{"Assets/Prototype/jordan/SmallCompilation.unity","SmallCompilation", "1 - The Sarcadian Sanctuary"},
			{"Assets/Prototype/Matthew/Compilation.unity","Compilation", "2 - The Nameless Catacombs"},
			{"Assets/Prototype/javid/TempleRun.unity","TempleRun", "S1 - Temple Run"},
			{"Assets/Prototype/javid/RelicRoomBridgePuzzle2.unity","RelicRoomBridgePuzzle2", "S2 - Light Bridge Puzzle"},
			{"Assets/Prototype/JoeGremlich/PusherPullerAwesome.unity","PusherPullerAwesome", "S3 - Pusher! Puller! Awesome!"},
		};*/

		levelData = readInLevelList.getLevelList ();

	//	Debug.Log (levelData.GetLength(0));
	
		int num = levelData.GetLength(0) - 3; //utility scenes
		for (int i =0; i<num; i++) {
			GameObject s2 = (GameObject)Instantiate (GameObject.Find ("SpriteTemplate"));
						s2.transform.parent = GameObject.Find ("SpriteRoot").transform;
						s2.transform.localScale = new Vector3 (1f, 1f, 1f);
						s2.transform.localPosition = new Vector3(4,-33f +i*-100,0 );
			s2.GetComponentInChildren<UILabel>().text = levelData[i+3,2];
//			s2.GetComponent<menu_script>().lvl_index = i+3;
		//	s2.GetComponent<menu_script>().levelToLoad = levelData[i+3,1]; //lvl_index = i+3;
			Debug.Log (levelData[i+3,1]);
		}
		GameObject.Find("SpriteTemplate").SetActive(false);

		GameObject.Find ("SpriteRoot").GetComponent<UISprite> ().SetDimensions (650, 200 * (num+1));

		GameObject.Find ("SpriteBack").transform.localPosition= new Vector3(4,-33f +(num)*-100,0 );


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
