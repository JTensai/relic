using UnityEngine;
using System.Collections;

public class FMODAutoplay : MonoBehaviour {
	public SoundInformation ToPlay;
	// Use this for initialization
	void Start () {
		if (ToPlay.SoundFile != null) {
			ToPlay.Initialize();
			SoundInstance s = ToPlay.CreateSoundInstance();
			s.SetParameter("Intensity", 0.3f);
			s.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
