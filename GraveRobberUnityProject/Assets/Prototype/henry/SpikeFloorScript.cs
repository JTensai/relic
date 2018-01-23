using UnityEngine;
using System.Collections;

public class SpikeFloorScript : EnvironmentBase {
	private AttackBase _spikeAttack;
	private VisionBase _spikeVision;
	// Use this for initialization
	void Start () {
		_spikeAttack = AttackBase.GetAttackByVariant(AttackEnum.Default, gameObject);
		_spikeVision = VisionBase.GetVisionByVariant(VisionEnum.Default, gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(IsActivated){
			GameObject[] inVision = _spikeVision.ObjectsInVision();
			foreach(GameObject g in inVision){
				_spikeAttack.Attack(g.transform);
			}
		}
	}
	
}
