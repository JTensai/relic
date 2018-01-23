using UnityEngine;
using System.Collections;

public class PillarTrigger : MonoBehaviour 
{
	public GameObject pillarBody;
	public GameObject arrow;
	public GameObject arrowImage;
	
	public bool canFall;
	public bool pushesToFall;
	public bool despawns;
	public float despawnTime = 10.0f;
	public bool regenerates;
	public float regenerateTime = 20.0f;
	public bool attacksPlayer = false;
	public float attackTriggerRange = 5.0f;
	public float fallSpeed = 1.0f;
	public float angleOfDamage = 120.0f;
	
	private float collisionRange = 1.5f;//How close a monster has to be to the pillar to knock it over
	private double arrowRange = 1.5;
	private bool countDown = false;
	private bool hasFallen = false;
	private float regenerateTimeCopy;
	private float despawnTimeCopy;

	public SoundInformation PillarHitSound;
	// Use this for initialization
	void Start () 
	{
		if(Application.isPlaying){
			regenerateTimeCopy = regenerateTime;
			despawnTimeCopy = despawnTime;
			GetComponent<InteractableComponent>().OnNotify+= HandleOnNotify;
			GetComponent<InteractableComponent>().OnInteract+= HandleOnInteract;
			arrowImage.renderer.enabled = false;
			pillarBody.GetComponent<PillarMain>().SetPosition(transform.position.x, transform.position.z);
		}
	}
	
	void HandleOnInteract (InteractableInteractEventData data)//Check if the axis is Pull or Action to decide if a push or a pull
	{
		if(canFall && data.Distance < 1.2f)
		{
			if(pushesToFall)
				pillarBody.GetComponent<PillarMain>().Fall(transform.position - data.Source.transform.position, fallSpeed, angleOfDamage, true);
			else
				pillarBody.GetComponent<PillarMain>().Fall(data.Source.transform.position - transform.position, fallSpeed, angleOfDamage, true);
			canFall = false;
			arrowImage.renderer.enabled = false;
			countDown = true;
			hasFallen = true;

			if(PillarHitSound.SoundFile != null){
				PillarHitSound.CreateSoundInstance(gameObject).Play();
			}
		}
	}

	public void HandleRammedByMonster(Transform monsterPosition) {
		if(canFall) {
			pillarBody.GetComponent<PillarMain>().Fall(transform.position - monsterPosition.position, fallSpeed, angleOfDamage, true);
			canFall = false;
			arrowImage.renderer.enabled = false;
			countDown = true;
			hasFallen = true;

			if(PillarHitSound.SoundFile != null){
				PillarHitSound.CreateSoundInstance(gameObject).Play();
			}
		}
	}

	public bool HasPillarFallen() {
		return hasFallen;
	}
	
	void HandleOnNotify (InteractableNotifyEventData data)
	{
		if(attacksPlayer && data.Distance <= attackTriggerRange && data.IsPlayer && canFall)
		{
			pillarBody.GetComponent<PillarMain>().Fall(data.Source.transform.position - transform.position, fallSpeed, angleOfDamage, true);
			canFall = false;
			arrowImage.renderer.enabled = false;
			countDown = true;
		}
		else if(canFall && arrowImage != null)
		{
			if(data.Distance < arrowRange)
			{
				arrowImage.renderer.enabled= true;
				Vector3 lookPoint = data.Source.transform.position - arrow.transform.position;
				lookPoint.y = 0.0f;
				if(pushesToFall)
					lookPoint = arrow.transform.position - lookPoint;
				else
					lookPoint = arrow.transform.position + lookPoint;
				arrow.transform.LookAt(lookPoint);
			}
			else
				arrowImage.renderer.enabled= false;
			
		}
		if(data.Source.GetComponent<MonsterBase>() != null && data.Distance < collisionRange)
		{
			if(canFall)
			{
				if(pushesToFall)
					pillarBody.GetComponent<PillarMain>().Fall(transform.position - data.Source.transform.position, fallSpeed, angleOfDamage, true);
				else
					pillarBody.GetComponent<PillarMain>().Fall(data.Source.transform.position - transform.position, fallSpeed, angleOfDamage, true);
				canFall = false;
				arrowImage.renderer.enabled = false;
				countDown = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(pillarBody!= null && pillarBody.GetComponent<PillarMain>().HasFallen())
		{
			canFall = false;
			if(!countDown)
				countDown = true;
		}
		if(countDown && despawns)
		{
			despawnTimeCopy -= Time.deltaTime;
			if(despawnTimeCopy <= 0.0f)
				Destroy (gameObject);
		}
		if(countDown && regenerates)
		{
			regenerateTimeCopy -= Time.deltaTime;
			if(regenerateTimeCopy <= 0.0f)
			{
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				pillarBody.GetComponent<PillarMain>().ResetPosition();
				countDown = false;
				canFall = true;
				regenerateTimeCopy = regenerateTime;
				despawnTimeCopy = despawnTime;
			}
		}
	}
}
