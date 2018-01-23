using UnityEngine;
using System.Collections;

public class CountdownTimer : MonoBehaviour {
	
	public float StartTime;
	public float CurTime;
	public GUIStyle TextStyle;
	private bool expired;

	// Use this for initialization
	void Start () {
		expired = false;
		TextStyle = new GUIStyle ();
		TextStyle.fontSize = 50;
		TextStyle.normal.textColor = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		if (!expired)
			CurTime -= Time.deltaTime;

		if (CurTime <= 0) { // Time's up! Trigger that event!
			if (TimerFinished != null)
				TimerFinished.Invoke(); // not sure if this is how you trigger an event. 
			expired = true;
		}
	}
	
//	void OnGUI(){
//		if (!expired) {
//
//			GUI.Label(new Rect(600, 50, 200, 100), formatTime(CurTime), TextStyle);
//		}
//	}

	private string formatTime(float time){ // There's probably an easier way, but I couldn't find it
		string timeString = "";

		int min = (int)time / 60;
		int sec = (int)(time % 60);

		//if (min < 10)
		//	timeString += "0";
		timeString += min + ":";

		if (sec < 10)
			timeString += "0";
		timeString += sec;

		return timeString;
	}

	public event System.Action TimerFinished;

	
	public static CountdownTimer CreateTimer(int StartTimer){
		GameObject retVal = new GameObject ("Timer");
		CountdownTimer timer = retVal.AddComponent<CountdownTimer> ();
		timer.CurTime = StartTimer;
		timer.StartTime = StartTimer;

		return timer;
	}

	public void Destroy(){
		this.Destroy ();
	}
	
}
