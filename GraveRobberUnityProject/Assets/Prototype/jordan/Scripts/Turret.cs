using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	
	public enum TurretState {Off, Fire};
	public TurretState state = TurretState.Off;
	public bool rotate = false;
	public GameObject projectile;
	public float rateOfFire = 1;


	public float rotateRange = 1;
	
	private bool leftRight;
	private float timer = 0;
	private float spintimer =0;
	public bool delay;

	private MonsterBase monster;
	
	// Use this for initialization
	void Start () {

		monster = gameObject.GetComponent<MonsterBase> ();

		spintimer = rotateRange / 4;
		//state = TurretState.Fire;
		if (delay) {
			timer = rateOfFire/2;		
		}

	

	}
	
	// Update is called once per frame
	void Update () {

		if (monster.IsActivated) {
		

						switch (state) {
						case TurretState.Off:
								break;
						case TurretState.Fire:
								timer += Time.deltaTime;
								if (timer > rateOfFire) {

										Vector3 offset = this.transform.rotation * (new Vector3 (0, 1, 2));
										Vector3 place = this.transform.position + offset;

									GameObject fire = (GameObject)Instantiate (projectile, place, this.transform.rotation);
			
										timer = 0;
								}

		
								if (rotate) {
										spintimer += Time.deltaTime;
										if (spintimer > rotateRange / 2) {
												leftRight = !leftRight;
												spintimer = 0;
										}
										if (leftRight) {
												transform.Rotate (Vector3.up, 30 * Time.deltaTime);
										} else {
												transform.Rotate (Vector3.up, -30 * Time.deltaTime);
										}
								}
								break;
						}
				}	
	}
	
	public void toggleState ()
	{
		switch (state) {
		case TurretState.Off:
			state = TurretState.Fire;
			break;
		case TurretState.Fire:
			state = TurretState.Off;
			break;
		
		}
	}
	

	
	
}
