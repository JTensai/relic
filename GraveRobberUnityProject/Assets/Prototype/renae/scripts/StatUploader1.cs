using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatUploader1 : MonoBehaviour {

	StatTracker inventory;
	UILabel kills;
	UILabel relics;
	UILabel doubble;
	UILabel trippple;
	UILabel poisonWater;
	UILabel charger;
	UILabel pillar;
	UILabel relicOne;
	UILabel relicTwo;
	UILabel relicThree;

	// Use this for initialization
	void Start () {


		inventory = GameObject.FindObjectOfType<StatTracker> ();
		if (inventory != null) {
						relics = GameObject.Find ("Relics").GetComponent<UILabel> ();
						kills = GameObject.Find ("Kills").GetComponent<UILabel> ();
						doubble = GameObject.Find ("Doubble").GetComponent<UILabel> ();
						trippple = GameObject.Find ("Trippple").GetComponent<UILabel> ();
						//poisonWater = GameObject.Find ("PoisonWater").GetComponent<UILabel> ();
						charger = GameObject.Find ("Charger").GetComponent<UILabel> ();
						pillar = GameObject.Find ("Pillar").GetComponent<UILabel> ();
						relicOne = GameObject.Find("relicOne").GetComponent<UILabel>();
						relicTwo = GameObject.Find("relicTwo").GetComponent<UILabel>();
						relicThree = GameObject.Find("relicThree").GetComponent<UILabel>();
						populateStats ();
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void populateStats(){

		kills.text = "" + inventory.getKills() + "/30";
		relics.text = "" + inventory.getCollectedMiniRelics().Count +"/3";
		doubble.text = ""+inventory.getDouble();
		trippple.text = ""+inventory.getTriple();
		//poisonWater.text = ""+inventory.getPoison();
		charger.text = ""+inventory.getCharger();
		pillar.text = ""+inventory.getPillar();
	
		List<relicInfo> temp = inventory.getCollectedMiniRelics();

		if (temp.Count >= 1) {
		
			relicOne.text = "" + temp [0].name;
		} else {
			relicOne.text = "Unknown";
			relicOne.fontStyle = FontStyle.Italic;
		}

		if (temp.Count >= 2) {
			relicTwo.text = ""+temp[1].name;
		}
		else {
			relicTwo.text = "Unknown";
			relicTwo.fontStyle = FontStyle.Italic;
		}

		if (temp.Count >= 3) {
			relicThree.text = ""+temp[2].name;

		}
		else {
			relicThree.text = "Unknown";
			relicThree.fontStyle = FontStyle.Italic;
		}




	}
}
