using UnityEngine;
using System.Collections;
using Pathfinding;

public class PillarMain : MonoBehaviour 
{
	public CameraShakeManager.CameraShakeEventHandler ImpactCameraShakes;
	public RumbleManager.RumbleEventHandler ImpactRumbles;
	public SoundInformation ImpactSound;
	private Vector3 fallDirection;
	private float fallPickup;
	private float fallSpeed;
	private float fallSpeedBegin = 0.5f;
	private float fallSpeedMax = 1.8f;
	private float  angleOfDamage = 120.0f;

	private GameObject pivot;
	private GameObject bottom;
	private GameObject middleLow;
	private GameObject middleHigh;
	private GameObject top;
	private OldPillarCollision colliderRenamed;
	private OldPillarCollision colliderBottom;
	private OldPillarCollision colliderMidLow;
	private OldPillarCollision colliderMidHigh;
	private OldPillarCollision colliderTop;
	private OldPillarCollision colliderMain;
	private bool activeRenamed;
	private bool hasFallen;
	private bool alreadyUpdated;
//	private PillarBreak crumble;
	public GameObject kickable;
	public GraphUpdateScene protector;

	private void CheckInit()
	{
		if (colliderMain == null)
		{
			activeRenamed = false;
			hasFallen = false;
			alreadyUpdated =false;
			fallPickup = fallSpeedBegin * 1.2f;
			fallSpeed = fallSpeedBegin;
			pivot = gameObject;
			colliderMain = pivot.GetComponent<OldPillarCollision>();
			colliderRenamed = colliderMain;
//			crumble = pivot.GetComponent<PillarBreak>();
			protector = pivot.GetComponentInChildren<GraphUpdateScene>();
			if(protector !=null)
				print("Got protector!");
			
			foreach(Transform child in pivot.transform)
			{
				if(child.gameObject.name.Contains ("Bottom"))
				{
					bottom = child.gameObject;
					colliderBottom = bottom.GetComponent<OldPillarCollision>();
				}
			}
			foreach(Transform child in bottom.transform)
			{
				if(child.gameObject.name.Contains ("MiddleLow"))
				{
					middleLow = child.gameObject;
					colliderMidLow = middleLow.GetComponent<OldPillarCollision>();
				}
			}
			foreach(Transform child in middleLow.transform)
			{
				if(child.gameObject.name.Contains ("MiddleHigh"))
				{
					middleHigh = child.gameObject;
					colliderMidHigh = middleHigh.GetComponent<OldPillarCollision>();
				}
			}
			foreach(Transform child in middleHigh.transform)
			{
				if(child.gameObject.name.Contains ("Top"))
				{
					top = child.gameObject;
					colliderTop = top.GetComponent<OldPillarCollision>();
				}
			}
		}
	}

	// Use this for initialization
	void Start () 
	{
		if(Application.isPlaying){
			CheckInit();
		}
	}

	public void ResetPosition()
	{
		CheckInit();
		pivot.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		bottom.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		middleLow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		middleHigh.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		top.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		hasFallen = false;
		fallSpeed = fallSpeedBegin;
		colliderRenamed = colliderMain;
	}

	public void SetPosition(float x, float z)
	{
		CheckInit();
		colliderMain.SetPosition(x, z);
		colliderBottom.SetPosition(x, z);
		colliderMidLow.SetPosition(x, z);
		colliderMidHigh.SetPosition(x, z);
		colliderTop.SetPosition(x, z);
	}

	public bool HasFallen()
	{
		return hasFallen;
	}

	public bool AlreadyUpdated()
	{
		return alreadyUpdated;
	}

	public Vector3 CurrentFallSpeed()
	{
		return fallDirection * fallSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(activeRenamed)
			colliderRenamed.Fall(fallDirection * fallSpeed);
		if(fallSpeed < fallSpeedMax)
		{
			if(fallSpeed < fallPickup)
				fallSpeed *= 1.01f;
			else
				fallSpeed *= 1.07f;
		}
		if (hasFallen && fallSpeed == 0 && !alreadyUpdated)
		{
			print (this.GetComponent<OldPillarCollision>().PillarName.name);
			if(this.GetComponent<OldPillarCollision>().PillarName.name.Equals("BreakingPillar"))
			{
//				if(crumble == null)
					print ("the pillar breaker doesn't exist");
//				else
//					crumble.Break();
				//AstarPath.active.UpdateGraphs(this.GetComponentInChildren<PillarCollision>().PillarName.collider.bounds);
				if(protector!=null)
					print ("found it!");
				protector.Apply();
				alreadyUpdated = true;
				return;
			}
			//AstarPath.active.Scan();
			//alreadyUpdated = true;
			//crumble.Break();
			//AstarPath.active.UpdateGraphs(this.GetComponentInChildren<PillarCollision>().PillarName.collider.bounds);
			if(protector){
				protector.Apply();}
			alreadyUpdated = true;
			//Instantiate(kickable,transform.position + new Vector3(0,1,0),transform.rotation);

			return;
		}
	}

	public void SlowFall()
	{
		fallSpeed *= 0.35f;
		if(fallSpeed < fallSpeedBegin)
			fallSpeed = fallSpeedBegin;
	}

	public void Fall(Vector3 location, float fallSpeedX, float damageAngle, bool rotate)
	{
		fallSpeed *= fallSpeedX;
		fallSpeedBegin *= fallSpeedX;
		fallSpeedMax *= fallSpeedX;
		angleOfDamage = damageAngle;

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
			colliderMain.setActive(true, angle, angleOfDamage, fallSpeedX);
		}
		if(colliderBottom != null){
			colliderBottom.setActive(true, angle, angleOfDamage, fallSpeedX);
		}
		if(colliderMidLow != null){
			colliderMidLow.setActive(true, angle, angleOfDamage, fallSpeedX);
		}
		if(colliderMidHigh != null){
			colliderMidHigh.setActive(true, angle, angleOfDamage, fallSpeedX);
		}
		if(colliderTop != null){
			colliderTop.setActive(true, angle, angleOfDamage, fallSpeedX);
		}

		activeRenamed = true;
		hasFallen = true;
	}

	void Normalize()
	{
		float distance = Mathf.Sqrt((fallDirection.x * fallDirection.x) + (fallDirection.z * fallDirection.z));
		fallDirection.x /= distance;
		fallDirection.z /= distance;
	}

	public void UpdatePivot(int numberPivot)
	{
		//1 = bottom, 2 = middleLow, 3 = middleHigh, 4 = top
		if(numberPivot == 1)
			colliderRenamed = colliderBottom;
		if(numberPivot == 2)
			colliderRenamed = colliderMidLow;
		if(numberPivot == 3)
			colliderRenamed = colliderMidHigh;
		if(numberPivot == 4)
		{
			fallSpeed = 0.0f;
			fallDirection = new Vector3(0.0f, 0.0f, 0.0f);
			activeRenamed = false;
			colliderMain.setActive(false, 0.0f, 0f, 0f);
			colliderBottom.setActive(false, 0.0f, 0f, 0f);
			colliderMidLow.setActive(false, 0.0f, 0f, 0f);
			colliderMidHigh.setActive(false, 0.0f, 0f, 0f);
			colliderTop.setActive(false, 0.0f, 0f, 0f);
			colliderRenamed = colliderTop;

			ImpactCameraShakes.PlayCameraShakes();
			ImpactRumbles.PlayRumbles();
			if(ImpactSound.SoundFile != null){
				ImpactSound.CreateSoundInstance(this.gameObject).Play();
			}
		
		}

	}
}
