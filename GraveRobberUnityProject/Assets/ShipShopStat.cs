using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipShopStat : MonoBehaviour {
	
	public GameHUDManager GameHUD;
	public GameObject inventory;
	public GameObject MiniRelicsContainer;
	public GameObject VictoryScreenStatPrefab;
	public GameObject FinalStatsContainer;
	public UILabel TotalPoints;
	public GameObject MainRelicModel;
	public Light VictoryScreenLight;
	
	private List<GameObject> FinalStats = new List<GameObject>();
	private int currentY = 0;

	
	private float collectedMiniRelics = 0;
	private UILabel MiniRelicsLabel;
	private List<GameObject> CollectedMiniRelicsList = new List<GameObject> ();
	
	private bool showingVictoryScreen;
	private UIWidget VictoryScreenWidget;
	
	private StatTracker stats;
	private float currentX = 0;
	// Use this for initialization
	void Start () {
		if (GameHUD == null)
		{
			if (GameObject.FindGameObjectWithTag ("GameHUD") != null)
			{
				GameHUD = GameObject.FindGameObjectWithTag ("GameHUD").GetComponent<GameHUDManager> ();
			}
		}
		if (inventory == null)
		{
			inventory = GameObject.FindGameObjectWithTag("Inventory");
		}
		
		if (stats == null && inventory != null)
		{
			stats = inventory.GetComponent<StatTracker>();
		}
		
		if (MiniRelicsContainer == null)
		{
			MiniRelicsContainer = transform.FindChild("MiniRelicsContainer").gameObject;
		}
		if (MiniRelicsContainer != null && MiniRelicsLabel == null)
		{

			MiniRelicsLabel = MiniRelicsContainer.transform.FindChild("RelicCount").gameObject.GetComponent<UILabel>();
		}
		if (VictoryScreenLight != null)
		{
			VictoryScreenLight.enabled = false;
		}
		
		currentY = (FinalStatsContainer.GetComponent<UITexture> ().height / 2) - (VictoryScreenStatPrefab.GetComponent<UILabel>().height / 2) - 10;
		
		VictoryScreenWidget = GetComponent<UIWidget> ();
		VictoryScreenWidget.alpha = 0;
		currentX = -50;
	}
	
	// Update is called once per frame
	void Update () {		
		showingVictoryScreen = VictoryScreenWidget.alpha == 1 ? true : false;
	}
	
	public void AddMiniRelicToVictoryScreen(GameObject miniRelic){
		
		GameObject newMiniRelic = (GameObject) Instantiate (miniRelic);
		newMiniRelic.transform.parent = MiniRelicsContainer.transform;
		//		newMiniRelic.transform.localScale = Vector3.Scale(temp.transform.localScale, new Vector3(0.107f,0.107f,0.107f));
		newMiniRelic.transform.localScale = new Vector3 (60, 60, 60);
		newMiniRelic.transform.localPosition = new Vector3 (currentX-50, -50, -70);
		newMiniRelic.transform.localRotation = Quaternion.Euler (new Vector3 (15, -120, 18));
		
		foreach (MeshRenderer mr in newMiniRelic.GetComponentsInChildren<MeshRenderer>())
		{
			mr.gameObject.layer = LayerMask.NameToLayer("UI");
		}
		newMiniRelic.SetActive (false);
		CollectedMiniRelicsList.Add (newMiniRelic);
		
		// create a 3d model of it from relic.model
		currentX += 200;
		collectedMiniRelics++;



		GameObject newStat = (GameObject) Instantiate(VictoryScreenStatPrefab);
		newStat.transform.parent = FinalStatsContainer.transform;
		newStat.transform.localScale = new Vector3(1f, 1f, 1f);
		newStat.transform.localPosition = new Vector3(currentX-250, 270, -70); 
		newStat.name = "Relic Worth";
		
		UILabel label = newStat.GetComponent<UILabel>();
		//				label.leftAnchor.absolute = FinalStatsContainer.GetComponent<UITexture>().leftAnchor.absolute;
		
		shopStat stat = newStat.GetComponent<shopStat>();
		stat.SetText("$" +miniRelic.GetComponent<Collect>().value);

		
		
		
		
		FinalStats.Add(newStat);
	


	}
	
	public void BuildStatSheet(){

		MiniRelicsContainer.transform.FindChild ("Bank").gameObject.GetComponent<UILabel> ().text = "$" + inventory.GetComponent<StatTracker> ().bank;
		if (VictoryScreenLight != null)
		{
			VictoryScreenLight.enabled = true;
		}
		
		showingVictoryScreen = true;
		if (MainRelicModel != null)
		{
			MainRelicModel.GetComponent<MeshRenderer>().enabled = true;
			StartCoroutine ("RotateRelic");
		}
		
		if (MiniRelicsLabel != null)
		{
			MiniRelicsLabel.text = "Relics: " + collectedMiniRelics + " / " + stats.getMiniRelics ().Count;
		}
		foreach (GameObject g in CollectedMiniRelicsList)
		{
			g.SetActive(true);
		}
		
		if (VictoryScreenStatPrefab != null)
		{
		/*	foreach(KeyValuePair<string, StatDataContainer> entry in stats.BuildFinalStats())
			{
				GameObject newStat = (GameObject) Instantiate(VictoryScreenStatPrefab);
				newStat.transform.parent = FinalStatsContainer.transform;
				newStat.transform.localScale = new Vector3(1f, 1f, 1f);
				newStat.transform.localPosition = new Vector3(-200, currentY, 0); // the -100 used here is a magic number used to offset the object to the correct position
				newStat.name = "FinalStat-"+entry.Key;
				
				UILabel label = newStat.GetComponent<UILabel>();
				//				label.leftAnchor.absolute = FinalStatsContainer.GetComponent<UITexture>().leftAnchor.absolute;
		
				shopStat stat = newStat.GetComponent<shopStat>();
				stat.SetText(entry.Key);
				stat.SetValue(entry.Value.value);
				stat.SetValueText(entry.Value.displayText);

				
				currentY -= label.height + 10;
				
				FinalStats.Add(newStat);
			}
			TotalPoints.text = score.ToString();*/
		}
	}
	
	private IEnumerator RotateRelic()
	{
		float step = 0;
		Debug.Log ("Showing: " + showingVictoryScreen);
		while (showingVictoryScreen)
		{
			step += .5f;
			step = step >= 360 ? 0 : step;

			//			Debug.Log("Rotating Relic: " + 180 * Mathf.Sin(step));
			//			float rotateValue = (180 * Mathf.Sin(step))/ 100;
			//			RelicModel.transform.rotation = Quaternion.AngleAxis(step, Vector3.up) + Quaternion.AngleAxis(10f, Vector3.right);
			MainRelicModel.transform.rotation = Quaternion.Euler(new Vector3(10, step, 0));
			yield return null;
		}
	}
}
