using UnityEngine;
using System.Collections;

public class InteractableNotifyEventData {
	public GameObject Source{get;private set;}
	public bool IsPlayer {get;private set;}
	public float Distance {get;private set;}
	public float NotificationStrength{get;private set;}

	public InteractableNotifyEventData(GameObject source, bool isPlayer, float distance, float notificationStrength){
		this.Source = source;
		this.IsPlayer = isPlayer;
		this.Distance = distance;
		this.NotificationStrength = notificationStrength;
	}

}
