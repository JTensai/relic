using UnityEngine;
using System.Collections;

public class ExplodingAlterBehavior : EnvironmentBase {
	public float WarmupDuration;
	public float TimeAfterWarmupDuration;

	public Gradient RendererColorChangeGradient;
	
	public EffectBase ExplosionEffect;
	public EffectBase ImpactEffect;
	
	public SoundInformation ActivatedSoundEffect;
	public SoundInformation ExplosionSoundEffect;
	public SoundInformation ImpactSoundEffect;

	public RumbleManager.RumbleEventHandler ExplosionRumbles;
	public CameraShakeManager.CameraShakeEventHandler ExplosionCameraShake;

	public DamageEffect ImpactDamage;

	public MovementOverTimeComponent.MovementOverTimeInfo KnockBackMovement;

	public SpriteRenderer FinalRangeOutline;
	public SpriteRenderer FillingRangeOutline;

	private InteractableComponent _interactable;
	private VisionBase _explosionVision;
	private bool _isInteracted;
	private float _elapsedTime;
	private Renderer _rendererToTint;
	private float _range;
	// Use this for initialization
	void Start () {
		_interactable = GetComponent<InteractableComponent>();
		_interactable.OnInteract += HandleOnInteract;
		_rendererToTint = GetComponentInChildren<Renderer>();
		_explosionVision = VisionBase.GetVisionByVariant(VisionEnum.Default, gameObject);

		_range = ((VisionArc)_explosionVision).Distance;

		FinalRangeOutline.gameObject.SetActive(false);
		FillingRangeOutline.gameObject.SetActive(false);
	}

	void HandleOnInteract (InteractableInteractEventData data)
	{
		_isInteracted = true;
		if(ActivatedSoundEffect.SoundFile != null){
			ActivatedSoundEffect.CreateSoundInstance(gameObject).Play();
		}
		FinalRangeOutline.gameObject.SetActive(true);
		FillingRangeOutline.gameObject.SetActive(true);
		FinalRangeOutline.transform.localScale = new Vector3(_range, _range, 1f);
		FillingRangeOutline.transform.localScale = new Vector3(0, 0, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(_isInteracted){
			_elapsedTime += Time.deltaTime;
			float colorPercentage = Mathf.Clamp(_elapsedTime / WarmupDuration, 0, 1);
			if(_rendererToTint != null){
				_rendererToTint.material.color = RendererColorChangeGradient.Evaluate(colorPercentage);
			}
			FillingRangeOutline.transform.localScale = new Vector3(colorPercentage * _range, colorPercentage * _range, 1f);

			if(_elapsedTime >= WarmupDuration + TimeAfterWarmupDuration){
				if(ExplosionSoundEffect.SoundFile != null){
					ExplosionSoundEffect.CreateSoundInstance(gameObject).Play();
				}
				if(ExplosionEffect != null){
					EffectBase newInstance = ExplosionEffect.GetInstance(gameObject.transform.position);
					newInstance.PlayEffect();
				}
				ExplosionRumbles.PlayRumbles();
				ExplosionCameraShake.PlayCameraShakes();

				GameObject[] players = _explosionVision.PlayersInVision();
				GameObject[] monsters = _explosionVision.MonstersInVision();
				Debug.Log ("I'm exploding, I see " + players.Length + " players and " + monsters.Length + " monsters!");

				foreach(GameObject o in players){
					Vector3 direction = o.transform.position - gameObject.transform.position;
					KnockBackMovement.ActivateMovement(o, direction.normalized);

					if(ImpactEffect != null){
						EffectBase newInstance = ImpactEffect.GetInstance(o.transform.position);
						newInstance.PlayEffect();
					}
					if(ImpactSoundEffect.SoundFile != null){
						ImpactSoundEffect.CreateSoundInstance(o);
					}

					HealthComponent h = o.GetComponent<HealthComponent>();
					if(h != null){
						h.Damage(ImpactDamage,AttackEnum.Default);
					}
				}

				foreach(GameObject o in monsters){
					Vector3 direction = o.transform.position - gameObject.transform.position;
					KnockBackMovement.ActivateMovement(o, direction.normalized);

					if(ImpactEffect != null){
						EffectBase newInstance = ImpactEffect.GetInstance(o.transform.position);
						newInstance.PlayEffect();
					}
					if(ImpactSoundEffect.SoundFile != null){
						ImpactSoundEffect.CreateSoundInstance(o);
					}

					HealthComponent h = o.GetComponent<HealthComponent>();
					if(h != null){
						h.Damage(ImpactDamage, AttackEnum.Default);
					}
				}

				GameObject.Destroy(gameObject);
			}
		}
	}
}
