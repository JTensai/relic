using UnityEngine;
using System.Collections;

public class ProjectileSimpleImpact : ProjectileBase {
	private AttackBase _attackBase;
	#region implemented abstract members of ProjectileBase

	public override void Start ()
	{
		base.Start ();
		_attackBase = AttackBase.GetAttackByVariant(AttackEnum.Default, gameObject);
	}

	public override void HandleProjectileCollision (Collider col)
	{

		if(col.gameObject.GetComponent<RoomBehavior>() == null){
			if(col.gameObject.GetComponent<HealthComponent>() != null){
				_attackBase.Attack(col.gameObject.transform);
			}
			
			GameObject.Destroy(gameObject);
		}


	}

	#endregion
}
