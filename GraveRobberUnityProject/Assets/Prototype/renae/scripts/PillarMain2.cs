using UnityEngine;
using System.Collections;

public class PillarMain2 : MonoBehaviour {

	public CameraShakeManager.CameraShakeEventHandler ImpactCameraShakes;
	public RumbleManager.RumbleEventHandler ImpactRumbles;
	public SoundInformation ImpactSound;
	private Vector3 fallDirection;
	private float fallSpeed;
	private float fallSpeedBegin = 0.5f;
	private float fallSpeedMax = 1.8f;

	private GameObject pivot;
	private PillarCollision colliderMain;

	private bool hasFallen;
	private bool alreadyUpdated;

	public SoundInformation PillarHitSound;

	private void CheckInit()
	{
		if (colliderMain == null)
		{
			hasFallen = false;
			alreadyUpdated =false;
			fallSpeed = fallSpeedBegin;
			pivot = gameObject;
			colliderMain = pivot.GetComponent<PillarCollision>();
			

		}
	}
	// Use this for initialization
	void Start () {
		if(Application.isPlaying){
			CheckInit();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (hasFallen && fallSpeed == 0 && !alreadyUpdated)
		{
			//print (this.GetComponent<PillarCollision>().PillarName.name);
			//if(this.GetComponent<PillarCollision>().PillarName.name.Equals("BreakingPillar"))
//			{
//				if(crumble == null)
//					print ("the pillar breaker doesn't exist");
//				else
//					crumble.Break();
//				AstarPath.active.Scan();
//				alreadyUpdated = true;
//				Destroy(this.gameObject);
//				return;
//			}
//			AstarPath.active.Scan();
//			alreadyUpdated = true;
		}	
	}

	public bool HasFallen()
	{
		return hasFallen;
	}
	
	public bool AlreadyUpdated()
	{
		return alreadyUpdated;
	}

	public void Fall(Vector3 location, float fallSpeedX, float damageAngle, bool rotate)
	{
		fallSpeed *= fallSpeedX;
		fallSpeedBegin *= fallSpeedX;
		fallSpeedMax *= fallSpeedX;
		
		fallDirection = location;
		fallDirection.y = 0.0f;//Normalize fallDirection
		Normalize();
		if(rotate)
			fallDirection = Quaternion.AngleAxis(90, Vector3.up) * fallDirection;
		//fallDirection x,z gives the slope, from which we can know our angle...then just have to get the angle of the object I hit at the other end
		float angle = (Mathf.Atan2(fallDirection.z, fallDirection.x));
		if(angle < 0f)
			angle = (2*Mathf.PI) + angle;
		//Debug.Log (angle);
		if(colliderMain != null){
			//colliderMain.setActive(true, angle, angleOfDamage, fallSpeedX);
		}
		hasFallen = true;
	}

	void Normalize()
	{
		float distance = Mathf.Sqrt((fallDirection.x * fallDirection.x) + (fallDirection.z * fallDirection.z));
		fallDirection.x /= distance;
		fallDirection.z /= distance;
	}
}
