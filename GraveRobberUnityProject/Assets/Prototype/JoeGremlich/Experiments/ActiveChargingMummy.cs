using UnityEngine;
using System.Collections;

public class ActiveChargingMummy : PathingMonsterBase 
{
	private VisionBase _startVision;
	private VisionBase _stopVision;
	private VisionBase _attackVision;
	private AttackBase _baseAttack;
	private MovementComponent _moveComp;

	private enum MummyStates {Targeting, Charging}
	private MummyStates _state;

	private Vector3 _moveDirection;
	private float _elapsedTime;

	public float attackWindup = 1;
	private float attackTimer = 0;
    private bool windingUp = false;

	/** Minimum velocity for moving */
	public float sleepVelocity = 0.4F;
	
	// Use this for initialization
	new void Start () 
	{
		base.Awake ();
		_state = MummyStates.Targeting;
		_startVision = VisionBase.GetVisionByVariant(VisionEnum.Default, gameObject);
		_stopVision = VisionBase.GetVisionByVariant(VisionEnum.Variant2, gameObject);
		_attackVision = VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);
		_baseAttack = AttackBase.GetAttackByVariant(AttackEnum.Shambler, gameObject);
		_moveComp = GetComponent<MovementComponent>();

		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () 
	{
		ActivateEntity ();
		if(IsActivated)
		{		
			if(_state == MummyStates.Targeting)
			{
				GameObject playerInVision = _startVision.PlayerInVisionV2();
				if(playerInVision!=null)
				{
					this.target = playerInVision.transform;
					_state = MummyStates.Charging;
				}
				/*if(_startVision.PlayersInVision().Length>0)
				{
					this.target = _startVision.PlayersInVision()[0].transform;
					_state = MummyStates.Charging;
				}*/
			}
			if(_state == MummyStates.Charging)
			{
	//			Debug.Log("In charge mode!");
				if(_stopVision.PlayerInVisionV2()==null)
				//if(_stopVision.PlayersInVision().Length==0)
				{
					//Debug.Log("Here 3");
					this.target = null;
					_state = MummyStates.Targeting;
				}
				MoveMummy();
			}
		}
		GameObject o = _attackVision.PlayerInVisionV2 ();
		if(o!=null){
			mummyAttack(o);
		}

		/*GameObject[] inVision = _attackVision.ObjectsInVision();
		foreach(GameObject o in inVision)
		{
			if(o.GetComponent<PlayerController>() != null)
			{
				mummyAttack(o);
			}
			//return;
		}*/
		//attackTimer = 0;
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

	private void mummyAttack(GameObject o)
    {
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
}

