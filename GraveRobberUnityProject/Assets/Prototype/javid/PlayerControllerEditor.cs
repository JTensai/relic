using UnityEngine;
using System.Collections;
/*using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
	bool teleport = false;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
	//	PlayerController myScript = (PlayerController)target;
		//GUI.enabled = !teleport;

		if(GUILayout.Button("Teleport Tesla"))
		{
			teleport = !teleport;
			Debug.Log("Click to Teleport Tesla " + teleport);
		}
	}

	void OnSceneGUI()
	{
		//Debug.Log("A");
		if (teleport)
		{
		//	Debug.Log("B");
			if (Event.current.type == EventType.MouseDown)
			{
			//	Debug.Log("C");
				Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
				RaycastHit hitInfo;
				
				
				if (Physics.Raycast(worldRay, out hitInfo))
				{
					Debug.Log("D");
				//	GameObject waypoint = Resources.LoadAssetAtPath("Assets/Prefabs/Waypoint.prefab", typeof(GameObject)) as GameObject;
					//GameObject waypointInstance = Instantiate(waypoint) as GameObject;
					//waypointInstance.transform.position = hitInfo.point;
					PlayerController myScript = (PlayerController)target;
					myScript.teleportToMouse(hitInfo.point);
					Selection.activeGameObject = myScript.gameObject;
				}
				
			}
			



			if (Event.current.type == EventType.MouseUp)
			{
				teleport = false;
			}
			Event.current.Use();

		}
		
	}
}
*/