using UnityEngine;
using System.Collections;

public class MovementOverTimeComponent : MonoBehaviour {
	private MovementComponent _move;
	private float _elapsedTime;
	private MovementOverTimeInfo _movementInfo;
	private Vector3 _moveDirection;
	// Use this for initialization
	void Start () {
		_move = GetComponent<MovementComponent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_movementInfo != null){
			_elapsedTime += Time.deltaTime;
			float percentageTime = _elapsedTime / _movementInfo.Duration;
			if(percentageTime > 1f){
				GameObject.Destroy(this);
			}
			else{
				float deltaX = _movementInfo.XMovement.Evaluate(_elapsedTime) * _moveDirection.x * _movementInfo.Magnitude * Time.deltaTime;
				float deltaY = _movementInfo.YMovement.Evaluate(_elapsedTime) * _moveDirection.y * _movementInfo.Magnitude * Time.deltaTime;
				float deltaZ = _movementInfo.ZMovement.Evaluate(_elapsedTime) * _moveDirection.z * _movementInfo.Magnitude * Time.deltaTime;

				_move.Move (_movementInfo.MovementPriority, new Vector3(deltaX, deltaY, deltaZ));
			}


		}
	}
	

	[System.Serializable]
	public class MovementOverTimeInfo{
		public float Duration = 1f;
		public float Magnitude = 1f;
		public int MovementPriority = 1;
		public AnimationCurve XMovement = AnimationCurve.Linear(0, 1, 1, 0);
		public AnimationCurve YMovement = AnimationCurve.Linear(0, 0, 1, 0);
		public AnimationCurve ZMovement = AnimationCurve.Linear(0, 1, 1, 0);

		public MovementOverTimeComponent ActivateMovement(GameObject target, Vector3 direction){
			MovementOverTimeComponent retVal = target.AddComponent<MovementOverTimeComponent>();
			retVal._moveDirection = direction;
			retVal._movementInfo = this;
			return retVal;
		}

	}
}
