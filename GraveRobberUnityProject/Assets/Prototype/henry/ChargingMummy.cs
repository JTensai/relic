using UnityEngine;
using System.Collections;

public class ChargingMummy : PathingMonsterBase 
{
	public Vector3[] waypoints;
	public int currentWaypoint = 0;
	private Transform temp;

	private Vector3 initialPosition;

	private VisionBase _startVision;
	private VisionBase _stopVision;
	private VisionBase _attackVision;
	private AttackBase _baseAttack;
	private MovementComponent _moveComp;

	private enum MummyStates {Targeting, Charging, Taunt, Waypointing}
	private MummyStates _state;

	private Vector3 _moveDirection;
	private float _elapsedTime;

	public float attackWindup = 1;
	private float attackTimer = 0;
    private bool windingUp = false;

	// time of taunt
	public float tauntTime = 1f; 
	private float defaultChargeTime = 3f;
	private float chargeTime;
	private float chargeTimeStdDeviation = 2.5f;

	private float timer = 0f;

	public EffectBase TauntFX;

	/** Minimum velocity for moving */
	public float sleepVelocity = 0.4F;

	private GameObject inventory;
	private StatTracker stats;

	private Animator _shamblerAnimator;

	public SoundInformation SeePlayerSound;

	// Use this for initialization
	new void Start () 
	{
		base.Awake ();
		_state = MummyStates.Targeting;
		if (waypoints.Length != 0) {
			_state = MummyStates.Waypointing;
			speed = speed/2;
			initialPosition = transform.position;
		}
		_startVision = VisionBase.GetVisionByVariant(VisionEnum.Default, gameObject);
		_stopVision = VisionBase.GetVisionByVariant(VisionEnum.Variant2, gameObject);
		_attackVision = VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);
		_baseAttack = AttackBase.GetAttackByVariant(AttackEnum.Shambler, gameObject);
		_moveComp = GetComponent<MovementComponent>();

		_shamblerAnimator = GetComponent<Animator>();
		_shamblerAnimator.enabled = false;


		base.Start ();

		temp = new GameObject().transform;

		
		inventory = GameObject.FindGameObjectWithTag("Inventory");
		if (inventory != null) {
			stats = inventory.GetComponent<StatTracker>();
			if (stats != null){
				stats.incrementTotalShamblers();
			}
		}
	}

	void OnBecameVisible(){
		_shamblerAnimator.enabled = true;
	}

	void OnBecameInvisible(){
		_shamblerAnimator.enabled = false;
	}
	
	// Update is called once per frame
	new void Update () 
	{
		//ActivateEntity ();
		if(IsActivated)
		{	
			if(_state == MummyStates.Waypointing)
			{
				GameObject playerInVision = _startVision.PlayerInVisionV2();
				if(playerInVision!=null)
				{
					speed = speed*2;
					this.target = playerInVision.transform;
					playSeePlayerSound();
					_state = MummyStates.Charging;
					chargeTime = defaultChargeTime + Random.Range(-chargeTimeStdDeviation, chargeTimeStdDeviation);
				}else {
					temp.position = initialPosition + waypoints[currentWaypoint];
					this.target = temp;
					MoveMummy();
					if(Vector3.Distance(this.transform.position,temp.position) < 1f){
						currentWaypoint = (currentWaypoint +1) % waypoints.Length;
					}
				}
			}
			if(_state == MummyStates.Targeting)
			{
				GameObject playerInVision = _startVision.PlayerInVisionV2();
				if(playerInVision!=null)
				{
					playSeePlayerSound();
					this.target = playerInVision.transform;
					_state = MummyStates.Charging;
					chargeTime = defaultChargeTime + Random.Range(-chargeTimeStdDeviation, chargeTimeStdDeviation);
				}
				/*if(_startVision.PlayersInVision().Length>0)
				{
					this.target = _startVision.PlayersInVision()[0].transform;
					_state = MummyStates.Charging;
				}*/
			}
			if(_state == MummyStates.Charging)
			{
				//Set animations
				_shamblerAnimator.SetBool("move", true);
				_shamblerAnimator.SetBool("attack", false);

	//			Debug.Log("In charge mode!");
				if(_stopVision.PlayerInVisionV2()==null)
				//if(_stopVision.PlayersInVision().Length==0)
				{
					//Debug.Log("Here 3");
					this.target = null;
					_state = MummyStates.Targeting;
				}
				MoveMummy();
				timer+= Time.deltaTime;
				if(timer> chargeTime){
					timer = 0;
					_state = MummyStates.Taunt;
				}
			}
			else{
				//Set animations
				_shamblerAnimator.SetBool("move", false);
				_shamblerAnimator.SetBool("attack", false);
			}
			if(_state == MummyStates.Taunt){
				timer += Time.deltaTime;
		//		Debug.Log("TAUNT");
				if(timer> tauntTime){
					_state = MummyStates.Targeting;
					timer = 0;
				}
				if (TauntFX != null)
				{
					EffectBase newInstance = TauntFX.GetInstance(transform.position);
					newInstance.transform.rotation = transform.rotation;
					newInstance.PlayEffect();
				}

			}

			if (_attackVision != null){
				GameObject o = _attackVision.PlayerInVisionV2 ();
				if(o != null){
					mummyAttack(o);
				}
			}
			if (windingUp)
			{
				if (attackTimer > attackWindup)
				{
					canMove = true;
					windingUp = false;
				}
				attackTimer += Time.deltaTime;
			}
		}

	}

	private void mummyAttack(GameObject o)
    {
		//Set animations
		_shamblerAnimator.SetBool("move", false);
		_shamblerAnimator.SetBool("attack", true);

        if (!windingUp) // begin wind-up
        {
            canMove = false;
            windingUp = true;
            attackTimer = 0;
        }

        //attackTimer += Time.deltaTime;
        if (windingUp && attackTimer > attackWindup)
        {
            _baseAttack.Attack(o.transform);
        }

    }


	private void MoveMummy ()
	{
		if (canMove) 
		{	
			//Calculate desired velocity
			Vector3 dir = CalculateVelocity (GetFeetPosition());
			
			//Rotate towards targetDirection (filled in by CalculateVelocity)
			RotateTowards (targetDirection);
			
			dir.y = 0;
			if (dir.sqrMagnitude > sleepVelocity*sleepVelocity) 
			{
				//If the velocity is large enough, move
			} 
			else 
			{
				//Otherwise, just stand still (this ensures gravity is applied)
				dir = Vector3.zero;
			}
			
			if (_moveComp != null) 
			{
				_moveComp.Move (0, dir*Time.deltaTime);
			} 
			else 
			{
				Debug.LogWarning ("No or CharacterController attached to GameObject");
			}
		}
	}

	public void OnDrawGizmosSelected ()
	{
		if (waypoints != null){
			foreach(Vector3 a in waypoints){
				Gizmos.color = Color.blue;
				Gizmos.DrawSphere (a+ transform.position, .5f);
			}
		}
	}

	public void playSeePlayerSound() {
		if(SeePlayerSound != null && SeePlayerSound.SoundFile != null) {
			SoundInstance s = SeePlayerSound.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}

}

