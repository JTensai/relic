using UnityEngine;
using System.Collections;

public class Deathbox : MonoBehaviour 
{
	[ReadOnlyAttribute]
	public float distance = 0;
	[ReadOnlyAttribute]
	public float speed;
	public float startSpeed = 1f;
	public float fullSpeed = 3f;
	public float timeToFullSpeed = 10f;
	private float timeRemainingToFullSpeed;
	public float maxCatchUpSpeed = 5;
	public float catchUpDistance = 9f;
	public float absoluteMinSpeed = 1f;
	private bool activated;
	private AttackBase _attack;
//	private VisionBase _collisionVisionCube;
	private Vector3 init_pos;
	private GameObject playerReference;
//	public bool alwaysVisible = false;

	// Use this for initialization
	void Start () 
	{
		_attack = this.GetComponent<AttackBase>();
//		_collisionVisionCube = this.GetComponent<VisionCube>();
		init_pos = this.transform.position;
		activated = false;
		timeRemainingToFullSpeed = timeToFullSpeed;
		speed = startSpeed;
		playerReference = GameObject.FindObjectOfType<PlayerBase>().gameObject;
//		this.gameObject.SetActive (alwaysVisible);
		this.GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (activated)
		{
			if(playerReference != null){			
				distance = this.transform.position.z - playerReference.transform.position.z;
			}

//			// over x seconds change speed from startspeed to fullspeed
			if (timeRemainingToFullSpeed > 0)
			{
				timeRemainingToFullSpeed -= Time.deltaTime;
				if (speed < fullSpeed)
				{
					speed = startSpeed + (1 - (timeRemainingToFullSpeed/timeToFullSpeed))*(fullSpeed - startSpeed);
				}
			}
			else{
				// if player is more than x units away, increase speed on a curve
				if (distance > catchUpDistance)
				{
					if (speed < maxCatchUpSpeed)
					{					
						speed += (distance)/100f;
					}
//						Debug.Log ("Player is far away from wall of death! --> " + Vector3.Distance (this.transform.position, GameObject.FindObjectOfType<PlayerBase>().gameObject.transform.position));
				}
				else if (speed > fullSpeed){//if (Vector3.Distance (this.transform.position, playerReference.transform.position) < slowDownDistance){
					speed -= (distance)/100f;
				}
			}//

			if (speed < absoluteMinSpeed){
				speed = absoluteMinSpeed;
			}


			this.transform.Translate(new Vector3(0,0,Time.deltaTime*speed*(-1)));
//		    foreach (GameObject m in _collisionVisionCube.MonstersInVision()) 
//			{
//				_attack.Attack (m.transform);
//			}
//			foreach (GameObject p in _collisionVisionCube.PlayersInVision()) 
//			{
//				Debug.Log("Player is in the Death Wall");
//				_attack.Attack (p.transform);
//			}
		}
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.layer == LayerMask.NameToLayer("Player")){
			_attack.Attack (c.transform);
		}
	}

	void OnTriggerStay(Collider c){
		if (c.gameObject.layer == LayerMask.NameToLayer("Player")){
			_attack.Attack (c.transform);
		}
	}

	public void Activate(){
		this.gameObject.SetActive(true);

		activated = true;
	}

	public void Reset(){
		fullSpeed -= .4f;
		Debug.Log (fullSpeed);
		this.transform.position = init_pos;
		speed = startSpeed;
		timeRemainingToFullSpeed = timeToFullSpeed;
	}
}
