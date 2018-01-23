using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

[AddComponentMenu("proTileMapEditor/uteMapLoader")]
[ExecuteInEditMode]
#endif
public class uteMapLoader : MonoBehaviour {

	[SerializeField]
	public bool LoadAuto=true;
	[SerializeField]
	public bool StaticBatching=true;
	[SerializeField]
	public bool AddMeshColliders=false;
	[SerializeField]
	public bool RemoveLeftovers=true;
	[SerializeField]
	public bool PrepareForLightmapping=false;
	[SerializeField]
	public Vector3 MapOffset = new Vector3(0,0,0);
	[SerializeField]
	public Vector3 MapScale = new Vector3(1,1,1);
	[HideInInspector]
	public Dictionary<string, GameObject> refTiles = new Dictionary<string, GameObject>();
	[HideInInspector]
	public string mapName;
	[HideInInspector]
	public bool isMapLoaded;

	public Vector3 loadMapOffset
	{
		get { return MapOffset; }
		set
		{
			if(MapOffset == value) return;

			MapOffset = value;
		}
	}

	public Vector3 loadMapScale
	{
		get { return MapScale; }
		set
		{
			if(MapScale == value) return;

			MapScale = value;
		}
	}

	public bool loadAutoVal
    {
        get { return LoadAuto; }
        set
        {
            if (LoadAuto == value) return;
 
            LoadAuto = value;
        }
    }

    public bool loadStaticBatching
    {
    	get { return StaticBatching; }
    	set
    	{
    		if(StaticBatching==value) return;

    		StaticBatching = value;
    	}
    }

    public bool loadAddMeshColliders
    {
    	get { return AddMeshColliders; }
    	set
    	{
    		if(AddMeshColliders==value) return;

    		AddMeshColliders = value;
    	}
    }

    public bool loadRemoveLeftovers
    {
    	get { return RemoveLeftovers; }
    	set
    	{
    		if(RemoveLeftovers==value) return;

    		RemoveLeftovers = value;
    	}
    }

    public bool loadPrepareForLightmapping
    {
    	get { return PrepareForLightmapping; }
    	set
    	{
    		if(PrepareForLightmapping==value) return;

    		PrepareForLightmapping = value;
    	}
    }

    private string myLatestMap = "";
	
	private GameObject GetPrefab(string guid)
	{
		if (refTiles.ContainsKey(guid))
		{
			return refTiles[guid];
		}
		else
		{
			return null;
		}
	}

	#if UNITY_EDITOR
	[SerializeField]
	[HideInInspector]
	public int currentMapIndex;
	[HideInInspector]
	public string currentMapName;

	public int myMapIndexVal
    {
        get { return currentMapIndex; }
        set
        {
            if (currentMapIndex == value) return;
 
            currentMapIndex = value;
        }
    }

	private void PreLoadPrefab(string guid)
	{
		if (!refTiles.ContainsKey(guid))
		{
			string opath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
			GameObject result = (GameObject) UnityEditor.AssetDatabase.LoadMainAssetAtPath(opath);
			refTiles[guid] = result;
		}
	}

    public void SetMap(string name)
    {
    	isMapLoaded = false;
    	currentMapName = name;
    	mapName = name;

		string fileBase = uteGLOBAL3dMapEditor.getMapsDir()+name;
		string filename = null;

		if (File.Exists(fileBase + ".xml"))
		{
			filename = fileBase + ".xml";
		}
		else
		{
			filename = fileBase + ".txt";
		}

		StreamReader sr = new StreamReader(filename);
		myLatestMap = sr.ReadToEnd();
    	sr.Close();
		
		uteMapDefinition mapDefinition = uteMapDefinitionLoader.LoadDefinition(myLatestMap);
		
		refTiles = new Dictionary<string, GameObject>();

		for(int i=0;i<mapDefinition.TileCount;i++)
		{
			PreLoadPrefab(mapDefinition.GetTile(i).PrefabGUID);
		}
    }

    private List<string> RemoveDuplicates(List<string> myList)
    {
        List<string> newList = new List<string>();

        for(int i=0;i<myList.Count;i++)
            if (!newList.Contains(myList[i].ToString()))
                newList.Add(myList[i].ToString());

        return newList;
    }

	#endif

	private void Awake()
	{
		#if UNITY_EDITOR
		if(EditorApplication.isPlaying)
		{
		#endif
			if(LoadAuto)
			{
				LoadMap();
			}
		#if UNITY_EDITOR
		}
		#endif
	}

	public void LoadMap()
	{
		StartCoroutine(_LoadMapAsync(2000));
	}

	private IEnumerator _LoadMapAsync(int frameSkip)
	{
		#if UNITY_EDITOR
		Debug.Log("Loading Map... (This message appears only in the Editor)");
		#endif

		GameObject MAP = new GameObject(mapName);
		GameObject MAP_S = new GameObject("STATIC");
		GameObject MAP_D = new GameObject("DYNAMIC");
		MAP_S.transform.parent = MAP.transform;
		MAP_D.transform.parent = MAP.transform;

		uteMapDefinition mapDefinition = uteMapDefinitionLoader.LoadDefinition(myLatestMap);

		for(int i=0;i<mapDefinition.TileCount;i++)
		{
			if(i%frameSkip==0) yield return 0;

			uteMapTileDefinition tileDef = mapDefinition.GetTile(i);

			GameObject obj = GetPrefab(tileDef.PrefabGUID);

			GameObject newObj = (GameObject) Instantiate(obj,tileDef.Position+MapOffset+new Vector3(-500,0,-500),Quaternion.identity);
			newObj.name = tileDef.PrefabGUID;
			newObj.transform.localEulerAngles = tileDef.EulerAngles + obj.transform.localEulerAngles;

			if(tileDef.IsStatic)
			{
				newObj.isStatic = true;
				newObj.transform.parent = MAP_S.transform;
			}
			else
			{
				newObj.isStatic = false;
				newObj.transform.parent = MAP_D.transform;
			}
		}

		if(StaticBatching)
		{
			uteCombineChildren batching = (uteCombineChildren) MAP_S.AddComponent<uteCombineChildren>();
			batching.Batch(AddMeshColliders,RemoveLeftovers,false,PrepareForLightmapping);
		}

		MAP_S.transform.localScale = MapScale;
		MAP_D.transform.localScale = MapScale;

		isMapLoaded = true;

		#if UNITY_EDITOR
		Debug.Log("Map LOADED! (This message appears only in the Editor)");
		#endif

		yield return 0;
	}

	public void LoadMapAsync(int frameSkip = 5)
	{
		StartCoroutine(_LoadMapAsync(frameSkip));
	}
}
