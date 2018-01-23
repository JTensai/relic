using UnityEngine;
using System.Collections;

public class ProjectilePositionLauncher : ProjectileLauncherBase {

	private VisionBase _fireVision;
	
	new void Start(){
		base.Start();
		_fireVision = VisionBase.GetVisionByVariant(VisionEnum.Default, gameObject);
	}
	#region implemented abstract members of ProjectileLauncherBase
	
	public override bool HasTarget ()
	{
		return _fireVision.PlayersInVision().Length > 0;
	}
	
	public override void AssignProjectileTarget (ProjectileBase Projectile)
	{
		Projectile.transform.position = this.transform.position;
		Projectile.transform.rotation = this.transform.rotation;
		Projectile.CurrentTargetType = ProjectileBase.TargetType.Position;
		Projectile.PositionTarget = _fireVision.PlayersInVision()[0].transform.position;
	}
	
	#endregion
}
