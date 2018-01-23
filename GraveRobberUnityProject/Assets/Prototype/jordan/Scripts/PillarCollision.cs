using UnityEngine;
using System.Collections;
using Pathfinding;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PillarCollision : MonoBehaviour {
	
	private AttackBase attack;
	
    //private float xCenter;
    //private float zCenter;
	private bool isFalling;
	private string rotPosition;
	private Pillar pillar;
	private bool finishedFall;
	private bool hasCrumbled;
	private float collision_delay;
	private float kill_delay;
	private float crumble_delay;
	// Deprectaed Invisible Walls code
//	private BoxCollider trigger;
//	private BoxCollider wall1 = null;
//	private BoxCollider wall2 = null;

	private GraphUpdateScene gus;
	private BoxCollider graphCollider;

	public EffectBase PillarEffect;

    private CameraShakeManager shaker;

	// Use this for initialization
	void Start () {
        //xCenter = transform.position.x;
        //zCenter = transform.position.z;
		attack = GetComponent<AttackBase> ();
		rotPosition = transform.rotation.ToString("f7");
		isFalling = false;
		pillar = this.GetComponentInParent<Pillar> ();
		collision_delay = 0.05f;
		kill_delay = 0.2f;
		crumble_delay = 0.05f;
		gus = this.GetComponentInParent<GraphUpdateScene> ();
        //transform.Find ("PillarProtector").GetComponent<GraphUpdateScene> ();
        //this.GetComponent<GraphUpdateScene> ();
	}
	
	// Deprectaed Invisible Walls code
//	public void SpawnWalls()
//	{
//		if (wall1 == null || wall2 == null)
//		{
//			wall1 = (BoxCollider)this.gameObject.AddComponent("BoxCollider");
//			wall2 = (BoxCollider)this.gameObject.AddComponent("BoxCollider");
//			
//			wall1.size = new Vector3(0.01f, 1, 3);
//			wall2.size = new Vector3(0.01f, 1, 3);
//			
//			wall1.center = new Vector3(-0.5f, 0, -1);
//			wall2.center = new Vector3(0.5f, 0, -1);
//			
//		}
//		else
//		{
//			wall1.enabled = true;
//			wall2.enabled = true;
//		}
//	}
	
	// Deprectaed Invisible Walls code
//	public void DestroyWalls()
//	{		
//		if (wall1 != null || wall2 != null)
//		{
//			wall1.enabled = false;
//			wall2.enabled = false;
//		}
//	}
	
	
	void OnCollisionEnter(Collision other)
	{
		// The collider on the pillar is used to determine what the pillar should do if it hits something without hp
		if (isFalling && other.gameObject.GetComponent<HealthComponent>() == null)
		{
			// Make sure Bentley wasn't hit, and also not a debris
			if (!other.gameObject.CompareTag("Debris"))
			{
				// hit another pillar
				if (other.gameObject.GetComponentInParent<Pillar>() != null){
					Pillar pillar = other.gameObject.transform.GetComponentInParent<Pillar> ();
					pillar.HandleOnDashed(new InteractableInteractEventData(this.gameObject, false, Vector3.Distance(pillar.gameObject.transform.position, transform.position)));
				}
				else{
					// force wedge pillar to fall to 90 degrees
                    if (!pillar.bridgeIsWedged) 
					{ 
					    pillar.SetFreeze(true);
//						Debug.Log("Pillar hit: " + other.gameObject.name + " and finished falling");
					    FinishFall();
                    }
				}
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (pillar != null && pillar.WasHitByPlayer ()) {
			// do not damage the player if the player pushed the pillar over
			if (collider.gameObject.GetComponent<HealthComponent> () != null && !collider.CompareTag("Player") && !collider.CompareTag("Debris"))
			{
				if (kill_delay < 0){
					attack.Attack (collider.transform);
				}
			}
		}
		else{
			// Hit by a monster
			if (collider.gameObject.GetComponent<HealthComponent> () != null)
			{
				if (kill_delay < 0){
					//Debug.Log("Monster was crushed: " + other.gameObject.name);
					attack.Attack (collider.transform);
				}
			}
		}
	}

	void OnTriggerStay(Collider collider)
	{	
		if(isFalling){
			// Having this on trigger stay method allows for pillars to kill monsters standing very close to the pillar, it will also make it so that the pillar instakills the player
			if (pillar != null && pillar.WasHitByPlayer ()) {
				// do not damage the player if the player pushed the pillar over
				//if (!collider.CompareTag("Player") && !collider.CompareTag("Debris") && collider.gameObject.GetComponent<HealthComponent> () != null)
				if(collider.gameObject.layer == LayerMask.NameToLayer("Monster"))
				{
					if (kill_delay < 0){
						attack.Attack (collider.transform);
					}
				}
			}
			else{
				// Hit by a monster
				//if (collider.gameObject.GetComponent<HealthComponent> () != null)
				//{
					if (kill_delay < 0){
						//Debug.Log("Monster was crushed: " + other.gameObject.name);
						attack.Attack (collider.transform);
					}
				//}
			}
		}
	}
	
	// Deprectaed Invisible Walls code
//	void OnTriggerEnter(Collider collider)
//	{
//		if (finishedFall)
//		{
//			if (collider.gameObject.GetComponent<PlayerBase> () != null && pillar.makesBridge)
//			{
//				SpawnWalls();
//			}
//		}
//	}

	// Deprectaed Invisible Walls code
//	void OnTriggerExit(Collider collider)
//	{
//		if (finishedFall)
//		{
//			if (collider.gameObject.GetComponent<PlayerBase> () != null && pillar.makesBridge)
//			{
//				DestroyWalls();
//			}
//		}
//	}
	
	public void StartFalling()
	{
		isFalling = true;
	}
	// Update is called once per frame
	void Update () {
		if (isFalling)
		{
            if (!pillar.bridgeIsWedged)
            {
                // This collision delay was needed because of a rounding error in Unity. there wasn't enough difference between rotation during the first frame of the fall
                if (collision_delay < 0)
                {
                    // check to see if it is still falling
                    if (pillar.FallingPivotPoint.transform.rotation.ToString("f7") != rotPosition)
                    {
                        rotPosition = pillar.FallingPivotPoint.transform.rotation.ToString("f7");
                    }
                    else
                    {
                        //Debug.Log("Pillar stopped moving: " + pillar.FallingPivotPoint.transform.rotation.eulerAngles);
                        FinishFall();
                    }
                }
                else
                {
                    collision_delay -= Time.deltaTime;
                }
                kill_delay -= Time.deltaTime;
            }
		}
		// done falling
		if (finishedFall)
        {
			if (crumble_delay > 0){
                crumble_delay -= Time.deltaTime;
//                EditorApplication.isPaused = true;
			}
			else{
				if (pillar.crumbles && !hasCrumbled)
                {
					Crumble();
				}
			}
		}
	}

    void Crumble()
	{
		Vector3 fallRotation = pillar.RotationalPivotPoint.transform.localRotation.eulerAngles;
		Quaternion rotation = new Quaternion();
//		rotation = Quaternion.Euler(new Vector3(fallRotation.x + 90, fallRotation.y, fallRotation.z));
		rotation = Quaternion.Euler(new Vector3(0,0,0));
		float height = 5;// this shouldn't matter for round pillars, and since square pillars don't crumble it should never actually hit this.
		if (this.GetComponent<CapsuleCollider>() != null){
			height = (this.transform.localScale.y * this.GetComponent<CapsuleCollider>().height) + 0.4f;
		}
//		float heightAngle = 90 - pillar.FallingPivotPoint.transform.localRotation.eulerAngles.x;
//		float xoffset = (height * (Mathf.Sin(fallRotation.y * Mathf.Deg2Rad)));
//		float zoffset = (height * (Mathf.Cos(fallRotation.y * Mathf.Deg2Rad)));
//		float yoffset = (height * (Mathf.Sin(heightAngle * Mathf.Deg2Rad)));// + 0.05f;

		if (0 < pillar.RemainingRubble.Length){
			GameObject rubbleBottom = pillar.RemainingRubble[0];
//			rubbleBottom = (GameObject)Instantiate(rubbleBottom, transform.position + new Vector3(xoffset * -0.12f, yoffset * -0.12f, zoffset * -0.12f), rotation);
			rubbleBottom = (GameObject)Instantiate(rubbleBottom);
			rubbleBottom.transform.parent = transform;
			rubbleBottom.transform.localRotation = rotation;
			rubbleBottom.transform.localPosition = new Vector3(0,(height * 0f)-0.4f,0);
			rubbleBottom.transform.RotateAround(rubbleBottom.transform.position, rubbleBottom.transform.up, 90);
		}
		if (1 < pillar.RemainingRubble.Length){
			GameObject rubble1 = pillar.RemainingRubble[1];
//			rubble1 = (GameObject)Instantiate(rubble1, transform.position + new Vector3(xoffset * 0.57f, yoffset * 0.57f, zoffset * 0.57f), rotation);
			rubble1 = (GameObject)Instantiate(rubble1);
			rubble1.transform.parent = transform;
			rubble1.transform.localRotation = rotation;
			rubble1.transform.localPosition = new Vector3(0,(height * 0.65f)-0.4f,0);
			rubble1.transform.RotateAround(rubble1.transform.position, rubble1.transform.up, 90);
		}
		if (2 < pillar.RemainingRubble.Length){
			GameObject rubble2 = pillar.RemainingRubble[2];
//			rubble2 = (GameObject)Instantiate(rubble2, transform.position + new Vector3(xoffset * 0.79f, yoffset * 0.79f, zoffset * 0.79f), rotation);
			rubble2 = (GameObject)Instantiate(rubble2);
			rubble2.transform.parent = transform;
			rubble2.transform.localRotation = rotation;
			rubble2.transform.localPosition = new Vector3(0,(height * 0.855f)-0.4f,0);
			rubble2.transform.RotateAround(rubble2.transform.position, rubble2.transform.up, 90);
		}	
		if (3 < pillar.RemainingRubble.Length){
			GameObject rubbleTop = pillar.RemainingRubble[3];
//			rubbleTop = (GameObject)Instantiate(rubbleTop, transform.position + new Vector3(xoffset * .92f, yoffset * .92f, zoffset * .92f), rotation);
			rubbleTop = (GameObject)Instantiate(rubbleTop);
			rubbleTop.transform.parent = transform;
			rubbleTop.transform.localRotation = rotation;
			rubbleTop.transform.localPosition = new Vector3(0,(height * 0.96f)-0.4f,0);
			rubbleTop.transform.RotateAround(rubbleTop.transform.position, rubbleTop.transform.up, 90);
		}
		hasCrumbled = true;

        GetComponentInChildren<MeshRenderer>().enabled = false;
		pillar.FallingPivotPoint.GetComponent<CapsuleCollider> ().enabled = false;
		foreach (CapsuleCollider c in GetComponents<CapsuleCollider>()){
			c.enabled = false;
		}
	}
	
	void FinishFall()
	{		
		//Debug.Log ("Finishing up falling stuff");
		isFalling = false;
        CameraShakeManager.CameraShakeData shakeData = new CameraShakeManager.CameraShakeData(new Vector3(0.08f, 0, 0.08f), 0.6f, 10);
        CameraShakeManager.Shake(shakeData);
			
		if (!pillar.crumbles)
		{
			// Permanent Pillar
			// Debug.Log ("Pillar fell, updated A* pathing");
			graphCollider = (BoxCollider)this.gameObject.AddComponent("BoxCollider");
			graphCollider.bounds.Expand(.5f);
			gus.Apply();
			Destroy (graphCollider);			
		}

		// Deprectaed Invisible Walls code
//		if (this.GetComponentInParent<Pillar>().makesBridge) 
//		{
//			// Create the trigger to tell when the player is standing on the fallen pillar
//			trigger = (BoxCollider)this.gameObject.AddComponent("BoxCollider");
//			trigger.isTrigger = true;
//			trigger.center = new Vector3(0f,0f,-0.75f);
//			trigger.size = new Vector3(1f,.85f,0.5f);
//		}

		if(PillarEffect != null){
			float height = (this.transform.localScale.y * this.GetComponent<CapsuleCollider>().height) + 0.3f;
			float angle = pillar.RotationalPivotPoint.transform.localRotation.eulerAngles.y;
			float xoffset = (height * (Mathf.Sin(angle * Mathf.Deg2Rad)));
			float zoffset = (height * (Mathf.Cos(angle * Mathf.Deg2Rad)));
			EffectBase newInstance = PillarEffect.GetInstance(transform.position + new Vector3(xoffset/2, 0, zoffset/2));
			Vector3 pillarRot = pillar.RotationalPivotPoint.transform.localRotation.eulerAngles;
			newInstance.transform.rotation = Quaternion.Euler(new Vector3(pillarRot.x + 90, pillarRot.y, pillarRot.z));
			newInstance.PlayEffect();
		}

		finishedFall = true;
	}
}
