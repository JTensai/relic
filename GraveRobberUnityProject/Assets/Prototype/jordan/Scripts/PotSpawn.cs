using UnityEngine;
using System.Collections;

public class PotSpawn : MonoBehaviour {

	private GameObject spawnEffect = null;
	public GameObject[] PrefabList = new GameObject[6];
	public int[] ProbabilityList = new int[6];

	public Vector3 spawnOffset = Vector3.up * 0.1f;
	
	// Use this for initialization
	void Start () {
		int randomNum = Random.Range (1, 100);
		// Debug.Log (randomNum);
		int cumulative = 0;
		int index = 0;
		foreach (int probability in ProbabilityList) {
			cumulative += probability;
			if (randomNum < cumulative){
				spawnEffect = PrefabList[index];
				break;
			}
			index++;
		}
	}
	
	public void Spawn ()
	{				
		if (spawnEffect != null) {
			GameObject obj = (GameObject) Instantiate (spawnEffect, transform.position + spawnOffset, transform.rotation);
			if (obj.GetComponent<MonsterBase>() != null){
				obj.GetComponent<MonsterBase>().ActivateEntity();
			}
		}
	}	
}
