using UnityEngine;
using System.Collections;

public class ProjectileDoNothing : ProjectileBase {

	#region implemented abstract members of ProjectileBase

	public override void HandleProjectileCollision (Collider col)
	{
		GameObject.Destroy(gameObject);
	}

	#endregion
}
