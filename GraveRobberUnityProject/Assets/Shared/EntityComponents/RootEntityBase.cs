using UnityEngine;
using System.Collections;
using System;

public abstract class RootEntityBase : MonoBehaviour {
	private bool _isActivated;
	public bool IsActivated {
		get{return _isActivated;}
		private set{
			_isActivated = value;
#if UNITY_EDITOR
			Activated = value;
#endif
		}
	}
#if UNITY_EDITOR
	[ReadOnlyAttribute]
	public bool Activated;
#endif
	public bool StartsActivated = false;
	// Use this for initialization
	void Start () {
		IsActivated = false;
		if (StartsActivated) {
			ActivateEntity();
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateEntity(){
	
		IsActivated = true;
		if(OnActivated != null){
			OnActivated(gameObject, null);
		}
	}

	public void DeactivateEntity(){


		IsActivated = false;
		if(OnDeactivated != null){
			OnDeactivated(gameObject, null);
		}
	}

	public event EventHandler OnActivated;
	public event EventHandler OnDeactivated;
}
