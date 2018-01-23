using UnityEngine;
using System.Collections;

public class ThrowerProjectile : MonoBehaviour {

	// Use this for initialization
	//public MovementComponent mover;
	private VisionArc vision;
	public EffectBase ImpactEffect;
	public double timer = 0;
	public double timerMax = .2;
	public GameObject gas;

	void Start () {

		vision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);
		//mover = gameObject.GetComponent<MovementComponent> ();
	}




	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (this.rigidbody.velocity.y > 0) {

			foreach(GameObject obj in vision.ObjectsInVision()) {
				if(obj.GetComponent<HealthComponent>() != null) {
					//AttackBase.GetAttackByVariant(AttackEnum.Default, gameObject).Attack(obj.transform);

					}
			}
			//ImpactEffect.PlayEffect(gameObject.transform.position);
		
			//Destroy(this.gameObject);
	}
	

}

	void OnCollisionEnter(Collision c)
	{
				if (timer > timerMax) {
						foreach (GameObject obj in vision.ObjectsInVision()) {
								if (obj.GetComponent<HealthComponent> () != null) {
										AttackBase.GetAttackByVariant (AttackEnum.Default, gameObject).Attack (obj.transform);
								}
						}
			EffectBase newInstance = ImpactEffect.GetInstance(gameObject.transform.position);
						newInstance.PlayEffect ();
						Instantiate(gas, gameObject.transform.position, Quaternion.identity);
						Destroy (this.gameObject);
				}
		}
}