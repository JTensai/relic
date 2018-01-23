using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour {

	public float RegenerationLevel = 0;
	public float SpeedLevel = 0;
	private GameObject player;

	private float timer;
	private HealthComponent health;



	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		health = player.GetComponent<HealthComponent> ();
		PlayerController control = player.GetComponent<PlayerController> ();
		control.WalkSpeed += control.WalkSpeed * .1f * SpeedLevel;


	}
	
	// Update is called once per frame
	void Update () {
		if(RegenerationLevel >0){
			timer += Time.deltaTime;

			if (timer > 7) {
				health.Heal(RegenerationLevel * .1f, false);
				timer = 0;
		
			}
		}



	
	}
}
