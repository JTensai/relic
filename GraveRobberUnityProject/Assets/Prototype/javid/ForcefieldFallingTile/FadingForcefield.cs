using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(TriggerDetector))]
public class FadingForcefield : Forcefield {

	//what method 'activates' this
	public enum activationMethods{OnEnter, OnExit, OnStay, OnEnterStartFall};
	//public activationMethods on;
	

	//who can 'activate' this
	public enum triggeringActors{Player, Enemy, Both, None};
	public triggeringActors triggeredBy;
	private int thingsInBounds = 0;

	// 
	//public Vector3 size = new Vector3 (2f, 1f, 4f);
	



	public bool fadeable = true;
	private bool fading = false;
	public bool faded = false;
    //private bool justfaded = false;
	public bool respawns = true;
	public bool regenerates = true;

	public float liveTime = 1f;
	public float respawnDelay = 3f;

	public Color color = new Vector4(.1f, .2f, .3f, 1f);

	private float alpha = 1f;
	private Color currentColor;
//	private Color currentColorBeam;

	//public SoundInformation CrumblingSoundEffect;

    //private GameObject triggerChild;
	private GameObject meshColliderChild;
//	private GameObject beamChild;
//	private GameObject beamChild1;
//	private GameObject beamChild2;
	
	// Use this for initialization
	void Start () {
        //triggerChild = transform.GetChild (0).gameObject; 
		meshColliderChild = transform.GetChild (1).gameObject;
	//	beamChild = transform.GetChild (2).gameObject;
	//	beamChild1 = transform.GetChild (2).GetChild(0).gameObject;
	//	beamChild2 = transform.GetChild (2).GetChild(1).gameObject;
		currentColor = new Color (color.r,color.g,color.b, 1f);
	//	currentColorBeam = new Color (color.r,color.g,color.b, 1f);


		// adjust to real width,height, length

	//	triggerChild.transform.Translate (0, size.y-1.25f, 0);
	//	meshColliderChild.transform.Translate (0, size.y-1.25f, 0);
	//	triggerChild.transform.localScale = new Vector3(size.x, 1f, size.z);
	//	meshColliderChild.transform.localScale = new Vector3(size.x, 1f, size.z);

	//	beamChild.transform.localScale = new Vector3(size.x, 2f*size.y - 1.5f, size.z);

		// .5 tall,   desired = .25 
		// h=1 .25	.5
		// h=2 1.25	 2.5
		// h=3 2.25  4.5
	}

	// Update is called once per frame
	void Update () {
		if (!faded) {
			if (fading) {
				alpha = Mathf.Lerp (alpha, 0f, 1f/liveTime * Time.deltaTime);
			} else {
				if(regenerates)
					alpha = Mathf.Lerp (alpha, 1f, 1f/liveTime * Time.deltaTime);
			}
			if (alpha <= .3f) {
				faded = true;
				alpha = 0f;
				meshColliderChild.collider.enabled = false;
				if(respawns)
					StartCoroutine (respawn());
			}
			currentColor = new Color (color.r,color.g,color.b, alpha);
			meshColliderChild.renderer.material.SetColor("_Color",currentColor);
		//	beamChild1.renderer.material.SetColor("_Color",currentColor);
		//	beamChild2.renderer.material.SetColor("_Color",currentColor);
			meshColliderChild.transform.localScale = new Vector3(1f, alpha, 1f);
		} else {
			//is faded....do anything?
		}
	}

	private IEnumerator  respawn() {
		yield return new WaitForSeconds(respawnDelay);	

		while (thingsInBounds > 0) {
			yield return new WaitForSeconds(1f);
		}
		meshColliderChild.collider.enabled = true;
		alpha =.4f;
		faded = false;
		currentColor = new Color (color.r,color.g,color.b, alpha);
	//	meshColliderChild.renderer.material.SetColor("_Alpha",alpha);
		meshColliderChild.renderer.material.SetColor("_Color",currentColor);
//		beamChild1.renderer.material.SetColor("_Color",currentColor);
//		beamChild2.renderer.material.SetColor("_Color",currentColor);
		meshColliderChild.transform.localScale = new Vector3(1f, alpha, 1f);
	}
	
	public override void OnTriggerEnter(Collider other) {
		if ( ( other.GetComponent<PlayerBase>() && (triggeredBy == triggeringActors.Player || triggeredBy == triggeringActors.Both) ) || ( other.GetComponent<MonsterBase>() && (triggeredBy == triggeringActors.Enemy || triggeredBy == triggeringActors.Both) ) ) {
			thingsInBounds++;
			//triggeringEntity = other.gameObject;
		}
		if (thingsInBounds == 1) {
			if(fadeable) fading= true;
		}

	}
	
	public override void OnTriggerExit(Collider other) {
		//Debug.Log("Triggered not anymore");
		if ( ( other.GetComponent<PlayerBase>() && (triggeredBy == triggeringActors.Player || triggeredBy == triggeringActors.Both) ) || ( other.GetComponent<MonsterBase>() && (triggeredBy == triggeringActors.Enemy || triggeredBy == triggeringActors.Both) ) ) {
			thingsInBounds--;
		}
		if (thingsInBounds <= 0) {
			thingsInBounds = 0;
			if(fadeable) fading= false;
		}
	
	}

	 
//	void OnDrawGizmosSelected() {
//		Gizmos.color = Color.yellow;
//		Gizmos.DrawWireCube(transform.position+  new Vector3(0, size.y/2, 0), new Vector3( size.x,size.y, size.z));
//	}
	
	
}
