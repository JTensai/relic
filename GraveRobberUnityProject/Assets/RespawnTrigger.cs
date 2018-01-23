using UnityEngine;
using System.Collections;

public class RespawnTrigger : MonoBehaviour {
	
	public bool showEffect = true;
	public bool showText = true;
	public EffectBase RespawnFX;
	public SoundInformation ActivateSoundEffect;

	private bool activated;

	private void Start() {
		activated = false;
	}

	public void OnTriggerEnter(Collider other) {
		if (!this.activated && other.CompareTag("Player")){
			if (LevelBehavior.Instance != null)
				LevelBehavior.Instance.SetRespawn(this);
			this.activated = true;
			if(showText){
				GameUI.DisplayInstructionTextArea("Checkpoint", 2f);}
			if(showEffect){
				this.GetComponentInChildren<ParticleSystem>().Play();}
			if (ActivateSoundEffect.SoundFile != null && showEffect) {
				SoundInstance s = ActivateSoundEffect.CreateSoundInstance(gameObject);
				s.Play();
			}
		}
	}

	public void PlayRespawnEffect() {
		if(activated && RespawnFX != null){
			EffectBase newInstance = RespawnFX.GetInstance(transform.position);
			newInstance.transform.position = new Vector3(newInstance.transform.position.x, newInstance.transform.position.y - 1, newInstance.transform.position.z);
			newInstance.PlayEffect();
		}
	}

	public Vector3 GetRespawnPosition() {
		return this.gameObject.transform.position;
	}
}
