using UnityEngine;
using System.Collections;

public class TurretProjectile : MonoBehaviour {


	private MovementComponent move;
	private float timer;



	private AttackBase _attackBase;

	// Use this for initialization
	void Start () {

		this._attackBase = this.gameObject.GetComponent<AttackBase>();
		move = gameObject.GetComponent<MovementComponent>();

	
	}
	
	// Update is called once per frame
	void Update () {


				
		timer  += Time.deltaTime;

		//if (timer > .5) {
			//collider.isTrigger = true;		
		//}

		if (timer > 4) {
			if(this.gameObject)
			Destroy (this.gameObject);	
		
		}
		move.Move(0,(gameObject.transform.forward) * Time.deltaTime * 10);

	}



	void OnTriggerEnter(Collider other)
		{  GameObject hitObject = other.collider.gameObject;

	
				if (hitObject.GetComponentInChildren<HealthComponent>() != null && hitObject.name != "cube")
					{_attackBase.Attack (hitObject.transform);}

				Destroy(this.gameObject);
				
			
	}


	
}
