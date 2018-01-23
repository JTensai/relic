using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {

	private LineRenderer lineRenderer;

	public ParticleSystem pSystem;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(pSystem != null) {
			ParticleSystem.Particle[] particles = new ParticleSystem.Particle[pSystem.particleCount];
			int verticies = pSystem.GetParticles(particles);
			int increment = Random.Range(1,4);
			lineRenderer.SetVertexCount(verticies/increment);


			for(int i = 0; i < verticies; i+=increment){
				lineRenderer.SetPosition(i, particles[i].position);
			}

		}
	}
}
