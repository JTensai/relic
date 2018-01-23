using UnityEngine;
using UnityEditor;

public class DuplicateMove : EditorWindow
{
	private Vector3 prevPosition;
	private bool doSnap = true;
	private float snapValue = 1;
//	private float stepValue = 1;

    [MenuItem("Edit/DuplicateMove %_t")]
	
	static void Init()
	{
        var window = (DuplicateMove)EditorWindow.GetWindow(typeof(DuplicateMove));
		window.maxSize = new Vector2( 200, 100 );
	}

	[MenuItem("Extra/Duplicate Selected Prefab")]
	public static void DuplicateSelectedPrefab()
	{
		Object prefabRoot = PrefabUtility.GetPrefabParent(Selection.activeGameObject);
		GameObject clone = PrefabUtility.InstantiatePrefab(prefabRoot) as GameObject;
		clone.transform.Translate(Selection.activeGameObject.transform.position + new Vector3(1000,1000,1000));
		clone.transform.localScale =  Selection.activeGameObject.transform.localScale;
	}
	
//	public void OnGUI()
//	{
//		doSnap = EditorGUILayout.Toggle( "Auto Snap", doSnap );
//		snapValue = EditorGUILayout.FloatField( "Snap Value", snapValue );
//		stepValue = EditorGUILayout.FloatField ("Step Value", stepValue);
//		if (GUI.Button (new Rect (10, 70, 50, 30), "Click")) {
//			DuplicateXPos();
//			Debug.Log ("Clicked the button with ssssssssssstext");
//		}
//		//Rect r = EditorGUILayout.BeginHorizontal("Buttonnn");
//	}
	
	public void Update()
	{
		if ( doSnap
		    && !EditorApplication.isPlaying
		    && Selection.transforms.Length > 0
		    && Selection.transforms[0].position != prevPosition )
		{
			Snap();
			prevPosition = Selection.transforms[0].position;
		}
	}

	private void DuplicateXPos()
	{
		//GameObject a = PrefabUtility.InstantiatePrefab(Selection.activeObject as GameObject) as GameObject; 
		EditorWindow.focusedWindow.SendEvent (EditorGUIUtility.CommandEvent ("Duplicate"));


	//	GameObject prefabRoot = EditorUtility.GetPrefabParent(Selection.activeGameObject) as GameObject;
	//	Debug.Log ("pre" + prefabRoot);
		//GameObject clone = EditorUtility.InstantiatePrefab(prefabRoot);
		//GameObject clone = 
	//		EditorUtility.InstantiatePrefab (prefabRoot, prefabRoot.transform.position, prefabRoot.transform.rotation);
		//clone.transform ();
	//	clone.transform.Translate (Vector3 (2, 2, 2));


		//Object prefabRoot = PrefabUtility.GetPrefabParent (Selection.activeGameObject);
		
		//if (prefabRoot != null)
		//	PrefabUtility.InstantiatePrefab (prefabRoot);
		//else
		//	Instantiate (Selection.activeGameObject);

		//Debug.Log ("Clickedaaa");
	}
	
	private void Snap()
	{
		foreach ( var transform in Selection.transforms )
		{
			var t = transform.transform.position;
			t.x = Round( t.x );
			t.y = Round( t.y );
			t.z = Round( t.z );
			transform.transform.position = t;
		}
	}
	
	private float Round( float input )
	{
		return snapValue * Mathf.Round( ( input / snapValue ) );
	}
}