using UnityEngine;
using System.Collections;

public class ThrowablePrefab : MonoBehaviour
{

    private bool inAir = false;
//  private float _smallNumber = .001f;
    public EffectBase kickEffect;
    public EffectBase impactEffect;
    public float kickStrength = 700f;
    public bool activatesInteractables = true;
    public bool explodeOnImpact = true;
    private AttackBase _attackBase;

    //private float inAirTimer = 0.0f;

	public Quaternion initialRotation;

    private Vector3 kickForce;
    private bool kickForceApplied = true;
    [ReadOnlyAttribute]
    public bool _pickedUp;

    InteractableComponent interactable;
	InteractableComponent Interactable{
		get{
			if(interactable == null){
				interactable = gameObject.GetComponent<InteractableComponent>();
				interactable.OnInteract += HandleOnInteract;
				interactable.OnNotify += HandleOnNotify;
			}
			return interactable;
		}
	}
	
	public SoundInformation PickUpSoundEffect;
	public SoundInformation HoldSoundEffect;
	public SoundInformation ThrowSoundEffect;
	public SoundInformation ImpactSoundEffect;

    private PlayerController playerController;
    private float previousPlayerActionRadius;
	
	private SoundInstance _holdSoundInstance;

    // Use this for initialization
    void Start()
    {

        this._attackBase = this.gameObject.GetComponent<AttackBase>();
    }

    void HandleOnNotify(InteractableNotifyEventData data)
	{
        //Quaternion rotation = new Quaternion();
        //Vector3 dir = transform.position - data.Source.transform.position;

        //dir = new Vector3(dir.x, 0, dir.z).normalized;
        //float angleDegs;
        //angleDegs = Mathf.Atan2(-dir.z, dir.x) * Mathf.Rad2Deg;
        //rotation = Quaternion.Euler(new Vector3(0, angleDegs + 90, 0));
        //indicator.transform.rotation = rotation;
        //indicator.transform.position = transform.localPosition;
        //indicatorRenderer.enabled = true;
        //	Debug.Log ("Turning on arrow: " + indicatorRenderer.enabled);
    }

    public void HandleOnInteract(InteractableInteractEventData data)
	{
        if (data.IsPlayer)
		{
            playerController = data.Source.GetComponent<PlayerController>();
            playerController.PickUpObstacle(this);

            UpdateStatTracker();
            // stuff that happens when debris is picked up

			playPickUpSound();
			startPlayingHoldSound();

            rigidbody.detectCollisions = false;
            rigidbody.useGravity = false;
            collider.enabled = false;
            Interactable.enabled = false;
            Interactable.IsInteractable = false;
            _pickedUp = true;
			initialRotation = this.transform.rotation;
        }
    }

	public void Drop() {
		_pickedUp = false;
		stopPlayingHoldSound();
	}

	public void Throw(Vector3 direction)
	{

		if (playerController) {
			playerController.ThrowIndicator.GetComponentInChildren<MeshRenderer> ().enabled = false;
		
		}
		_pickedUp = false;

		Vector3 dir = direction;
		dir = new Vector3(dir.x, 0, dir.z).normalized;

		this.transform.position += dir * .1f;
		this.transform.position += new Vector3(0, .1f, 0);

		Vector3 toKick = (dir + new Vector3(0, .2f, 0));

		kickForce = toKick * kickStrength;
		kickForceApplied = false;

		inAir = true;

		if (kickEffect != null)
		{
			EffectBase newInstance = kickEffect.GetInstance(transform.position);
			newInstance.PlayEffect();
		}

		stopPlayingHoldSound();
		playThrowSound();
	}

    void OnCollisionEnter(Collision col)
    {
        if (inAir){
            GameObject hitObject = col.collider.gameObject;

            if (!hitObject.CompareTag("Player") && !hitObject.CompareTag("Spark"))
            {
                CameraShakeManager.CameraShakeData shakeData = new CameraShakeManager.CameraShakeData(new Vector3(0.04f, 0, 0.04f), 0.4f, 7);
                CameraShakeManager.Shake(shakeData);
                if (activatesInteractables)
                {
                    InteractableComponent ic = hitObject.GetComponent<InteractableComponent>();
                    if (ic == null)
                    {
                        //Debug.Log("Throwable hit an object, it didn't have an IC, search parents");
                        ic = hitObject.GetComponentInParent<InteractableComponent>();
                    }
                    if (ic != null)
					{
                        //Debug.Log("IC Found in parent, notify interaction");
						Vector3 distance = this.transform.position - ic.gameObject.transform.position;
//						ic.NotifyInteraction(new InteractableInteractEventData(this.gameObject, false, distance.magnitude));
						ic.NotifyDashed(new InteractableInteractEventData(this.gameObject, true, distance.magnitude));
                    }
                }
				bool chargerFace = false;
                if (hitObject.GetComponentInChildren<HealthComponent>() != null)
                {
					if (hitObject.GetComponentInChildren<ChargingBruteMonster>() != null){
//						Debug.Log("ChargingBruteMonster");
						//Debug.Log("ChargingBruteMonster" + col.contacts[0].point);
						ChargingBruteMonster charger = hitObject.GetComponentInChildren<ChargingBruteMonster>();
						if(!charger.CurrentStateIsReadyingCharge()){
							if (Vector3.Angle (
								Vector3.Scale (new Vector3 (1f, 0f, 1f), hitObject.transform.forward),
								Vector3.Scale (new Vector3 (1f, 0f, 1f), col.contacts[0].point - hitObject.transform.position )
								) < 50f) {
								
								//							Debug.Log("Hit Charger Face");
								chargerFace = true;
								this.Throw(col.contacts[0].normal);
							}
						}
			
					//	Debug.Log("hitObject.transform.position" + hitObject.transform.position);
					//	Debug.Log("col.contacts[0].point - hitObject.transform.position " + (col.contacts[0].point - hitObject.transform.position ));
					//	Debug.Log("col.contacts[0].point" + col.contacts[0].point);

					//	Debug.Log(" hitObject.transform.forward" + hitObject.transform.forward);




					}
					if(!chargerFace){
                    	_attackBase.Attack(hitObject.transform);
					}
                }

				if (explodeOnImpact && !chargerFace)
                {
                    // Impact effect goes here!
                    Vector3 attacker = hitObject.transform.position;
                    Vector3 dead = transform.position;
                    EffectBase newInstance = impactEffect.GetInstance(attacker);
                    newInstance.transform.LookAt(dead);
                    newInstance.transform.position = dead;
                    newInstance.PlayEffect();

					playImpactSound();

                    Destroy(this.gameObject);
                }
            }
        }
    }

    void Update(){
        if (!_pickedUp)
        {
            rigidbody.detectCollisions = true;
            rigidbody.useGravity = true;
            collider.enabled = true;
            Interactable.enabled = true;
            Interactable.IsInteractable = true;
            rigidbody.detectCollisions = true;
        }
    }

    private bool waitframe = false;
    void FixedUpdate()
    {

        if (!kickForceApplied)
        {
            if (waitframe)
            {
                this.rigidbody.AddForce(kickForce);
				this.rigidbody.AddRelativeTorque(Random.onUnitSphere*7000);
                kickForceApplied = true;
                waitframe = false;
            }
            else
            {
                waitframe = true;
            }
        }
    }

    public void UpdateStatTracker()
    {
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        if (inventories.Length > 0)
        {
//            string type = gameObject.name;
            StatTracker st = inventories[0].GetComponent<StatTracker>();
            st.InteractableHit(this.gameObject);
        }
    }

    /****************
     * SOUND EFFECTS
     ****************/

	private void playPickUpSound() {
		if (PickUpSoundEffect != null && PickUpSoundEffect.SoundFile != null) {
			SoundInstance s = PickUpSoundEffect.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}

	private void playThrowSound() {
		if (ThrowSoundEffect != null && ThrowSoundEffect.SoundFile != null) {
			SoundInstance s = ThrowSoundEffect.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}
	
	private void playImpactSound() {
		if (ImpactSoundEffect != null && ImpactSoundEffect.SoundFile != null) {
			SoundInstance s = ImpactSoundEffect.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}

	private void startPlayingHoldSound() {
		if (HoldSoundEffect != null && HoldSoundEffect.SoundFile != null) {
			_holdSoundInstance = HoldSoundEffect.CreateSoundInstance(this.gameObject);
			_holdSoundInstance.Play();
		}
	}
	
	private void stopPlayingHoldSound() {
		if (_holdSoundInstance != null) {
			_holdSoundInstance.Stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
			_holdSoundInstance = null;
		}
	}

}
