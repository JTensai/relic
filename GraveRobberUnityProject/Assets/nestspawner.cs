using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class nestspawner : MonsterBase {
	


	public float kidTotal=3;
	public GameObject brood;
	private VisionArc outerVision;
	public float spawnRate = 1;
	public float waveAmount =3;
	private float attackDelayTimer =0;
	public float lifeTime = 5;
	private float numKids = 0;
	public bool dieOnAttack;
	public float swarmSpeed;
	public List<GameObject> CreepStamps = new List<GameObject>();


	// Use this for initialization
	void Start () {

		outerVision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);

	}


	private void OnDestroy()
	{
		foreach (GameObject obj in CreepStamps) {

			Destroy(obj);
		}
	
		
	}




	public void kidDied()
		{numKids --;}
	
	// Update is called once per frame
	void Update () {
		ActivateEntity ();
		if (gameObject.GetComponent<MonsterBase>().IsActivated){
			if (numKids < kidTotal) {

				Vector3 spawnLoc = new Vector3 (this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
				GameObject look = (GameObject)Instantiate (brood, spawnLoc, Quaternion.identity);
				if (look.GetComponent<MonsterBase>() != null){
					look.GetComponent<MonsterBase>().ActivateEntity();
				}
				look.GetComponent<BroodSwarm> ().home = this.gameObject;
				look.GetComponent<BroodSwarm>().setParentSight(outerVision.Distance);
				look.GetComponent<BroodSwarm> ().lifetime = this.lifeTime;
				look.GetComponent<BroodSwarm> ().dieOnAttack = this.dieOnAttack;
			
			
				numKids ++;

			}
				
		
			foreach (GameObject obj in outerVision.ObjectsInVision()) {
		
				if (obj.GetComponent<PlayerController> () != null || obj.GetComponent<ChargingMummy>() != null ||obj.GetComponent<ChargingBruteMonster>() != null) {

					attackDelayTimer += Time.deltaTime;
					if (attackDelayTimer >= spawnRate) {
					
						for(int i =0; i< waveAmount; i ++)	
						{Vector3 spawnLoc = new Vector3 (this.transform.position.x, this.transform.position.y +1, this.transform.position.z);
						GameObject look = (GameObject)Instantiate (brood, spawnLoc, Quaternion.identity);
						if (look.GetComponent<MonsterBase>() != null){
							look.GetComponent<MonsterBase>().ActivateEntity();
						}
						look.transform.LookAt(obj.transform.position);
						look.GetComponent<BroodSwarm>().lifetime = this.lifeTime;
						look.GetComponent<BroodSwarm>().dieOnAttack = this.dieOnAttack;
							look.GetComponent<BroodSwarm>().setAttackTarget(obj);
							attackDelayTimer = 0;}
						
					}
				}
			}
		}
		
	}
		
	
}

	