using UnityEngine;
using System.Collections;

public abstract class EffectBase : MonoBehaviour {
	public bool IsFinished{get;private set;}
	protected bool _isPlaying;
	protected bool _isInitialized;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(_isPlaying && IsFinished){
			StopEffect();
		}
	}

	public void PlayEffect(){
		this._isPlaying = true;
		this.StartEffect();
	}

	public EffectBase GetInstance(Vector3 position){
		GameObject createdEffect = (GameObject)GameObject.Instantiate(this.gameObject, position, this.transform.rotation);
		EffectBase eff = createdEffect.GetComponent<EffectBase>();
		if(!eff._isInitialized){
			eff.InitializeEffect();
		}
		return eff;
	}

	public EffectBase GetInstance(Vector3 position, Quaternion rotation){
		GameObject createdEffect = (GameObject)GameObject.Instantiate(this.gameObject, position, rotation);
		EffectBase eff = createdEffect.GetComponent<EffectBase>();
		if(!eff._isInitialized){
			eff.InitializeEffect();
		}
		return eff;
	}

	public virtual void StopEffect(){
		GameObject.Destroy(gameObject);
	}

	public virtual void StopEffectImmediately(){
		GameObject.Destroy(gameObject);
	}

	protected abstract void InitializeEffect();
	protected abstract void StartEffect();
	public abstract float GetAnimationRate();
	public abstract void SetAnimationRate(float val);
	public abstract void MultiplyAnimationRate(float mult);
}
