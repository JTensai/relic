using UnityEngine;
using System.Collections;

public class WalkablePillarv3 : MonoBehaviour {
	public GameObject RotationalPivotPoint;
	public GameObject FallingPivotPoint;
	public GameObject PillarBody;
	public float Duration = 1f;
	private float _elapsedTime;
	private bool doThing;
	public AnimationCurve FallCurve;
	private bool freeze;	
	private bool hasFallen;
	
	public enum FallDirectionType{Single, Four, Any};
	public FallDirectionType FallDirection = FallDirectionType.Single;

	public bool makesBridge;
	public bool crumbles;	
	public GameObject[] RemainingRubble = new GameObject[4];//for kickable parts

	public SoundInformation FallingSoundEffect;
	public SoundInformation ImpactSoundEffect;

	private Transform curPos;
	private Transform lastPos;

	private InteractableComponent _interactable; 
	// Use this for initialization
	void Start () {
		_interactable = GetComponentInChildren<InteractableComponent>();
		_interactable.OnInteract += HandleOnInteract;
		hasFallen = false;
		freeze = false;
		_elapsedTime = 0f;
		//_interactable.OnNotify
	}

	public void HandleOnInteract (InteractableInteractEventData data)
	{
		if (_interactable.IsInteractable){
			_interactable.IsInteractable = false;
			Vector3 dir = new Vector3(0,0,0);

			switch(FallDirection){
			case FallDirectionType.Single:
				dir = new Vector3(0,0,1).normalized;
				break;
				
			case FallDirectionType.Four:
				dir = transform.position - data.Source.gameObject.transform.position;
				if (dir.x >= 0){
					if (dir.z >= 0){
						if (dir.x >= dir.z)
							dir = new Vector3(0.7f, 0.3f, 0); // SE		
						else
							dir = new Vector3(0, 0.3f, 0.7f); // NE	
					}
					else{					
						if (dir.x >= -dir.z)
							dir = new Vector3(0.7f, 0.3f, 0); // SE			
						else
							dir = new Vector3(0, 0.3f, -0.7f); // SW
					}
				}
				else {
					if (dir.z >= 0f){					
						if (-dir.x >= dir.z) 
							dir = new Vector3(-0.7f, 0.3f, 0); // NW
						else
							dir = new Vector3(0, 0.3f, 0.7f); // NE
					}
					else{
						if (dir.x >= dir.z) 
							dir = new Vector3(0, 0.3f, -0.7f); // SW
						else
							dir = new Vector3(-0.7f, 0.3f, 0); // NW
					}
				}
				dir = new Vector3(dir.x, 0, dir.z).normalized;
				break;
				
			case FallDirectionType.Any:
				dir = transform.position - data.Source.gameObject.transform.position;
				dir = new Vector3(dir.x, 0, dir.z).normalized;
				break;
			}

			// Debug.Log ("Falling!");
			float angleDegs;
			angleDegs = Mathf.Atan2(-dir.z, dir.x) * Mathf.Rad2Deg;
			RotationalPivotPoint.transform.rotation = Quaternion.Euler(new Vector3(0, angleDegs + 90, 0));
			PillarBody.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

			playFallingSound();

			StartCoroutine("Fall");

			this.GetComponentInChildren<NewPillarCollision>().StartFalling();
		}
	}


	// Update is called once per frame
	void Update () {

	}


	public bool HasFallen()
	{
		return hasFallen;
	}

	public void SetFreeze(bool f)
	{
		freeze = f;
	}

	IEnumerator Fall(){
		while(true){
			_elapsedTime += Time.deltaTime;
			if(_elapsedTime >= Duration || freeze){
				break;
			}
			Quaternion target = Quaternion.LookRotation(Vector3.down, this.transform.forward);
			Quaternion src = this.transform.localRotation;
			float delta = FallCurve.Evaluate(_elapsedTime / Duration);
			FallingPivotPoint.transform.localRotation = Quaternion.Slerp(src, target, delta);
			hasFallen = true;

			yield return null;
		}
	}

	public void OnFinishedFalling() {
		//This is called by the NewPillarCollision component.
		//Debug.Log ("Pillar finished falling.");
		playImpactSound();
	}

	private void playFallingSound() {
		if(FallingSoundEffect.SoundFile != null){
			SoundInstance s = FallingSoundEffect.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}

	private void playImpactSound() {
		if(ImpactSoundEffect.SoundFile != null){
			SoundInstance s = ImpactSoundEffect.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}
}
