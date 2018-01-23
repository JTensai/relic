using UnityEngine;
using System.Collections;

public abstract class ProjectileLauncherBase : MonoBehaviour {
	public float ReloadTime;
	public ProjectileBase ProjectileToFire;
	public float DistanceOverride = -1f;
	private float _elapsedTime;
	// Use this for initialization
	public virtual void Start () {
	
	}
	
	// Update is called once per frame
	public virtual void Update () {
		_elapsedTime += Time.deltaTime;
		if(_elapsedTime >= ReloadTime && HasTarget()){
			FireProjectile();
			_elapsedTime = 0f;
		}
	}

	public void FireProjectile(){
		ProjectileBase fired = (ProjectileBase)GameObject.Instantiate(ProjectileToFire);
		if(fired.collider.enabled && collider.enabled){
			Physics.IgnoreCollision(fired.collider, collider);
		}


		if(DistanceOverride != -1f){
			fired.DefaultDistance = DistanceOverride;
		}

		AssignProjectileTarget(fired);
		fired.initialize();
	}

	public abstract bool HasTarget();
	public abstract void AssignProjectileTarget(ProjectileBase Projectile);
}
