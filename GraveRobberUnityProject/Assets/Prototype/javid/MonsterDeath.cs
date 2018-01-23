using UnityEngine;
using System.Collections;

public class MonsterDeath : DeathScript {

	public GameObject splatBody;

/*	// Use this for initialization
	void Start () {
		print("Spawn Monster");
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/

	public override void OnDeath() {
		print("Spawn Monster Has Died");
		Instantiate(splatBody,transform.position,Quaternion.identity);
		GameObject.Destroy(gameObject);


	///	StartCoroutine(DeathAnimation());
	}
	
/*	IEnumerator RestartLevel() {


		yield return new WaitForSeconds(2f);
		deathMessage = false;
		GameObject.Destroy(gameObject);
		print("Character Has Died 2 seconds passed");
		Application.LoadLevel(Application.loadedLevel);
	}*/

}
