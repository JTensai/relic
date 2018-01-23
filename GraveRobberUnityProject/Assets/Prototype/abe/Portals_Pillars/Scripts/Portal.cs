using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour 
{


	public double delayTime;
	private double countDown;
	private bool paused;
	public GameObject otherPortal;
	public bool onlySwarm;
	public EffectBase TeleportFX;





	void Start () 
	{onlySwarm = true;
		paused = false;
	}


	// Update is called once per frame
	void Update () 
	{
		if(paused)
		{
			countDown -= Time.deltaTime;
			if(countDown <= 0.0f)
				paused = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(!paused)
		{

			if(other.GetComponentInParent<BroodSwarm>() != null && onlySwarm)
				{teleport(other);}
			else if(other.GetComponent<HealthComponent>() !=null)
				{
				teleport(other);
			}
		

			}


	}


	public void teleport(Collider other)
		{pauseTransports ();
		if (TeleportFX != null && !onlySwarm) {
			EffectBase newInstance = TeleportFX.GetInstance (transform.position);
			newInstance.PlayEffect ();
			EffectBase secondINstance = TeleportFX.GetInstance(otherPortal.transform.position);
			secondINstance.PlayEffect();
				}
		otherPortal.GetComponent<Portal> ().pauseTransports ();//Not the pad, want the parent object itself
		


		Vector3 offset = otherPortal.transform.position;//ignore y
		offset.y += 1;
		
		other.gameObject.transform.position = offset;
		Debug.Log("Should teleport");


		}


	public void pauseTransports()
	{
		paused = true;
		countDown = delayTime;
	}
}
