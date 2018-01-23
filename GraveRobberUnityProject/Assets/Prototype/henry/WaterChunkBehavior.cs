using UnityEngine;
using System.Collections;

public class WaterChunkBehavior : EnvironmentBase {
//	public EffectBase ElectricalEffect;
//	private AttackArea _electricalAttack;
	private VisionCube _collisionVisionCube;
	Vector2 _dimensions;
//	public bool StartingState = true;
	private bool _isWaterActivated = true;
	private bool _isInitialized = false;
	// old variables used to slow player in the water
//	private float playerSpeed = 0;
//	private float halfSpeed = 0;
	private PlayerController pController = null;
	public float slowDuration = 3f;
	public float speedMultiplier = 0.5f;
	public bool DoesAttack = true;
	// Use this for initialization
	private AttackArea _areaAttack;

	void Start () {
		if (StartsActivated) {
			ActivateEntity();
				}
//		_collisionVisionCube = (VisionCube)VisionBase.GetVisionByVariant(VisionEnum.Default, gameObject);
		_collisionVisionCube = gameObject.AddComponent<VisionCube>();
		_collisionVisionCube.SetSize (new Vector3 (2, 0.5f, 2));
		_collisionVisionCube.SetCenter (new Vector3 (0,-0.25f,0));
		_collisionVisionCube.ToIgnore = ((1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Monster")));

		_dimensions = new Vector2(transform.localScale.x, transform.localScale.z);
		_areaAttack = AttackArea.GetAttackByVariant (AttackEnum.PoisonWater, gameObject);
		Vector3 newVisionSize = _collisionVisionCube.GetSize();
		newVisionSize = new Vector3(newVisionSize.x * _dimensions.x, newVisionSize.y, newVisionSize.z * _dimensions.y);
		//_collisionVisionCube.SetSize(newVisionSize);
//		_isWaterActivated = StartingState;
//		float squareUnitIncrease = _dimensions.x * _dimensions.y;

//		if (ElectricalEffect != null)
//			ElectricalEffect.MultiplyAnimationRate(squareUnitIncrease);
		pController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
//		playerSpeed = pController.WalkSpeed;
//		halfSpeed = playerSpeed * speedMultiplier;
//		_electricalAttack = AttackArea.GetAttackByVariant(AttackEnum.Default, gameObject);
	}
	
	// Update is called once per frame
	void Update () {
//		if (IsActivated) {
		if(!_isInitialized){
			_isInitialized = true;
//				HandleActivationState();
		}
		if (_isWaterActivated && DoesAttack && IsActivated) {
        //foreach (GameObject m in _collisionVisionCube.MonstersInVision())
        //{
        //    AttackArea.GetAttackByVariant(AttackEnum.Default, gameObject).Attack(m.transform);
        //}
        //foreach (GameObject p in _collisionVisionCube.PlayersInVision())
        //{
        //    AttackArea.GetAttackByVariant(AttackEnum.Default, gameObject).Attack(p.transform);
        //}
        // this should most likely fix the persistant water damaging bug. It was caused because old functions were being called from an inherited class. These old functions did not clear the list sometimes of objects being seen.
	        foreach (GameObject o in _collisionVisionCube.ObjectsInVisionV4())
			{
				_areaAttack.Attack(o.transform);

				if(o.layer == LayerMask.NameToLayer("Player")){
					pController.IsWalkingInWater = true;
				}
	        }
			foreach(GameObject o in _collisionVisionCube.ObjectsLeftVision()){
				if(o != null && o.layer == LayerMask.NameToLayer("Player")){
					pController.IsWalkingInWater = false;
				}
			}
		}
//		}
	}

//	void OnTriggerEnter(Collider c){
//		if (c.gameObject.CompareTag ("Player")) {
//			pController.WalkSpeed = halfSpeed;
//			pController.IsWalkingInWater = true;
//		}
//	}
//
//	void OnTriggerStay(Collider c){
//		if (c.gameObject.CompareTag("Player") && pController.WalkSpeed != halfSpeed) {
//			pController.WalkSpeed = halfSpeed;
//		}
//
//		if (c.gameObject.CompareTag("Player") && !pController.IsWalkingInWater) {
//			pController.IsWalkingInWater = true;
//		}
//	}
//
//	void OnTriggerExit(Collider c){
//		if (c.gameObject.CompareTag ("Player")) {
//			StartCoroutine(pController.RemoveSlow(slowDuration));
//			pController.IsWalkingInWater = false;
//			// player automatically resets its movement to full speed over time after leaving the water
//		}
//	}

//	public override void TriggeredActions (bool active)
//	{
//		base.TriggeredActions (active);
//		_isWaterActivated = active;
//		HandleActivationState();
//	}

//	private void HandleActivationState(){
//		if(_isWaterActivated){			
//			if (ElectricalEffect != null)
//				ElectricalEffect.PlayEffect();
//		}
//		else{
//			if (ElectricalEffect != null)
//				ElectricalEffect.StopEffect();
//		}
//	}
}
