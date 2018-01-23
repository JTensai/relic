using UnityEngine;
using System.Collections;

public class ProjectileAutoFireLauncher : ProjectileLauncherBase {

	#region implemented abstract members of ProjectileLauncherBase

	public override bool HasTarget ()
	{
		return true;
	}

	public override void AssignProjectileTarget (ProjectileBase Projectile)
	{
		Projectile.transform.position = this.transform.position;
		Projectile.transform.rotation = this.transform.rotation;
	}

	#endregion
}
