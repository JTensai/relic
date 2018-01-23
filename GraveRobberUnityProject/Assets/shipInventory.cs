using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class shipInventory : MonoBehaviour {

	GameObject invent;
	private ShipShopStat sss;
	public List<GameObject> possibleArtifacts;

	// Use this for initialization
	void Start () {
		invent = GameObject.Find ("Inventory");
		sss = GameObject.FindObjectOfType<ShipShopStat> ();
		List<relicInfo> list = invent.GetComponent<StatTracker> ().getCollectedMiniRelics ();
	
		float x = 0;
		foreach (relicInfo entry in list) {


			foreach(GameObject obj in possibleArtifacts)
			{

				if(entry.GetComponent<relicInfo>().relicName.Equals(obj.name))
				{ Debug.Log("HI");
					sss.AddMiniRelicToVictoryScreen(obj);
					Vector3 place =  this.transform.position;
					place.x += x;
					obj.transform.position =place;

					x +=1;

				}}

		

				
		
		}

	}
	
	// Update is called once per frame
	void Update () {


	
	}
}
