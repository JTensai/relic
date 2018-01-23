using UnityEngine;
using System.Collections;


public class ArtifactBehavior : MonoBehaviour {
	public bool IsFinalArtifact = false;
	public LevelBehavior manager;
	public EffectBase IdleEffect;
	private EffectBase IdleNewInstance;
	public EffectBase PickUpFX;
	public string MessageAfterGrab = "Got it!";
	public SoundInformation PickUpSoundEffect;
	public SoundInformation PickUpDialogue;
	public GameObject RelicHolder;

	private InteractableComponent _interactable;

	// Use this for initialization
	void Start () {
		_interactable = GetComponent<InteractableComponent>();
		_interactable.OnInteract += HandleOnInteract;
		_interactable.OnNotify += HandleOnNotify;

		if (manager == null)
		{
			manager = GameObject.FindObjectOfType<LevelBehavior>();
		}
		if(IdleEffect != null)
		{
			IdleNewInstance = IdleEffect.GetInstance(transform.position);
			IdleNewInstance.transform.rotation = this.transform.rotation;
			IdleNewInstance.PlayEffect ();
		}
	}

	void HandleOnInteract (InteractableInteractEventData data)
	{
		if(data.Source.gameObject.CompareTag("Player")){
			data.Source.gameObject.GetComponent<PlayerController>().PickUpArtifact();
			GameUI.DisplayInstructionTextArea(MessageAfterGrab, 10f);
			//GameUI.DisplayDialogueTextArea(MessageAfterGrab, 5f); // stay open for 4 seconds
            if (manager != null)
                manager.ArtifactGrabbed();
			if(IdleNewInstance != null)
				IdleNewInstance.StopEffect();
			if(PickUpFX != null)
			{
				EffectBase newInstance = PickUpFX.GetInstance(transform.position);
				newInstance.PlayEffect();
			}
			if(RelicHolder!=null){
				RelicHolder.GetComponent<Animator>().SetTrigger("Open");
			}

			playPickUpSoundEffect();
			playPickUpDialogue();

			GameObject.Destroy(gameObject);
		}
	}

	void HandleOnNotify(InteractableNotifyEventData data){

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/****************
	 * SOUND EFFECTS
	 ****************/

	private void playPickUpSoundEffect() {
		if (PickUpSoundEffect.SoundFile != null) {
			SoundInstance s = PickUpSoundEffect.CreateSoundInstance(gameObject);
			s.Play();
		}
	}

	private void playPickUpDialogue() {
		if (PickUpDialogue.SoundFile != null) {
			SoundInstance s = PickUpDialogue.CreateSoundInstance();
			s.Play();
		}
	}
}
