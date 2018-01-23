using UnityEngine;
using System.Collections;
using System.IO;

public class uteSaveMap : MonoBehaviour {
#if UNITY_EDITOR
	[HideInInspector]
	public bool isSaving;
	public uteOptionsBox options;

	private void Start()
	{
		isSaving = false;
	}

	public IEnumerator SaveMap(string mapName, bool isItMap)
	{
		isSaving = true;

		GameObject main = (GameObject) GameObject.Find("MAP");
		uteTagObject[] allObjects = main.GetComponentsInChildren<uteTagObject>();

		uteMapDefinition mapDefinition = new uteMapDefinition();

		for(int i=0;i<allObjects.Length;i++)
		{
			if(i%2000==0) yield return 0;

			uteTagObject currentObject = allObjects[i];
			string tcFamilyName = null;

			if(currentObject.GetComponent<uteTcTag>())
			{
				tcFamilyName = ((uteTcTag) currentObject.GetComponent<uteTcTag>()).tcFamilyName;
			}

			uteMapTileDefinition tileDefinition = new uteMapTileDefinition(
				currentObject.objGUID,
				currentObject.transform.position,
				currentObject.transform.localEulerAngles,
				currentObject.isStatic,
				tcFamilyName);

			mapDefinition.AddTile(tileDefinition);
		}

		string path;

		if(isItMap)
		{
			path = uteGLOBAL3dMapEditor.getMapsDir();
		}
		else
		{
			path = uteGLOBAL3dMapEditor.getPatternsDir();
		}
		
		StreamWriter xmlWriter = new StreamWriter(path+mapName+".xml");
		xmlWriter.Write("");
		xmlWriter.Write(uteMapDefinitionWriter.Write(mapDefinition, true));
		xmlWriter.Flush();
		xmlWriter.Close();
		
		SaveMapSettings(mapName);

		isSaving = false;

		yield return 0;
	}

	public void SaveMapSettings(string mapName)
	{
		string path = uteGLOBAL3dMapEditor.getMapsDir();
		GameObject MAIN = (GameObject) GameObject.Find("MAIN");
		GameObject YArea = (GameObject) GameObject.Find("MAIN/YArea");
		GameObject MapEditorCamera = (GameObject) GameObject.Find("MAIN/YArea/MapEditorCamera");

		string info = MAIN.transform.position.x+":"+MAIN.transform.position.y+":"+MAIN.transform.position.z+":"+MAIN.transform.localEulerAngles.x+":"+MAIN.transform.localEulerAngles.y+":"+MAIN.transform.localEulerAngles.z+":"+YArea.transform.localEulerAngles.x+":"+YArea.transform.localEulerAngles.y+":"+YArea.transform.localEulerAngles.z+":"+MapEditorCamera.transform.localEulerAngles.x+":"+MapEditorCamera.transform.localEulerAngles.y+":"+MapEditorCamera.transform.localEulerAngles.z+":";

		info += options.isEditorLightOn+":"+options.isCastShadows+":"+uteGLOBAL3dMapEditor.XZsnapping+":"+uteGLOBAL3dMapEditor.OverlapDetection+":"+options.isShowGrid+":"+uteGLOBAL3dMapEditor.CalculateXZPivot+":"+options.snapOnTop+":";

		StreamWriter sw = new StreamWriter(path+mapName+"_info.txt");
		sw.Write("");
		sw.Write(info);
		sw.Flush();
		sw.Close();
	}

	private float RoundToHalf(float point)
	{
		point *= 2.0f;
		point = Mathf.Round(point);
		point /= 2.0f;

		return point;
	}
#endif
}
