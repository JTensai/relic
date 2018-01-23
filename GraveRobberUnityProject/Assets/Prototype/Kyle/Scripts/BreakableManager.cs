using UnityEngine;
using System.Collections;

public class BreakableManager : MonoBehaviour {

	private float total = 0;

	private void Start () {
		// Find all the exploders in the scene and add listeners to each
		Breakable[] breakables = FindObjectsOfType<Breakable>();
		foreach(Breakable objects in breakables)
		{
			AddListener(objects);
		}
	}
	
	private void AddListener(Breakable breakableObject)
	{
		// For the "event" type, + and - operators have been overloaded. "+" adds
		// a method reference to the list of methods to call when the event is invoked.
		// "-" removes the reference from the list.
		breakableObject.OnObjectBroken += HandleOnObjectBroken;
	}
	
	private void RemoveListener(Breakable exploder)
	{
		exploder.OnObjectBroken -= HandleOnObjectBroken;
	}
	
	private void HandleOnObjectBroken (GameObject unit, float value)
	{
		// Do something useful here
		Debug.Log(unit.name + " broke, costing: " + value);
		total += value;
	}

//	void OnGUI() {
//		GUI.Label(new Rect(10, 10, 300, 20), "Total Destruction: " + total );
//	}

}
