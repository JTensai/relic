using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private Rigidbody projectile;
	private float rigidBodySpeed;
	private GameObject _player;

	private Vector3 _movementDirection;

	public float Speed;

	public EffectBase ImpactEffect;
	private AttackBase _attack;
	// Use this for initialization
	void Start () {
		_attack = AttackBase.GetAttackByVariant (AttackEnum.Default, this.gameObject);
		_movementDirection = transform.forward;

		if(GetComponent<Rigidbody>() != null){
			projectile = GetComponent<Rigidbody>();
		}
		else {
			Debug.LogError ("Rigidbody is missing!");
		}

		projectile.velocity = _movementDirection * Speed;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {

		if(!other.isTrigger){
			if (other.gameObject.GetComponent<HealthComponent> () != null)
				_attack.Attack (other.transform);
			
			if(ImpactEffect != null){
				EffectBase newInstance = ImpactEffect.GetInstance(this.transform.position);
				newInstance.PlayEffect();
			}
			
			Destroy(this.gameObject);
		}

	}
}
