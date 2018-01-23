using UnityEngine;
using System.Collections;

public class SpawnParticlesBySize : MonoBehaviour {

	public float BaseEmissionRate = 0.5f;

	// Use this for initialization
	void Start () {
		ParticleSystem _pSystem = GetComponent<ParticleSystem> ();
		Vector3 parentScale = transform.parent.localScale;

		float surfaceArea = parentScale.x * parentScale.z;

		_pSystem.emissionRate = BaseEmissionRate * surfaceArea;
		_pSystem.maxParticles = (int) (BaseEmissionRate * surfaceArea * _pSystem.startLifetime);
	}
}
