using UnityEngine;
using System.Collections;

public class InteractableInteractEventData {
	public GameObject Source{get;private set;}
	public bool IsPlayer {get;private set;}
	public float Distance {get;private set;}
	
	public InteractableInteractEventData(GameObject source, bool isPlayer, float distance){
		this.Source = source;
		this.IsPlayer = isPlayer;
		this.Distance = distance;
	}
}
