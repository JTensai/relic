using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EditorTest : MonoBehaviour {

	public float emission;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ParticleSystem p = GetComponent<ParticleSystem> ();
		p.emissionRate = emission;

	}
}
