using UnityEngine;
using System.Collections;

public class DeathChanger : MonoBehaviour {

	public GameObject player;
	private GameObject deathEffect;
	public GameObject[] PrefabList = new GameObject[6];
	public TrapEnum TrapVariant = TrapEnum.Element_0;

	// Use this for initialization
	void Start () {
		HealthComponent h = GetComponent<HealthComponent> ();
		h.OnDeath += HandleOnDeath;
		deathEffect = PrefabList[(int) TrapVariant];
	}

	void HandleOnDeath ()
	{				
		if (deathEffect != null) {
			//Quaternion rotation = this.gameObject.transform.rotation * Quaternion.AngleAxis (-90, Vector3.right);
			Instantiate (deathEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);
		}
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		// get position of player and move towards it
		Vector3 playerPos = player.transform.position;
		Vector3 monsterPos = this.gameObject.transform.position;
		// Debug.Log (playerPos);
		// movement.Move (0, playerPos);

		//if (monster.collisionFlags)
		//	Debug.Log ("Collided with ");
		float distanceToPlayer = Vector3.Distance (monsterPos, playerPos);
		if (distanceToPlayer < 1)
			HandleOnDeath ();
	}

	public enum TrapEnum {
		Element_0, Element_1, Element_2, Element_3, Element_4, Element_5
	}

}
