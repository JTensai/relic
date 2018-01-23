using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#
using System;

public class RumbleManager : MonoBehaviour{
    //bool playerIndexSet = false;
//	PlayerIndex playerIndex;
//	GamePadState state;
//	GamePadState prevState;
	
	static RumbleManager mInstance;
	
	public enum RumbleTypes {Solid, Decrease, Increase};
	
	public static RumbleManager Instance
	{
		get{
			if(mInstance == null){
				mInstance = (new GameObject("RumbleManager")).AddComponent<RumbleManager>();
			}
			return mInstance;
		}
	}

	public void StartRumble(float magnitude, float duration){
//		setState();
//		StartCoroutine(RumbleForDuration(AnimationCurve.Linear(0, 1, 1, 1), AnimationCurve.Linear(0, 1, 1, 1), magnitude, magnitude, duration, duration));
	}

	public void StartRumble(AnimationCurve curve, float magnitude, float duration){
//		setState();
//		StartCoroutine(RumbleForDuration(curve, curve, magnitude, magnitude, duration, duration));
	}

	public void StartRumble(AnimationCurve leftRumble, AnimationCurve rightRumble, 
	                        float leftMagnitude, float rightMagnitude, float leftDuration, float rightDuration){
//		setState();
//		StartCoroutine(RumbleForDuration(leftRumble, rightRumble, leftMagnitude, rightMagnitude, leftDuration, rightDuration));
	}
		
	private IEnumerator RumbleForDuration(AnimationCurve leftRumble, AnimationCurve rightRumble, 
	                                      float leftMagnitude, float rightMagnitude, float leftDuration, float rightDuration){

//			float elapsedTime = 0f;
//			
//			while(elapsedTime < Mathf.Max(leftDuration, rightDuration)){
//				float leftPercentage = elapsedTime / leftDuration;
//				float rightPercentage = elapsedTime / rightDuration;
//				
//				float leftAmount = leftRumble.Evaluate(leftPercentage) * leftMagnitude;
//				float rightAmount = rightRumble.Evaluate(rightPercentage) * rightMagnitude;
//			try{
//				GamePad.SetVibration(playerIndex, leftAmount, rightAmount);
//			}
//			catch (Exception){
//				Debug.Log ("You forgot the Rumble dll!");
//			}
				yield return null;
//				elapsedTime += Time.deltaTime;
//			}
//				try{
//				GamePad.SetVibration(playerIndex,0,0);
//			}
//			catch (Exception){
//				Debug.Log ("You forgot the Rumble dll!");
//			}
		


	}
	

	public void StopRumble(){
//		GamePad.SetVibration(playerIndex, 0f, 0f);
	}
	
	public void OnDestroy(){
//		StopRumble();
	}
	
	private void setState(){
//		// Find a PlayerIndex, for a single player game
//		// Will find the first controller that is connected ans use it
//		if (!playerIndexSet || !prevState.IsConnected)
//		{
//			for (int i = 0; i < 4; ++i)
//			{
//				PlayerIndex testPlayerIndex = (PlayerIndex)i;
//				GamePadState testState = GamePad.GetState(testPlayerIndex);
//				if (testState.IsConnected)
//				{
//					Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
//					playerIndex = testPlayerIndex;
//					playerIndexSet = true;
//				}
//			}
//		}
//		
//		prevState = state;
//		state = GamePad.GetState(playerIndex);
	}

	[System.Serializable]
	public class RumbleInfo{
		public AnimationCurve LeftRumbleCurve = AnimationCurve.Linear(0, 1, 1, 1);
		public AnimationCurve RightRumbleCurve = AnimationCurve.Linear(0, 1, 1, 1);
		public float LeftMagnitude = 1f;
		public float RightMagnitude = 1f;
		public float LeftDuration = 1f;
		public float RightDuration = 1f;

		public RumbleInfo(){
			LeftRumbleCurve = AnimationCurve.Linear(0, 1, 1, 1);
			RightRumbleCurve = AnimationCurve.Linear(0, 1, 1, 1);
			LeftMagnitude = 1f;
			RightMagnitude = 1f;
			LeftDuration = 1f;
			RightDuration = 1f;
		}

		public void PlayRumble(){
			RumbleManager.Instance.StartRumble(LeftRumbleCurve, RightRumbleCurve, LeftMagnitude, RightMagnitude, LeftDuration, RightDuration);
		}
	}

	[System.Serializable]
	public class RumbleEventHandler{
		public RumbleInfo[] RumbleEvents;

		public void PlayRumbles(){
			foreach(RumbleInfo i in RumbleEvents){
				i.PlayRumble();
			}
		}
	}
	
}
