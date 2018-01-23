using UnityEngine;
using System.Collections;

public class KickableJunkScript : MonoBehaviour 
{
	private bool inAir = false;
	private float _smallNumber = .001f;
	private AttackBase _attackBase;
	public EffectBase kickEffect;
	public GameObject DirectionIndicator;
	
	public SoundInformation KickSoundEffect;
	public SoundInformation ImpactSoundEffect;
	
	GameObject indicator;
	MeshRenderer indicatorRenderer;
	private Transform indicatorPos;

	InteractableComponent ic;

	// Use this for initialization
	void Start () 
	{
		ic = gameObject.GetComponent<InteractableComponent> ();
		if (ic == null) 
		{
			Debug.LogError("This is a problem! You tried to make something kickable without making it interactable! Add an interactibleComponent to it ASAP!");
		}
		ic.OnInteract += KickMe;
		ic.OnNotify += HandleOnNotify;
		this._attackBase = this.gameObject.GetComponent<AttackBase> ();

		if (_attackBase == null) 
		{
			Debug.LogError("This is a problem! You tried to make something kickable without making it able to damage things! Add an AttackBase component to it ASAP!");
		}
		
		Quaternion rotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
		indicator = (GameObject) Instantiate (DirectionIndicator, new Vector3(0,0.2f,0f)+transform.position, rotation);
		//	Debug.Log ("Pillar Indicator Created");
		indicatorRenderer = indicator.GetComponentInChildren<MeshRenderer> ();
		indicatorRenderer.enabled = false;
		//	Debug.Log ("Pillar Indicator Hidden");
		indicator.transform.parent = this.gameObject.transform;
	}

	void HandleOnNotify (InteractableNotifyEventData data)
	{
		Quaternion rotation = new Quaternion();
		Vector3 dir = transform.position - data.Source.transform.position;
	
		dir = new Vector3(dir.x, 0, dir.z).normalized;
		Debug.Log (dir);
		float angleDegs;
		angleDegs = Mathf.Atan2(-dir.z, dir.x) * Mathf.Rad2Deg;
		rotation = Quaternion.Euler(new Vector3(0, angleDegs+90, 0));
		indicator.transform.rotation = rotation;
		indicatorRenderer.enabled = true;
	}

	void KickMe (InteractableInteractEventData data)
	{
		//Debug.Log ("Got it.");
		Vector3 fromPosition = data.Source.transform.position;
		Vector3 myPosition = this.transform.position;

		Vector3 toKick = ((myPosition - fromPosition).normalized+new Vector3(0,0,0)).normalized;

		this.rigidbody.AddForce (toKick*600);

		this.inAir = true;

		if (kickEffect != null) {
			kickEffect.PlayEffect ();
		}

		//TODO: play this at the right times, not every frame!
		//TODO: pass in the velocity of the kick
		//playKickSound(new Vector3());
	}

	void Update () 
	{
		if (this.inAir == true && this.rigidbody.velocity.magnitude<_smallNumber)
		{
			this.inAir = false;
		}

//		if (indicatorRenderer.enabled && !ic.getShowDirectionIndicator()){
//			indicatorRenderer.enabled = false;
//		}
	}

	void OnCollisionEnter(Collision col)
	{
		GameObject hitObject = col.collider.gameObject;

		MonsterBase m = hitObject.GetComponentInChildren<MonsterBase>();
		
		if(!inAir) 
		{
			return; //If you are not in the air, then don't attack stuff.
		}
		
		if(m != null)
		{
			Debug.Log ("Debris Hit a monster. vel = " + col.relativeVelocity);
			_attackBase.Attack(m.transform);

			playImpactSound(col.relativeVelocity);
		}

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
