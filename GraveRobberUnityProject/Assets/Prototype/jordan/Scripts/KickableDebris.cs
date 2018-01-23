using UnityEngine;
using System.Collections;

public class KickableDebris : MonoBehaviour 
{
//	private bool inAir = false;
	private float _smallNumber = .3f;
	public EffectBase kickEffect;
	public GameObject DirectionIndicator;
	public float kickStrength = 700f;
	private AttackBase _attackBase;

	private float inAirTimer = 0.0f;

	private Vector3 kickForce;
//	private bool kickForceApplied = true;

	GameObject indicator;
	MeshRenderer indicatorRenderer;
	private Transform indicatorPos;
	
	InteractableComponent interactable;
	
	public SoundInformation KickSoundEffect;
	public SoundInformation ImpactSoundEffect;

    private bool hasBeenKicked = false;
	private bool kickable = true;

    private BoxCollider initialWall;
    private SphereCollider sphere;
	
	// Use this for initialization
	void Start () 
	{
		interactable = gameObject.GetComponent<InteractableComponent> ();
		interactable.OnDashed += HandleOnDashed;
//		interactable.OnInteract += HandleOnInteract;
		interactable.OnNotify += HandleOnNotify;
		this._attackBase = this.gameObject.GetComponent<AttackBase> ();

        initialWall = this.gameObject.GetComponent<BoxCollider>();

        sphere = gameObject.AddComponent<SphereCollider>();
        sphere.center = new Vector3(0, 0, 0);
        sphere.radius = 1f;
        sphere.enabled = false;
	}

//	void HandleOnInteract (InteractableInteractEventData data)
//	{
//        //Debug.Log ("Boulder Interacted");
//		// Set UI message ("That is a big boulder! Maybe I should try Dashing (B) into it.");
//	}

	void HandleOnNotify (InteractableNotifyEventData data)
	{		
        //Debug.Log ("Boulder Notify");
		// Set UI message ("That is a big boulder! Maybe I should try Dashing (B) into it.");
	}

	void HandleOnDashed (InteractableInteractEventData data)
	{
        if (kickable)
        {
            Destroy(initialWall);
            sphere.enabled = true;
			kickable = false;
			hasBeenKicked = true;

            this.rigidbody.isKinematic = false;
            Vector3 sourcePosition = data.Source.transform.position;
            Vector3 debrisPosition = this.transform.position;
            Vector3 dir = debrisPosition - sourcePosition;
            dir = new Vector3(dir.x, 0, dir.z).normalized;


            Vector3 toKick = (dir + new Vector3(0, 0, 0));
            this.rigidbody.AddForce(toKick * kickStrength);
            
            if (kickEffect != null)
            {
                kickEffect.PlayEffect();
            }

            playKickSound(new Vector3());
        }
		else{
			Debug.Log("Not Kickable");
		}
	}

	void OnCollisionEnter(Collision col)
	{
        if (!hasBeenKicked)
        {
			return; //If you are not in the air, then don't attack stuff.
        }

        GameObject hitObject = col.collider.gameObject;
        MonsterBase m = hitObject.GetComponentInChildren<MonsterBase>();
		
		if(m != null && (rigidbody.velocity.magnitude > 1 || inAirTimer < 0.5f)){
			_attackBase.Attack(m.transform);
			playImpactSound(col.relativeVelocity);
		}
		
	}
	
	void Update () 
	{
		if (hasBeenKicked)
        {
            //Debug.Log(this.rigidbody.velocity.magnitude);
			inAirTimer += Time.deltaTime;
			if (inAirTimer > .3f && this.rigidbody.velocity.magnitude < _smallNumber)
			{
				this.rigidbody.velocity = Vector3.zero;
                this.rigidbody.isKinematic = true;
				hasBeenKicked = false;
				inAirTimer = 0;
			}
		}
		kickable = true;
	}	
	
	/****************
	 * SOUND EFFECTS
	 ****************/
	
	private void playKickSound(Vector3 velocity) {
		if(KickSoundEffect != null && KickSoundEffect.SoundFile != null){
			SoundInstance s = KickSoundEffect.CreateSoundInstance(this.gameObject);
			//TODO: set the velocity as a parameter
			s.Play();
		}
	}
	
	private void playImpactSound(Vector3 velocity) {
		if(ImpactSoundEffect != null && ImpactSoundEffect.SoundFile != null){
			SoundInstance s = ImpactSoundEffect.CreateSoundInstance(this.gameObject);
			//TODO: set the velocity as a parameter
			s.Play();
		}
	}
	
}
