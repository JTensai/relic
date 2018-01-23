using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Pillar : MonoBehaviour {
	public GameObject RotationalPivotPoint;
	public GameObject FallingPivotPoint;
	public GameObject PillarCollider;
	public GameObject FallIndicator;

	public SoundInformation CrashSoundEffect;
	public SoundInformation HitByPlayerSoundEffect;
	public SoundInformation HitByMonsterSoundEffect;

	public float Duration = 1f;
	private float _elapsedTime;
    private bool doThing;
    public AnimationCurve FallCurve;
    //public AnimationCurve SlideCurveZ;
    //public AnimationCurve SlideCurveY;
	private bool falling;
	private bool freeze;	
	private bool hasFallen;
	private bool hitByPlayer;
	
	public enum FallDirectionType{Single, Any, Four};
	public FallDirectionType FallDirection = FallDirectionType.Single;
	
//	public bool makesBridge;
    public bool bridgeIsWedged;
	public bool crumbles;	
	public GameObject[] RemainingRubble = new GameObject[2];//for kickable parts
	
	private Transform curPos;
	private Transform lastPos;

    private Vector3 initialPos;
    private Vector3 finalPos;

    private Vector3 initialPosFallingPivot;
    private Vector3 finalPosFallingPivot;

	public Vector3 scriptedFallDirection =  new Vector3(1f,0,0);

	GameObject indicator;
	MeshRenderer indicatorRenderer;
	private Transform indicatorPos;
    //private bool ableToBePushed = false;
	
	private InteractableComponent _interactable; 
	private GameObject player;

	public EffectBase PlayerHitFX;

	// Use this for initialization
	void Start () {
		_interactable = GetComponentInChildren<InteractableComponent>();
		if (_interactable != null){
			_interactable.OnDashed += HandleOnDashed;
			_interactable.OnLargeNotify += HandleOnLargeNotify;
		}
		falling = false;
		hasFallen = false;
		freeze = false;
		hitByPlayer = false;
		_elapsedTime = 0f;

		// fixes rotation of a round pillar
		if (FallDirection == FallDirectionType.Any){
			GameObject visual = this.gameObject.GetComponentInChildren<MeshRenderer>().gameObject;
			visual.transform.localRotation = Quaternion.Euler(0,this.gameObject.transform.localRotation.eulerAngles.y + visual.transform.localRotation.eulerAngles.y,0);
			this.gameObject.transform.localRotation = Quaternion.identity;
		}

		if (FallIndicator != null)
		{
			Quaternion rotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
			indicator = (GameObject) Instantiate (FallIndicator, new Vector3(0,0.2f,0f)+transform.position, rotation);
			indicatorRenderer = indicator.GetComponentInChildren<MeshRenderer> ();
			indicatorRenderer.enabled = false;
			indicator.transform.parent = this.gameObject.transform;
		}

		initialPos = FallingPivotPoint.transform.localPosition;

		player = GameObject.FindWithTag ("Player");
	}

	void HandleOnLargeNotify (InteractableLargeNotifyEventData data)
	{
		Quaternion rotation = new Quaternion();
		Vector3 dir = transform.position - data.Source.transform.position;
		_interactable.IsInteractable = false;
		switch(FallDirection){
		case FallDirectionType.Single:
			// make sure the pillar only lights up on the correct side
			//Debug.Log (this.transform.rotation.eulerAngles.y % 180);
			if (this.transform.rotation.eulerAngles.y % 180 > 89){
//				Debug.Log("RotateSample");
				dir =  this.gameObject.transform.rotation * -dir;
			}
			else{
				dir = this.gameObject.transform.rotation * dir;
			}
			if (dir.x >= 0){
				if (dir.z >= 0){
					if (dir.x <= dir.z){
						if (indicatorRenderer!= null){
                       		indicatorRenderer.enabled = true;
						}
						_interactable.IsInteractable = true;
					}
				}
			}
			else {
				if (dir.z >= 0f){					
					if (-dir.x <= dir.z) {
						if (indicatorRenderer!= null){
                        	indicatorRenderer.enabled = true;
						}
						_interactable.IsInteractable = true;
					}
				}
			}
			break;
		case FallDirectionType.Any:
			dir = new Vector3(dir.x, 0, dir.z).normalized;
			float angleDegs;
			angleDegs = Mathf.Atan2(-dir.z, dir.x) * Mathf.Rad2Deg;
			rotation = Quaternion.Euler(new Vector3(0, angleDegs+90, 0));
			if (indicatorRenderer!= null){
				indicator.transform.localRotation = rotation;
	            indicatorRenderer.enabled = true;
			}
			_interactable.IsInteractable = true;
			break;
		}
	}

	
	public void HandleOnDashed (InteractableInteractEventData data)
	{
		bool shouldFall = false;
		if (_interactable.IsInteractable){
			Vector3	dir = transform.position - data.Source.transform.position;
			hitByPlayer = data.IsPlayer;

			UpdateStatTracker();

			if (hitByPlayer) {
				playHitByPlayerSound();
				if(PlayerHitFX != null){
					EffectBase newInstance = PlayerHitFX.GetInstance(transform.position);
					newInstance.PlayEffect();
				}
			} else {
				playHitByMonsterSound();
			}

			switch(FallDirection){
			case FallDirectionType.Single:
				// This is needed to solve a problem where if the root pillar was rotated, the side that would knock over the pillar was on the opposite side
//				Debug.Log(this.transform.rotation.eulerAngles.y % 180);
				if (this.transform.rotation.eulerAngles.y % 180 > 89){
					dir =  this.gameObject.transform.localRotation * -dir;
				}
				else{
					dir = this.gameObject.transform.localRotation * dir;
				}

				// Makes sure a single direction pillar was hit on the appropriate side before falling over
				if (dir.x >= 0){
					if (dir.z >= 0){
						if (dir.x <= dir.z){
							shouldFall = true;
						}
					}
				}
				else {
					if (dir.z >= 0f){					
						if (-dir.x <= dir.z) {
							shouldFall = true;
						}
					}
				}
				dir = new Vector3(0,0,1).normalized;
				break;
				
			case FallDirectionType.Four:
				dir = this.gameObject.transform.localRotation * dir;
				if (dir.x >= 0){
					if (dir.z >= 0){
						if (dir.x >= dir.z)
							// source is in the NW
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
				shouldFall = true;
				break;
				
			case FallDirectionType.Any:
				dir = new Vector3(dir.x, 0, dir.z).normalized;
				dir = this.gameObject.transform.localRotation * dir;
				shouldFall = true;
				break;
			}

			if (shouldFall){
				_interactable.IsInteractable = false;
				falling = true;
				
				float angleDegs = (Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg);
//				Debug.Log("angleDegs: " + angleDegs);
				RotationalPivotPoint.transform.localRotation = Quaternion.Euler(new Vector3(0, angleDegs, 0));
				PillarCollider.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

				float xoffset;
				float yoffset;
				float zoffset;
				Vector3 offset;
	            if (bridgeIsWedged)
	            {
					xoffset = 0;
					yoffset = (-FallingPivotPoint.GetComponent<BoxCollider>().size.z / 2) - FallingPivotPoint.transform.localPosition.y;
					zoffset = (FallingPivotPoint.GetComponent<BoxCollider>().size.z / 2) + FallingPivotPoint.transform.localPosition.y;
					
					offset = new Vector3(xoffset, yoffset, zoffset);
	            }
	            else
	            {
					float slideDistance;
					if (FallingPivotPoint.GetComponent<BoxCollider>() != null){
						slideDistance = FallingPivotPoint.GetComponent<BoxCollider>().size.z;
					}
	                else{
						slideDistance = 0.6f;
					}
					slideDistance = slideDistance * this.gameObject.transform.localScale.z;

//					Debug.Log (dir);

					xoffset = 0;
					yoffset = 0;
					zoffset = slideDistance;
					offset = new Vector3(xoffset, yoffset, zoffset);
				}
				finalPos = initialPos + offset;
				
				this.GetComponentInChildren<PillarCollision>().StartFalling();
				StartCoroutine("Fall");
			}
		}
	}

	public void scriptedFall(){
		_interactable.IsInteractable = false;
		falling = true;
		float angleDegs;
		angleDegs = Mathf.Atan2(-scriptedFallDirection.z, scriptedFallDirection.x) * Mathf.Rad2Deg;
		RotationalPivotPoint.transform.rotation = Quaternion.Euler(new Vector3(0, angleDegs + 90, 0));
		PillarCollider.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
		
		float slideDistance = 0.6f;
		float angle = RotationalPivotPoint.transform.localRotation.eulerAngles.y;
		float xoffset = slideDistance * (Mathf.Sin(angle * Mathf.Deg2Rad));
		float zoffset = slideDistance * (Mathf.Cos(angle * Mathf.Deg2Rad));
		finalPos = initialPos + new Vector3 (xoffset, 0, zoffset);
				
		StartCoroutine("Fall");
		
		this.GetComponentInChildren<PillarCollision>().StartFalling();
	}
	
	void Update () {
		if (FallDirection == FallDirectionType.Single && !falling && _interactable != null && !_interactable.IsInteractable){
			Vector3 dir = transform.position - player.transform.position;
			dir = this.gameObject.transform.localRotation * dir;
			if (dir.x >= 0){
				if (dir.z >= 0){
					if (dir.x <= dir.z){
						_interactable.IsInteractable = true;
					}
				}
			}
			else {
				if (dir.z >= 0f){					
					if (-dir.x <= dir.z) {
						_interactable.IsInteractable = true;
					}
				}
			}
		}
		if (_interactable != null && indicatorRenderer != null){
			if (!_interactable.getShowLargeNotification() || falling || !_interactable.IsInteractable)
			{
				indicatorRenderer.enabled = false;
			}
		}
	}
	
	public bool HasFallen()
	{
		return hasFallen;
	}
	
	public void SetFreeze(bool f)
	{
		freeze = f;
	}

	public bool WasHitByPlayer(){
		return hitByPlayer;
	}
	
	IEnumerator Fall(){
//		EditorApplication.isPaused = true;
		Quaternion target = Quaternion.LookRotation(Vector3.down, FallingPivotPoint.transform.forward);
		Quaternion src = FallingPivotPoint.transform.rotation;

	    while (_elapsedTime < Duration && !freeze)
	    {
	        _elapsedTime += Time.deltaTime;
	        float delta = FallCurve.Evaluate(_elapsedTime / Duration);
	        FallingPivotPoint.transform.rotation = Quaternion.Slerp(src, target, delta);
	        FallingPivotPoint.transform.localPosition = Vector3.Lerp(initialPos, finalPos, delta);
			//FallingPivotPoint.rigidbody.MoveRotation(Quaternion.Slerp(src, target, delta));
			//FallingPivotPoint.rigidbody.MovePosition(Vector3.Lerp(initialPos, finalPos, delta));
	        // A SILLY ATTEMPT TO GET IT TO SLIDE INTO WEDGE POSITION CORRECTLY.......
	        //float deltaz = SlideCurveZ.Evaluate(_elapsedTime / Duration);
	        //float deltay = SlideCurveY.Evaluate(_elapsedTime / Duration);
	        //Vector3 zOffsetStart = new Vector3(FallingPivotPoint.transform.position.x, FallingPivotPoint.transform.position.y, FallingPivotPoint.transform.position.z + initialPos.z);
	        //Vector3 zOffsetFinish = new Vector3(FallingPivotPoint.transform.position.x, FallingPivotPoint.transform.position.y, FallingPivotPoint.transform.position.z + finalPos.z);
	        //FallingPivotPoint.transform.position = Vector3.Lerp(zOffsetStart, zOffsetFinish, deltaz);
	        //float newZ = Mathf.Lerp(initialPos.z, finalPos.z, deltaz);
	        //Debug.Log("newZ = " + newZ);

	        //float newY = Mathf.Lerp(initialPos.y, finalPos.y, deltay);
	        //Debug.Log("newY = " + newY);

	        //FallingPivotPoint.transform.position.Set(initialPos.x, newY, newZ);

	        //Vector3 yOffsetStart = new Vector3(FallingPivotPoint.transform.position.x, FallingPivotPoint.transform.position.y + initialPos.y, FallingPivotPoint.transform.position.z);
	        //Vector3 yOffsetFinish = new Vector3(FallingPivotPoint.transform.position.x, FallingPivotPoint.transform.position.y + finalPos.y, FallingPivotPoint.transform.position.z);
	        //FallingPivotPoint.transform.position = Vector3.Lerp(yOffsetStart, yOffsetFinish, deltay);
	        yield return null;
	    }

	    hasFallen = true;
	    playCrashSound();
        
	}

    public void UpdateStatTracker()
    {
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        if (inventories.Length > 0)
        {
//            string type = gameObject.name;
            StatTracker st = inventories[0].GetComponent<StatTracker>();
            st.InteractableHit(this.gameObject);
        }
    }
	
	public void playCrashSound() {
		if(CrashSoundEffect != null && CrashSoundEffect.SoundFile != null){
			SoundInstance s = CrashSoundEffect.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}

	public void playHitByPlayerSound() {
		if(HitByPlayerSoundEffect != null && HitByPlayerSoundEffect.SoundFile != null){
			SoundInstance s = HitByPlayerSoundEffect.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}

	public void playHitByMonsterSound() {
		if(HitByMonsterSoundEffect != null && HitByMonsterSoundEffect.SoundFile != null){
			SoundInstance s = HitByMonsterSoundEffect.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}
}
