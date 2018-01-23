using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChargingBruteMonster : PathingMonsterBase
{
    public float StartingSpeed;
    public float MaxSpeed;
    public float RampUpDuration;
    public float RampDownDuration;
    public float ChargeCooldownDuration;
    public float ReadyingChargeDuration;
    public float StunDuration;
    public float LostPlayerDuration;
    public float WalkingSpeed;
    public AnimationCurve RampUpCurve;
    public AnimationCurve RampDownCurve;
    public AnimationCurve StunCurve;

    public SoundInformation PlayerSeenSound;
	public SoundInformation ChargeUpSound;

    public EffectBase ChargingFX;
	private bool FXPlaying = false;

    private VisionBase _allAroundVision;
    private VisionBase _playerInSightVision;

    private MovementComponent _moveComponent;

    private AttackBase _collisionAttack;

    private float _elapsedTime;
    private Vector3 _chargeDirection;
    private float _speedReached;
    private float _timeSincePlayerLost;
    // _slowDownFactor means the monster takes less time to slow down
    // when he hasn't reached max speed.
    private float _slowDownFactor;

    private enum ChargerStates { Waiting, Charging, SlowingDown, Stunned, Cooldown, ReadyingCharge };
    private ChargerStates _currentState;

    private Animator _chargerAnimator;
	private GameObject inventory;
	private StatTracker stats;

    //public bool interactsWithEverything = true;
    public bool interactsWithPillars = true;
    // Use this for initialization
    new void Start()
	{
        _allAroundVision = VisionBase.GetVisionByVariant(VisionEnum.Default, gameObject);
        _playerInSightVision = VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);
        _collisionAttack = AttackBase.GetAttackByVariant(AttackEnum.Charger, gameObject);

        _moveComponent = GetComponent<MovementComponent>();

        _currentState = ChargerStates.Waiting;
        _timeSincePlayerLost = 0f;

        transform.particleSystem.Stop();

        _chargerAnimator = GetComponent<Animator>();
		_chargerAnimator.enabled = false;
		base.Start();

		inventory = GameObject.FindGameObjectWithTag("Inventory");
		if (inventory != null) {
			stats = inventory.GetComponent<StatTracker>();
			if (stats != null){
				stats.incrementTotalChargers();
			}
		}
    }

	void OnBecameVisible(){
		_chargerAnimator.enabled = true;
	}
	
	void OnBecameInvisible(){
		_chargerAnimator.enabled = false;
	}

    // Update is called once per frame
    new void Update()
    {
        //ActivateEntity ();
        if (IsActivated)
        {
            switch (_currentState)
            {
                case ChargerStates.Waiting:
                    //Debug.Log ("Waiting State");

                    _chargerAnimator.SetBool("Charging", false);
                    _chargerAnimator.SetBool("Idle", true);

                    bool playerWasLost = (this.target == null);
                    if (this.FindPlayer() != null)
                    {
                        this.target = this.FindPlayer().transform;
                    }
                    if (this.target != null)
                    {
                        _elapsedTime = 0f;
                        _currentState = ChargerStates.ReadyingCharge;
                        if (playerWasLost)
                        {
							playPlayerSeenSound();
                        }
						else
						{
							playChargeUpSound();
						}
                    }
                    break;
                case ChargerStates.ReadyingCharge:
                    //Debug.Log ("ReadyingCharge State");
                    if (_elapsedTime == 0f)
                    { // just entered this state
                        transform.particleSystem.Play();

                        _chargerAnimator.SetBool("Idle", false);
                        _chargerAnimator.SetBool("Roaring", true);

                        //_moveComponent.Move(-1, Vector3.up);

						//TODO: this should only happen the first time, not every Update()!
						//playChargeUpSound();
                    }
                    _elapsedTime += Time.deltaTime;
                    if (_elapsedTime >= ReadyingChargeDuration)
                    {
                        _elapsedTime = 0f;
                        _currentState = ChargerStates.Charging;
                        Vector3 delta = this.target.position - transform.position;
                        delta = new Vector3(delta.x, 0, delta.z);
                        _chargeDirection = delta.normalized;
                        transform.rotation = Quaternion.LookRotation(delta);
                        transform.particleSystem.Stop();
                        transform.particleSystem.Clear();
                    }
                    else
                    {
                        Vector3 delta = this.target.position - transform.position;
                        delta = new Vector3(delta.x, 0, delta.z);
                        transform.rotation = Quaternion.LookRotation(delta);
                    }
                    break;
                case ChargerStates.Charging:
                    //Debug.Log ("Charging State");

                    _chargerAnimator.SetBool("Roaring", false);
                    _chargerAnimator.SetBool("Charging", true);

                    if (ChargingFX != null && !FXPlaying)
                    {
						ChargingFX.PlayEffect();
						FXPlaying = true;
                    }

                    GameObject[] _playersInConeVision = _playerInSightVision.PlayersInVision();
                    _elapsedTime += Time.deltaTime;
                    if (_playersInConeVision.Length > 0)
                    {
                        _timeSincePlayerLost = 0f;
                    }
                    else
                    {
                        _timeSincePlayerLost += Time.deltaTime;
                    }
                    if (_timeSincePlayerLost < LostPlayerDuration)
                    {
                        //Keep charging!
                        float rampPercentage = Mathf.Min(_elapsedTime, RampUpDuration) / RampUpDuration;
                        rampPercentage = RampUpCurve.Evaluate(rampPercentage);
                        float moveSpeed = StartingSpeed + rampPercentage * (MaxSpeed - StartingSpeed);
                        _speedReached = moveSpeed;
                        _moveComponent.Move(0, moveSpeed * Time.deltaTime * _chargeDirection);
                    }
                    else
                    {
                        //Player left vision, slow down
                        _currentState = ChargerStates.SlowingDown;
                        _slowDownFactor = _speedReached / MaxSpeed;
                        if (_slowDownFactor == 0)
                        {
                            _currentState = ChargerStates.Waiting;
                            this.target = null;
                        }
                        _elapsedTime = 0f;
                    }
                    //HandleFrontCollision();
                    break;
                case ChargerStates.SlowingDown:
                    //Debug.Log ("SlowingDown State");

                    _chargerAnimator.SetBool("Charging", false);
                    _chargerAnimator.SetBool("Idle", true);

                    _elapsedTime += Time.deltaTime;
                    float rampDownPercentage = Mathf.Min(_elapsedTime, RampDownDuration * _slowDownFactor) / (RampDownDuration * _slowDownFactor);
                    rampDownPercentage = RampDownCurve.Evaluate(rampDownPercentage);
                    float rampDownMoveSpeed = _speedReached * rampDownPercentage;
                    _moveComponent.Move(0, rampDownMoveSpeed * Time.deltaTime * _chargeDirection);

                    if (_elapsedTime >= RampDownDuration * _slowDownFactor)
                    {
                        _currentState = ChargerStates.Cooldown;
                        _elapsedTime = 0f;
                    }
                    //HandleFrontCollision();
                    break;
                case ChargerStates.Cooldown:
                    //Debug.Log ("CoolingDown State");
					if (ChargingFX != null){
						ChargingFX.StopEffect();
					}
					FXPlaying = false;
                    _chargerAnimator.SetBool("Charging", false);
                    _chargerAnimator.SetBool("Idle", true);

                    _elapsedTime += Time.deltaTime;
                    if (FindPlayer() != null)
                    {
                        this.target = FindPlayer().transform;
                    }
                    if (_elapsedTime >= ChargeCooldownDuration)
                    {
                        _elapsedTime = 0f;
                        _currentState = ChargerStates.Waiting;
                    }
                    else if (this.target != null)
                    {
                        // Inherited magical pathfinding voodoo
                        this.speed = WalkingSpeed;
                        Vector3 dir = CalculateVelocity(GetFeetPosition());
                        RotateTowards(targetDirection);
                        dir.y = 0;
                        _moveComponent.Move(0, dir * Time.deltaTime);
                    }
                    break;
                case ChargerStates.Stunned:
                    //Debug.Log ("Stunned State");

                    _chargerAnimator.SetBool("Charging", false);
                    _chargerAnimator.SetBool("Idle", true);

                    if (_elapsedTime == 0)
                    { // just entered this state
                        _slowDownFactor = _speedReached / MaxSpeed;
                        // invert the charge direction
                        _chargeDirection.x = -_chargeDirection.x;
                        _chargeDirection.z = -_chargeDirection.z;
                    }
                    _elapsedTime += Time.deltaTime;
                    float stunPercentage = Mathf.Min(_elapsedTime, StunDuration) / StunDuration;
                    // Useful if we want the monster to spend less time stunned if the charge didn't get up to full speed:
                    //float stunPercentage = Mathf.Min (_elapsedTime, StunDuration * _slowDownFactor) / (StunDuration * _slowDownFactor);
                    stunPercentage = StunCurve.Evaluate(stunPercentage);
                    float stunMoveSpeed = _speedReached * stunPercentage;
                    _moveComponent.Move(0, stunMoveSpeed * Time.deltaTime * _chargeDirection);
                    //if(_elapsedTime >= StunDuration * _slowDownFactor){
                    if (_elapsedTime >= StunDuration)
                    {
                        _currentState = ChargerStates.Cooldown;
                    }
                    break;
            }
        }
    }

	public bool CurrentStateIsReadyingCharge(){
		return _currentState == ChargerStates.ReadyingCharge;
	}

    GameObject FindPlayer()
    {
        GameObject[] players = _allAroundVision.PlayersInVision();
        if (players.Length > 0)
        {
            return players[0];
        }
        else
        {
            return null;
        }
    }

    public void OnExtendedControllerColliderHit(ExtendedControllerColliderHit hit)
    {
        GameObject hitObject = hit.gameObject;
       // Debug.Log ("ExtendedControllerColliderHit: " + hitObject.name);
        // check it's environment through the gam
        // hit.gameObject.l

        if (_currentState != ChargerStates.Charging) return;
        float tolerance = 0.6f;
        // Check to see if we hit a wall AKA not a monster
		//I'm PRETTY sure we don't need that 'not monster' check anymore -Henry
		if (hit.gameObject.layer == LayerMask.NameToLayer("Static Environment"))// && hit.gameObject.GetComponentInChildren<MonsterBase>() == null)
        {
            // if we hit a wall (with some tolerance for a slanted wall
            // &&
            // if the hit was in front of us
            if (Mathf.Abs(hit.normal.y) < tolerance && this.transform.InverseTransformPoint(hit.point).normalized.z > (1f - tolerance))
            {
                // We rammed a wall.
                //Debug.Log ("Rammed a wall");
                _currentState = ChargerStates.Stunned;
                _elapsedTime = 0f;
            }
        }

        //if (interactsWithEverything)
        //{
        //   // Debug.Log("Charger hit an interactable: " + hitObject.name);
        //    InteractableComponent ic = hitObject.transform.GetComponent<InteractableComponent>();
        //    if (ic == null)
        //        ic = hitObject.transform.GetComponentInParent<InteractableComponent>();
        //    if (ic == null)
        //        ic = hitObject.transform.GetComponentInChildren<InteractableComponent>();

        //    if (ic != null)
        //    {
        //        ic.NotifyDashed(new InteractableInteractEventData(this.gameObject, false, Vector3.Distance(ic.gameObject.transform.position, transform.position)));
        //        _currentState = ChargerStates.Stunned;
        //        _elapsedTime = 0f;
        //    }
        //}
        else if (interactsWithPillars && hit.gameObject.layer == LayerMask.NameToLayer("Interactables"))
        {
            Pillar pillar = hitObject.transform.GetComponentInParent<Pillar>();
            if (pillar != null)
            {
                InteractableComponent ic = pillar.GetComponent<InteractableComponent>();
                if (ic != null)
                {
                    ic.NotifyDashed(new InteractableInteractEventData(this.gameObject, false, Vector3.Distance(ic.gameObject.transform.position, transform.position)));
                    _currentState = ChargerStates.Stunned;
                    _elapsedTime = 0f;
                }
            }            
        }
    }

    void handleCollision(Collider col)
    {

        if (_currentState != ChargerStates.Charging && _currentState != ChargerStates.SlowingDown)
        {
            return; // No need to attack without charging
        }

		GameObject hitObject = col.gameObject;
		if (hitObject != null && hitObject.GetComponent<HealthComponent>() != null){
	        _collisionAttack.Attack(hitObject.transform);
	        _currentState = ChargerStates.Stunned;
	        _elapsedTime = 0f;    
		}
    }

    void OnTriggerEnter(Collider col)
    {
        handleCollision(col);
    }

    void OnTriggerStay(Collider col)
    {
        handleCollision(col);
    }

	public void playPlayerSeenSound() {
		if(PlayerSeenSound != null && PlayerSeenSound.SoundFile != null) {
			SoundInstance s = PlayerSeenSound.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}

	public void playChargeUpSound() {
		if(ChargeUpSound != null && ChargeUpSound.SoundFile != null) {
			SoundInstance s = ChargeUpSound.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}
}
