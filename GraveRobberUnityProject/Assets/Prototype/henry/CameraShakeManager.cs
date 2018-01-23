using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraShakeManager : MonoBehaviour {
	private static CameraShakeManager _instance;
	private static CameraShakeManager Instance{
		get{
			if(_instance == null){
				_instance = GameObject.FindObjectOfType<CameraShakeManager>();
			}
			return _instance;
		}
	}

	private List<CameraShakeData> _currentShakes;
    //private CameraManagerScript _cameraManager;

	// Use this for initialization
	void Start () {
		_currentShakes = new List<CameraShakeData>();
        //_cameraManager = transform.parent.GetComponent<CameraManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 offset = Vector3.zero;
		float deltaTime = Time.deltaTime;
		for(int i = 0;i<_currentShakes.Count;i++){
			CameraShakeData shake = _currentShakes[i];
		//foreach(CameraShakeData shake in _currentShakes){
			shake.Update(deltaTime);
			offset += shake.CalculatedOffset;
		}
        //Quaternion rotateBy = Quaternion.LookRotation(_cameraManager.CurrentForward);
		_currentShakes.RemoveAll(x => x.IsDone);
		//float offsetMagnitude = offset.magnitude;
        //offset = rotateBy * offset;//Vector3.RotateTowards(offset, _cameraManager.CurrentForward, 360, 360).normalized * offsetMagnitude;


		this.transform.localPosition = offset;
	}

	public void ShakeCamera(CameraShakeData data){
		_currentShakes.Add (data);
	}


	public static void Shake(CameraShakeData data){
		Instance.ShakeCamera(data);
	}

	[System.Serializable]
	public class CameraShakeData{
		public Vector3 Direction;
		public float Duration;
		public int Frequency;

		public Vector3 CalculatedOffset{get;private set;}

		public bool IsDone {get;private set;}

		private float _elapsedTime;
		private float _totalTimeline;

		public CameraShakeData(Vector3 Direction, float Duration, int Frequency){
			this.Direction = Direction;
			this.Duration = Duration;
			this.Frequency = Frequency;
			
			_totalTimeline = 360 * Mathf.Deg2Rad * Frequency;

			IsDone = false;
		}

		public void Update(float deltaTime){
			if(!IsDone){
				_elapsedTime += deltaTime;
				
				if(_elapsedTime <= Duration){
					float percentage = _elapsedTime / Duration;
					float progress = _totalTimeline * percentage;

					float sin = Mathf.Sin(progress);
					CalculatedOffset = Direction * sin * (1-percentage);
				}
				else{
					CalculatedOffset = Vector3.zero;
					IsDone = true;
				}
			}
		}

		public CameraShakeData Clone(){
			CameraShakeData retVal = new CameraShakeData(Direction, Duration, Frequency);
			return retVal;
		}
	}

	[System.Serializable]
	public class CameraShakeEventHandler{
		public CameraShakeData[] CameraShakes;

		public void PlayCameraShakes(){
			for(int i = 0;i<CameraShakes.Length;i++){
				CameraShakeData d = CameraShakes[i];
		//	foreach(CameraShakeData d in CameraShakes){
				CameraShakeManager.Instance.ShakeCamera(d.Clone());
			}
		}
	}


}
