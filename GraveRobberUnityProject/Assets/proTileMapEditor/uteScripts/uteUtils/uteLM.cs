using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class uteLM : MonoBehaviour {
	#if UNITY_EDITOR
	[HideInInspector]
	public uteMapEditorEngine uMEE;
	[HideInInspector]
	public bool isMapLoaded;

	public IEnumerator LoadMap(string name, GameObject map_static, GameObject map_dynamic, bool isItMap)
	{
		isMapLoaded = false;
		string fileBase;
		string path;

		if(isItMap)
		{
			fileBase = uteGLOBAL3dMapEditor.getMapsDir()+name;
		}
		else
		{
			fileBase = uteGLOBAL3dMapEditor.getPatternsDir()+name;
		}

		if (File.Exists(fileBase + ".xml"))
		{
			path = fileBase + ".xml";
		}
		else
		{
			path = fileBase + ".txt";
		}

		StreamReader sr = new StreamReader(path);
		string info = sr.ReadToEnd();
		sr.Close();

		uteMapDefinition mapDefinition = uteMapDefinitionLoader.LoadDefinition(info);

		for(int i=0;i<mapDefinition.TileCount;i++)
		{
			if(i%2000==0) yield return 0;

			uteMapTileDefinition tileDefinition = mapDefinition.GetTile(i);

			string opath = UnityEditor.AssetDatabase.GUIDToAssetPath(tileDefinition.PrefabGUID);
			GameObject tGO = (GameObject) UnityEditor.AssetDatabase.LoadMainAssetAtPath(opath);

			if(tGO)
			{
				GameObject newGO = (GameObject) Instantiate(tGO,Vector3.zero,Quaternion.identity);
				List<GameObject> twoGO = new List<GameObject>();
				twoGO = uMEE.createColliderToObject(newGO,newGO);
				GameObject behindGO = (GameObject) twoGO[0];
				GameObject objGO = (GameObject) twoGO[1];
				newGO = objGO;
				behindGO.name = tGO.name;
				newGO.name = tGO.name;
				behindGO.layer = 0;
				behindGO.transform.position = tileDefinition.Position;
				behindGO.transform.localEulerAngles = tileDefinition.EulerAngles + tGO.transform.localEulerAngles;
				behindGO.collider.isTrigger = false;
				uteTagObject uTO = behindGO.AddComponent<uteTagObject>();
				uTO.objGUID = tileDefinition.PrefabGUID;
				
				if(tileDefinition.IsStatic)
				{
					newGO.isStatic = true;
					uTO.isStatic = true;
					behindGO.transform.parent = map_static.transform;
				}
				else
				{
					newGO.isStatic = false;
					uTO.isStatic = false;
					behindGO.transform.parent = map_dynamic.transform;
				}

				if(tileDefinition.IsTileConnected)
				{
					uTO.isTC = true;
					uteTcTag uTT = (uteTcTag) behindGO.AddComponent<uteTcTag>();
					uTT.tcFamilyName = tileDefinition.ConnectionFamily;
				}

				uteGLOBAL3dMapEditor.mapObjectCount++;
			}
		}

		isMapLoaded = true;
		
		yield return 0;
	}
	#endif
}
