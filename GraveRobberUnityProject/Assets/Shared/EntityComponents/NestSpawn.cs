using UnityEngine;
using System.Collections;

public class NestSpawn : MonoBehaviour {
	
	public bool leaveWater = false;
	public bool attackAll = false;
	public float lifeTime =5;

	public GameObject brood;
	private VisionArc outerVision;
	public float swarmSpeed;


	public float spawnDelay = 1;  // change this to change how fast they spawn
	private float attackDelayTimer = 0;


	// Use this for initialization
	void Start () {
		
		outerVision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);
		attackDelayTimer = spawnDelay;
	
	}
	
	// Update is called once per frame
	void Update () {

		attackDelayTimer += Time.deltaTime;
		foreach (GameObject obj in outerVision.PlayersInVision()) {
			if (obj.GetComponent<PlayerBehavior> () != null || obj.GetComponent<HealthComponent> () != null) {
				if (attackDelayTimer >= spawnDelay) {
					
					Vector3 spawnLoc = new Vector3 (this.transform.position.x, this.transform.position.y +2, this.transform.position.z);
					GameObject look = (GameObject)Instantiate (brood, spawnLoc, Quaternion.identity);
					look.transform.LookAt(obj.transform.position);

				
					look.GetComponent<BroodSwarm>().lifetime = this.lifeTime;
					attackDelayTimer = 0;
					
				}
			}
		}

		
	}
	


	
}