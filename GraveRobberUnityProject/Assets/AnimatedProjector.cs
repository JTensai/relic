using UnityEngine;
using System.Collections;

public class AnimatedProjector: MonoBehaviour {

	public float fps = 5.0f;
	public Texture2D[] frames;

	private int frameIndex;
	private Projector projector;

	void Start(){
		projector = GetComponent<Projector> ();
		NextFrame ();
		InvokeRepeating ("NextFrame", 1 / fps, 1 / fps);
		}

	void NextFrame(){
		if (projector != null){
			projector.material.SetTexture("_ShadowTex", frames[frameIndex]);
		}
		frameIndex = (frameIndex + 1) % frames.Length;
		}
}
