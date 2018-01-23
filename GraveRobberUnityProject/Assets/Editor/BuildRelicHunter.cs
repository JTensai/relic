using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class Build {

	enum Target {
		PC,
		MAC
	}

	/*********************
	 * TOP MENU INTERFACE
	 *********************/

	//TODO: refactor some of this stuff.
	//There's too much copying and pasting and it will get worse with more options.

	//Add a shortcut shift-alt-p: [MenuItem ("Build/For PC #&p")]

	/*
	//Menu item: [MenuItem ("Build Relic Hunter/Official Release/For PC")]
	public static void menu_buildOfficialForPC() {

		string destination = chooseDestinationPath("RelicHunter-official");
		string[] OFFICIAL_SCENES = {"Assets/Prototype/henry/OFFICIAL_SCENE.unity"};
		
		Debug.Log("Building official release for PC...");
		BuildPipeline.BuildPlayer(OFFICIAL_SCENES, destination, BuildTarget.StandaloneWindows, BuildOptions.None);
	}
	*/

	[MenuItem ("Build Relic Hunter/UnOfficial Release/For PC")]
	public static void menu_buildUnOfficialForPC() {
		
		string destination = chooseDestinationPath("RelicHunter-unofficial", Target.PC);

		string[,] temp = readInLevelList.getLevelList ();

		string[] OFFICIAL_SCENES = new string[temp.GetLength(0)];
		for(int i= 0;i<temp.GetLength(0);i++){
			OFFICIAL_SCENES[i] = temp[i,0];
		}

		/*string[] OFFICIAL_SCENES = {
			// Utility
			"Assets/Scenes/MainMenuScene.unity", 
			"Assets/Scenes/LevelSelectScene.unity", 
			"Assets/Scenes/VictoryScene.unity",
			// Levels
			"Assets/Prototype/jordan/SmallCompilation.unity", 
			"Assets/Prototype/Matthew/Compilation.unity",
			"Assets/Prototype/javid/TempleRun.unity",
			"Assets/Prototype/javid/RelicRoomBridgePuzzle2.unity",
			"Assets/Prototype/JoeGremlich/PusherPullerAwesome.unity",
		};*/
		
		Debug.Log("Building official release for PC...");
		buildForPC(OFFICIAL_SCENES, destination);
	}
	
	[MenuItem ("Build Relic Hunter/UnOfficial Release/For Mac")]
	public static void menu_buildUnOfficialForMac() {
		
		string destination = chooseDestinationPath("RelicHunter-unofficial", Target.MAC);
		
		string[,] temp = readInLevelList.getLevelList ();
		string[] OFFICIAL_SCENES = new string[temp.GetLength(0)];
		for(int i= 0;i<temp.GetLength(0);i++){
			OFFICIAL_SCENES[i] = temp[i,0];
		}

		Debug.Log("Building official release for Mac...");
		buildForPC(OFFICIAL_SCENES, destination);
	}

	[MenuItem ("Build Relic Hunter/Just This Scene/For PC")]
	public static void menu_buildCurrentForPC() {
		
		string currentScene = EditorApplication.currentScene;
		string destination = chooseDestinationPath(getCurrentSceneName(), Target.PC);
		string[] scenes = {currentScene};
		
		Debug.Log("Building the current scene for PC...");
		buildForPC(scenes, destination);
	}
	
	[MenuItem ("Build Relic Hunter/Just This Scene/For Mac")]
	public static void menu_buildCurrentForMac() {
		
		string currentScene = EditorApplication.currentScene;
		string destination = chooseDestinationPath(getCurrentSceneName(), Target.MAC);
		string[] scenes = {currentScene};
		
		Debug.Log("Building the current scene for Mac...");
		buildForMac(scenes, destination);
	}

	/*************************
	 * COMMAND LINE INTERFACE
	 *************************/

	//TODO: write some functions that we can run from the command line, so we don't need to waste time opening Unity!
	//See: http://docs.unity3d.com/Manual/CommandLineArguments.html

	/***********
	 * BUILDING
	 ***********/

	private static void buildForPC(string[] scenes, string destination) {
		BuildPipeline.BuildPlayer(scenes, destination, BuildTarget.StandaloneWindows, BuildOptions.None);
	}

	private static void buildForMac(string[] scenes, string destination) {
		BuildPipeline.BuildPlayer(scenes, destination, BuildTarget.StandaloneOSXIntel, BuildOptions.None);
	}

	/**********
	 * HELPERS
	 **********/

	private static string chooseDestinationPath(string defaultName, Target buildTarget) {

		string extension;
		if (buildTarget == Target.MAC) {
			extension = "app";
		} else {
			extension = "exe";
		}

		defaultName = defaultName + "-" + getDateString () + "." + extension;

		string destination = EditorUtility.SaveFilePanel("Build Destination", "", defaultName, extension);
		if (destination.Length == 0) {
			//Debug.LogError("No build destination specified.");
			throw new System.Exception("No build destination specified.");
		}

		return destination;
	}

	private static string getCurrentSceneName() {
		string[] pathParts = EditorApplication.currentScene.Split(char.Parse("/"));

		string currentSceneName = pathParts[pathParts.Length - 1];

		if (currentSceneName.Contains(".unity")) {
			string[] filenameParts = currentSceneName.Split(new string[]{".unity"}, StringSplitOptions.None);
			currentSceneName = filenameParts[0];
		}

		Debug.Log ("Current scene name: " + currentSceneName);
		return currentSceneName;
	}

	private static string getDateString() {
		return string.Format("{0:yyyy-MM-dd}", DateTime.Now);
	}
}
