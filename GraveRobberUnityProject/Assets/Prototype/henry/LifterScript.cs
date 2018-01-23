using UnityEngine;
using System.Collections;

public class LifterScript : MonsterBase {

	public float RunSpeed = 3f;
	public float WalkSpeed = 1f;

	private VisionBase _runFromPlayerVision;
	private VisionBase _pickSomeoneUpVision;
	private VisionBase _everythingVision;

	private MovementComponent _move;

	private Vector3 _startPos;

	// Use this for initialization
	void Start () {
		_runFromPlayerVision = VisionBase.GetVisionByVariant(VisionEnum.Default, gameObject);
		_pickSomeoneUpVision = VisionBase.GetVisionByVariant(VisionEnum.Variant1, gameObject);
		_everythingVision = VisionBase.GetVisionByVariant(VisionEnum.Variant2, gameObject);

		_move = GetComponent<MovementComponent>();

		_startPos = transform.position;

		ActivateEntity();
	}
	
	// Update is called once per frame
	void Update () {
		if(IsActivated){
			GameObject[] playersInVision = _runFromPlayerVision.PlayersInVision();
			if(playersInVision.Length > 0){
				GameObject player = playersInVision[0];
				Vector3 direction = player.transform.position - transform.position;
				direction = -new Vector3(direction.x, 0, direction.z).normalized;
				_move.Move(0, direction * RunSpeed * Time.deltaTime);
			}
			else{
				if(Vector3.Distance(transform.position, _startPos) > 0.1f){
					Vector3 dir = _startPos - transform.position;
					dir.Normalize();
					_move.Move(0, dir * WalkSpeed * Time.deltaTime);
				}
			}

			GameObject[] monstersInVision = _pickSomeoneUpVision.MonstersInVision();
			if(monstersInVision.Length > 0){
				GameObject monster = monstersInVision[0];
				if(monster.GetComponent<MonsterBase>().IsActivated){
					StartCoroutine("ThrowTarget", new object[] {monster, _everythingVision.PlayersInVision()[0].transform.position});
				}

			}


		}
	}

	IEnumerator ThrowTarget(object[] prams){
		GameObject toThrow = (GameObject)prams[0];
		Vector3 target = (Vector3)prams[1];
		Vector3 startPosition = toThrow.transform.position;
		Vector3 directionDelta = target - startPosition;
		directionDelta = new Vector3(directionDelta.x, 0, directionDelta.z).normalized;

		float Height = 5;
		float HorizontalDistance = Vector3.Distance(target, startPosition);
		float Speed = 2f;
		float _elapsedTime = 0f;

		MovementComponent move = toThrow.GetComponent<MovementComponent>();
		move.AffectedByGravity = false;
		toThrow.GetComponent<MonsterBase>().DeactivateEntity();

		Vector3 prevPosition = toThrow.transform.position;

		while(_elapsedTime < Speed){
			_elapsedTime += Time.deltaTime;
			float percentage = _elapsedTime / Speed;
			float horizontalMove = HorizontalDistance * percentage;
			float verticalMove = Height * (percentage <= 0.5f?2*percentage:1-(percentage));

			Vector3 newPos = startPosition + new Vector3(horizontalMove * directionDelta.x, verticalMove, horizontalMove * directionDelta.z);
			Vector3 moveDelta = newPos - prevPosition;
			prevPosition = newPos;
			move.Move(10, moveDelta);
			yield return null;
		}

		move.AffectedByGravity = true;
		toThrow.GetComponent<MonsterBase>().ActivateEntity();
	}
}
