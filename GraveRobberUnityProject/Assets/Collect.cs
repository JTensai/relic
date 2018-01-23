using UnityEngine;
using System.Collections;

public class Collect : MonoBehaviour {

	public float time;

	public string relicName;
	public GameObject model;
	public GameObject relicInfo;
	public SoundInformation PickedUpMiniRelicSound;

	public float value;
	public bool isKey;
	public float keyNumber;

	private VictoryScreen vs;
	private ShipShopStat sss;

	private relicInfo info;
	// Use this for initialization
	void Start () {
		//make a new prefab with name inventory;
		
		GameObject infoObject = (GameObject)Instantiate (relicInfo, this.transform.position, Quaternion.identity);
		info = infoObject.GetComponent<relicInfo> ();
		info.name = relicName;
		info.model = model;
		if (GameObject.FindObjectOfType<VictoryScreen> () != null) {
			vs = GameObject.FindObjectOfType<VictoryScreen> ();
		}
		else {
			sss = GameObject.FindObjectOfType<ShipShopStat> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void collect()
	{
		//GameUI.DisplayInstructionTextArea(saying, time); // stay open for 4 seconds
		if (!isKey) 
		{
			GameObject.FindGameObjectWithTag ("Inventory").GetComponent<StatTracker> ().MiniArtifactGrabed (info);
			if(vs != null)
			{
				vs.AddMiniRelicToVictoryScreen (this.gameObject);
			}
			else
			{
				sss.AddMiniRelicToVictoryScreen(this.gameObject);
			}
		} 
		else 
		{
			GameObject.FindGameObjectWithTag ("Inventory").GetComponent<StatTracker> ().addKey(keyNumber);	
		}

		playPickedUpMiniRelicSound ();

		Destroy (this.gameObject);
	}

	private void OnTriggerEnter(Collider collide)
	{
		if (collide.CompareTag("Player"))
		{
			collect();
		}
	}

	public void playPickedUpMiniRelicSound() {
		if(PickedUpMiniRelicSound != null && PickedUpMiniRelicSound.SoundFile != null) {
			SoundInstance s = PickedUpMiniRelicSound.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}
}
