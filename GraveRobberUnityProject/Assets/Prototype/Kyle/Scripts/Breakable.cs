using UnityEngine;
using System.Collections;


public delegate void ObjectBroken(GameObject unit, float value);

[RequireComponent(typeof(TriggerDetector))]
public class Breakable : MonoBehaviour {

	public float cost = 1;
	public event ObjectBroken OnObjectBroken;


	private TriggerDetector detectorScript;

	void Start () {
		detectorScript = GetComponent<TriggerDetector>();
		detectorScript.PlayerTriggered = true;
		detectorScript.EnemyTriggered = true;

	}

	public void Break(GameObject unit, float value)
	{
		// Check if there are any listeners. This throws an exception if OnUnitExploded is null.
		if(OnObjectBroken != null)
		{
			OnObjectBroken(unit, value);
		}
	}
	
	private void Update()
	{
		if ( detectorScript.TriggeredOn() ) {
			Break(gameObject, cost);
			Destroy(gameObject);
		}

	}
}
