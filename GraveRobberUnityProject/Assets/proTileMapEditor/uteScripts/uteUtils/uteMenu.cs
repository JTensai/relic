using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class uteMenu : MonoBehaviour {
#if UNITY_EDITOR
	public bool RunInBackground = true;
	private bool isShowMyMaps;
	private bool isShowMyPatterns;
	private bool isShowMenu;
	private string newMapName;
	private string newPatternName;
	private Vector2 scrollPosMaps = Vector2.zero;
	private Vector2 scrollPosPatterns = Vector2.zero;
	private GUISkin ui;
	private bool isConfirmingDeleteMap;
	private bool isConfirmingDeletePattern;
	private string currentMapToDelete;
	private string currentPatternToDelete;

	private void Start()
	{
		Application.runInBackground = RunInBackground;

		isConfirmingDeleteMap = false;
		isConfirmingDeletePattern = false;
		currentMapToDelete = "";
		currentPatternToDelete = "";
		isShowMenu = true;
		isShowMyPatterns = false;
		isShowMyMaps = true;
		newMapName = "myMap01";
		newPatternName = "myPattern01";
		ui = (GUISkin) Resources.Load("uteForEditor/uteUI");
	}

	private void OnGUI()
	{
		if(!isShowMenu)
			return;

		GUI.skin = ui;

		GUI.Box(new Rect(40,50,200,200),"Menu");

		if(isConfirmingDeleteMap)
		{
			GUI.Box(new Rect(240,50,320,500),"My MAPS");
			GUI.Label(new Rect(250,80,310,44),"Do you really want to delete "+currentMapToDelete+" map?");

			if(GUI.Button(new Rect(260,120,80,40),"Yes"))
			{
				DeleteMap(currentMapToDelete,true);
				isConfirmingDeleteMap = false;
			}

			if(GUI.Button(new Rect(350,120,80,40),"No"))
			{
				isConfirmingDeleteMap = false;
			}

			return;
		}

		if(isConfirmingDeletePattern)
		{
			GUI.Box(new Rect(240,50,320,500),"My PATTERNS");
			GUI.Label(new Rect(250,80,310,44),"Do you really want to delete "+currentMapToDelete+" pattern?");

			if(GUI.Button(new Rect(260,120,80,40),"Yes"))
			{
				DeleteMap(currentPatternToDelete,false);
				isConfirmingDeletePattern = false;
			}

			if(GUI.Button(new Rect(350,120,80,40),"No"))
			{
				isConfirmingDeletePattern = false;
			}

			return;
		}

		if(GUI.Button(new Rect(50,90,180,40),"My MAPS"))
		{
			isShowMyMaps = true;
			isShowMyPatterns = false;
		}

		if(GUI.Button(new Rect(50,140,180,40),"My PATTERNS"))
		{
			isShowMyPatterns = true;
			isShowMyMaps = false;
		}

		if(isShowMyMaps)
		{
			GUI.Box(new Rect(240,50,320,500),"My MAPS");
			newMapName = GUI.TextField(new Rect(250,75,150,30),newMapName);

			if(GUI.Button(new Rect(410,75,80,30),"Create"))
			{
				if(CreateNewMap(newMapName))
				{
					InitMapEditorEngine(false,true,newMapName,false);
				}
			}

			List<string> maps = uteMapDatabase.GetSingleton().EnumerateMaps();

			if(maps.Count>0)
			{
				scrollPosMaps = GUI.BeginScrollView(new Rect(240, 120, 310, 420), scrollPosMaps, new Rect(240, 120, 290, maps.Count*35));

				for(int i=0;i<maps.Count;i++)
				{
					if(GUI.Button(new Rect(250,120+(i*35),180,30),maps[i]))
					{
						InitMapEditorEngine(false,true,maps[i],true);
					}

					if(GUI.Button(new Rect(440,120+(i*35),90,30),"Delete"))
					{
						currentMapToDelete = maps[i];
						isConfirmingDeleteMap = true;
					}
				}

				GUI.EndScrollView();
			}
		}
		else if(isShowMyPatterns)
		{
			GUI.Box(new Rect(240,50,320,500),"My PATTERNS");
			newPatternName = GUI.TextField(new Rect(250,75,150,30),newPatternName);

			if(GUI.Button(new Rect(410,75,80,30),"Create"))
			{
				if(CreateNewPattern(newPatternName))
				{
					InitMapEditorEngine(true,false,newPatternName,false);
				}
			}
			
			List<string> patterns = uteMapDatabase.GetSingleton().EnumeratePatterns();

			if(patterns.Count>0)
			{
				scrollPosPatterns = GUI.BeginScrollView(new Rect(240, 120, 310, 420), scrollPosPatterns, new Rect(240, 120, 290, patterns.Count*35));

				for(int i=0;i<patterns.Count;i++)
				{
					if(GUI.Button(new Rect(250,120+(i*35),180,30),patterns[i]))
					{
						InitMapEditorEngine(true,false,patterns[i],true);
					}

					if(GUI.Button(new Rect(440,120+(i*35),90,30),"Delete"))
					{
						currentPatternToDelete = patterns[i];
						isConfirmingDeletePattern = true;
					}
				}

				GUI.EndScrollView();
			}
		}
	}

	private void InitMapEditorEngine(bool _isItNewPattern, bool _isItNewMap, string name, bool isLoad)
	{
		uteMapEditorEngine uteMEE = this.gameObject.AddComponent<uteMapEditorEngine>();
		uteMEE.isItNewPattern = _isItNewPattern;
		uteMEE.isItNewMap = _isItNewMap;
		uteMEE.isItLoad = isLoad;
		uteMEE.newProjectName = name;
		StartCoroutine(uteMEE.uteInitMapEditorEngine());
		HideMenu();
	}

	public void HideMenu()
	{
		isShowMenu = false;
	}

	public void ShowMenu()
	{
		isShowMenu = true;
	}

	private string FilterName(string name)
	{
		if(name.Contains(" ")||name.Contains("/")||name.Contains("\"")||name.Contains(":")||name.Contains("$")||name.Contains("."))
		{
			Debug.Log("Warning: empty spaces or symbols: \" : / $ : . are forbiden. They will be stripped.");
		}

		name = name.Replace(" ","");
		name = name.Replace("/","");
		name = name.Replace("\"","");
		name = name.Replace(":","");
		name = name.Replace("$","");
		name = name.Replace(".","");

		return name;
	}

	private bool CreateNewMap(string name)
	{
		name = FilterName(name);

		if(!CheckIfMapExists(name)&&!name.Equals(""))
		{
			uteMapDatabase.GetSingleton().CreateNewMap(name);

			return true;
		}
		else
		{
			Debug.Log("Error: Map with this name already exists or map name is empty.");

			return false;
		}
	}

	private bool CreateNewPattern(string name)
	{
		name = FilterName(name);

		if(!CheckIfPatternExists(name))
		{
			uteMapDatabase.GetSingleton().CreateNewPattern(name);

			return true;
		}
		else
		{
			Debug.Log("Error: Pattern with this name already exists.");

			return false;
		}
	}

	private bool CheckIfMapExists(string name)
	{
		return uteMapDatabase.GetSingleton().DoesMapExist(name);
	}

	private bool CheckIfPatternExists(string name)
	{
		return uteMapDatabase.GetSingleton().DoesPatternExist(name);
	}

	private void DeleteMap(string name, bool isItMap)
	{
		if (isItMap)
		{
			uteMapDatabase.GetSingleton().DeleteMap(name);
		}
		else
		{
			uteMapDatabase.GetSingleton().DeletePattern(name);
		}
	}
#endif
}
