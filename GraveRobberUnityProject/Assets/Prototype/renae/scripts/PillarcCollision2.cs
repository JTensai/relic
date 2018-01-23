using UnityEngine;
using System.Collections;

public class PillarcCollision2 : MonoBehaviour {

//	public GameObject PillarRoot;
//	public GameObject PillarName;
//	public EffectBase ImpactEffect;
//	
//	private AttackBase attack;
//	private bool isActive;
//	private float fallingAngle;
//	private float fallSpeed = 1.0f;
//	private float angleOfDamage = 120.0f;
//	
//	private float xCenter;
//	private float zCenter;
//	
//	// Use this for initialization
//	void Start () 
//	{
//		if(Application.isPlaying){
//			attack = AttackBase.GetAttackByVariant(AttackEnum.Default, this.gameObject);
//			isActive = false;
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//	public void SetPosition(float x, float z)
//	{
//		xCenter = x;
//		zCenter = z;
//	}
//	
//	public void setActive(bool value, float angle, float damageAngle, float fSpeed)
//	{
//		isActive = value;
//		fallingAngle = angle;
//		angleOfDamage = damageAngle;
//		fallSpeed = fSpeed;
//		
//		angleOfDamage *= (Mathf.PI/180.0f);
//	}
//	
//	public bool getActive()
//	{
//		return isActive;
//	}
//	
//	void OnTriggerEnter(Collider other)
//	{		
//		if(ImpactEffect != null && isActive) {
//			//if(ImpactEffect != null && !other.name.Contains("Pillar")) {
//			EffectBase newInstance = ImpactEffect.GetInstance(other.transform.position);
//			newInstance.PlayEffect();
//		}
//		
//		float collisionAngle = Mathf.Atan2 (zCenter-other.transform.position.z, xCenter-other.transform.position.x);
//		if(collisionAngle < 0f)
//			collisionAngle = (2*Mathf.PI) + collisionAngle;
//		collisionAngle += Mathf.PI/2.0f;
//		if(collisionAngle > (2.0f*Mathf.PI))
//			collisionAngle -= (2.0f*Mathf.PI);
//		
//		float angleDifference = Mathf.Abs(collisionAngle-fallingAngle);
//		if (isActive) {
//			if (other.gameObject.GetComponent<HealthComponent> () != null && 
//				(angleDifference < angleOfDamage || angleDifference > ((2 * Mathf.PI) - angleOfDamage)))
//				attack.Attack (other.transform);
//		}
//		
//		if(other.name.Contains("Pillar"))
//		{
//			if(!other.name.StartsWith ("PillarID") && !other.name.Equals ("PillarBase"))
//			{
//				if(other.GetComponent<PillarCollision>() != null && PillarName != other.GetComponent<PillarCollision>().PillarName)
//				{
//					PillarMain ToPush = other.GetComponent<PillarCollision>().PillarRoot.GetComponent<PillarMain>();
//					Vector3 zero = new Vector3(0.0f, 0.0f, 0.0f);
//					if(!ToPush.HasFallen())
//					{
//						PillarRoot.GetComponent<PillarMain>().SlowFall();
//						ToPush.Fall (PillarRoot.GetComponent<PillarMain>().CurrentFallSpeed(), fallSpeed, angleOfDamage, false);
//					}
//				}
//			}
//		}
//	}
//	
//	public void Fall(Vector3 direction)
//	{
//		if(!float.IsNaN(direction.x) && !float.IsNaN(direction.y) && !float.IsNaN(direction.z)){
//			transform.Rotate (direction);
//		}
//		
//	}
}
