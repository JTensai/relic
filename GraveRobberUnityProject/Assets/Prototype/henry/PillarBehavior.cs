using UnityEngine;
using System.Collections;

public class PillarBehavior : EnvironmentBase {
	public float FallSpeed = 1f;

	private AttackBase _collisionAttack;
	private bool _isFalling;
	private HealthComponent _health;
	private Vector3 _fallDirection;

	private GameObject _childToFall;
	private VisionBase _fallCollision;
	private InteractableComponent _interactable;

	private bool _isNotified;
	private GameObject _notifyingObject;
	private GameObject _directionArrow;
	private PillarBreakNew _crumble;

	// Use this for initialization
	void Start () {
		_health = GetComponent<HealthComponent>();
		//_health = GetComponentInParent<HealthComponent>();
		_health.DefaultBehavior = false;
		_health.OnDamage += HandleOnDamage;
		_health.OnDeath += HandleDeath;

		_collisionAttack = AttackBase.GetAttackByVariant(AttackEnum.Default, gameObject);
		print (_collisionAttack.ToString ());
		_childToFall = transform.FindChild("Pivot").gameObject;
		print (_childToFall.ToString ());
		_directionArrow = transform.FindChild("Arrow").GetComponent<SpriteRenderer>().gameObject;
		print (_directionArrow.ToString ());

		_fallCollision = VisionBase.GetVisionByVariant(VisionEnum.Default, gameObject);

		_interactable = GetComponent<InteractableComponent>();
		//_interactable = GetComponentInParent<InteractableComponent>();
		_interactable.OnNotify += HandleOnNotify;
		_interactable.OnInteract += HandleOnInteract;

		_crumble = GetComponentInChildren<PillarBreakNew> ();
	}

	void HandleOnInteract (InteractableInteractEventData data)
	{
		if(data.IsPlayer && !_isFalling){
			_fallDirection = transform.position - data.Source.transform.position;
			_interactable.IsInteractable = false;
			_isFalling = true;
			GameObject.Destroy(_health);
			StartCoroutine(Collapse());
		}
	}

	void HandleOnNotify (InteractableNotifyEventData data)
	{
		if(data.IsPlayer){
			_isNotified = true;
			_notifyingObject = data.Source;
		}

	}

	void HandleDeath ()
	{
		_isFalling = true;
		GameObject.Destroy(_health);
		StartCoroutine(Collapse());
	}

	void HandleOnDamage (DamageEffect damage)
	{
		_fallDirection = transform.position - damage.Source.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(_isNotified){
			_directionArrow.SetActive(true);
			Vector3 forward = transform.position - _notifyingObject.transform.position;
			forward = new Vector3(forward.x, 0, forward.z).normalized;
			_directionArrow.transform.localPosition = forward * 0.75f + new Vector3(0, 1, 0);
			_directionArrow.transform.rotation = Quaternion.LookRotation(forward);
			_directionArrow.transform.rotation = Quaternion.Euler(new Vector3(90, 
			                                                                  _directionArrow.transform.rotation.eulerAngles.y, _directionArrow.transform.rotation.eulerAngles.z));
			_isNotified = false;
		}
		else{
			_directionArrow.SetActive(false);
		}

		if(_isFalling){
			GameObject[] objs = _fallCollision.ObjectsInVision();
			foreach(GameObject g in objs){
				_collisionAttack.Attack(g.transform);
			}
		}
	}

	IEnumerator Collapse(){
		float deltaTime = 0f;
		Vector3 forward = _fallDirection;//_childToFall.transform.forward;
		forward = new Vector3(forward.x, 0, forward.z).normalized;
		/*Vector3 scaleFactor = new Vector3 (.5f, .5f, .5f);
		Vector3 move = forward;
		move.Scale (scaleFactor);
		_childToFall.transform.position = _childToFall.transform.position + move;
		GameObject cylindar = _childToFall.transform.FindChild("Cylinder").gameObject;
		cylindar.transform.position = cylindar.transform.position - move;*/
		_childToFall.transform.rotation = Quaternion.LookRotation(forward);

		float eulerX = 0;
		float eulerY = _childToFall.transform.rotation.eulerAngles.y;

		while(deltaTime <= FallSpeed){
			deltaTime += Time.deltaTime;
			float step = 90 / FallSpeed * Time.deltaTime;
			eulerX += step;
			_childToFall.transform.rotation = Quaternion.Euler(new Vector3(eulerX, eulerY, 0));//Quaternion.RotateTowards(transform.rotation, downQ, step);
			yield return null;
		}
		//Destroy(gameObject);
		_crumble.Break ();

	}

}
