using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WaterController : MonoBehaviour {


	private bool paused;
	public float speed = 1;
	public bool High; // false = low, true = high;
	public List<GameObject> list;
	public float raiseHeight;
	private InteractableComponent interact;
	public GameObject handle;
	private float nearTop;

	public GameObject player;
	public float cameraDelay;
	private float timer;


	private Dictionary<GameObject, float[]> map;

	// Use this for initialization
	void Start () {

		interact = GetComponent<InteractableComponent> ();
		interact.OnNotify +=handleOnNotify;
		interact.OnInteract += HandleOnInteract;
		map = new Dictionary<GameObject,float[] > ();
		High = !High;
		foreach (GameObject obj in  list) {
		if(obj.GetComponentInChildren<FloatingPlatform>() != null)
			{
				obj.GetComponentInChildren<FloatingPlatform>().controls = this;
			}
		}

	}
	

	
	public void handleOnNotify(InteractableNotifyEventData data){
	}
	


	public void HandleOnInteract (InteractableInteractEventData data)
	{

		if (!paused) {
		
			High = !High;
			foreach (GameObject obj in  list) {if(obj != null){

				Vector3 top;
				Vector3 bottom;
				
				Vector3 temp = obj.transform.position;
				if (High) {
					top = obj.transform.position;
					
					temp.y -=(raiseHeight );
					bottom = temp;
				} else {
					bottom = obj.transform.position;
					
					temp.y +=(raiseHeight );
					top = temp;
				}
				float[] limits = new float[2];
				
				limits[0] = top.y;
				limits[1] =bottom.y;
				map.Remove(obj);
				map.Add (obj,limits);
				
			}
			}
		

			pauseTransports ();


		}
	}



	public void pauseTransports()
	{
		paused = true;
	
	}

	// Update is called once per frame
	void Update () {

		if(paused)
		{
			timer += Time.deltaTime;
			if(handle != null){
			if(!High)
				{handle.transform.Rotate(new Vector3(.25f,0,0));}
			else
			{handle.transform.Rotate(new Vector3(-.25f,0,0));}

			}


			if(timer > cameraDelay)
			{
			
				if(player !=null)
					{
					Vector3 bobbleVector = player.transform.position;
					bobbleVector.y +=.03f;
					player.transform.position = bobbleVector;
				}
			
			foreach (KeyValuePair<GameObject, float[]> pair in map)
				{
				float[] temp = pair.Value;
				Vector3 bobbleVector = pair.Key.transform.position;

				if (!High && bobbleVector.y < temp[0]) {

					bobbleVector.y += speed*0.005f * (temp[0] - bobbleVector.y ) +.008f;
					pair.Key.transform.position = bobbleVector;
				} 
				else if(High && bobbleVector.y > temp[1])
					{	
						bobbleVector.y -= speed*0.005f *(bobbleVector.y - temp[1]) +.008f;
					pair.Key.transform.position = bobbleVector;}
				else
					{
						paused = false;
						timer = 0;
						player = null;
						
					}
						
			
			
			}

		
		}
	}}
}