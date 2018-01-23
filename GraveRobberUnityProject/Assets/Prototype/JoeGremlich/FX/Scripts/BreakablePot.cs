using UnityEngine;
using System.Collections;

public class BreakablePot : MonoBehaviour {
	private bool isBroken = false;
	public EffectBase BreakingFX;
	public GameObject unbrokenGlass;
	public GameObject unbrokenTop;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider c){
		if (c.gameObject.CompareTag("Player") && !isBroken){
			isBroken = true;
			unbrokenGlass.GetComponent<MeshRenderer>().enabled = false;	
			unbrokenTop.GetComponent<MeshRenderer>().enabled = false;
			if(BreakingFX != null){
				EffectBase newInstance = BreakingFX.GetInstance(c.gameObject.transform.position);
				newInstance.transform.LookAt(this.transform.position);
				newInstance.transform.position = this.transform.position;
				newInstance.transform.rotation = Quaternion.Euler(new Vector3(0, newInstance.transform.eulerAngles.y, 0));
				newInstance.PlayEffect();
			}
			UpdateStatTracker();
		}
	}
	public void UpdateStatTracker()
	{
		GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
		if (inventories.Length > 0)
		{
//			string type = gameObject.name;
			StatTracker st = inventories[0].GetComponent<StatTracker>();
			st.InteractableHit(this.gameObject);
		}
	}
}
