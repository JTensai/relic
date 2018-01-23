using UnityEngine;
using System.Collections;

public class PlayerUtils
{
	public const float DeadZone = 0.15f;
	public enum InputType{Controller, Keyboard}
	public static InputType CurrentInputType{get;private set;}
	private static Vector3 getInputDirectionJoystick()
	{
		//Find out what "Up" and "Right" really mean.
		Vector3 directionOfUp = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
		Vector3 directionOfRight = new Vector3(Camera.main.transform.forward.z, 0, -Camera.main.transform.forward.x).normalized;

		//Figure out where the player is trying to move by multiplying their "up" by up and their "Right" by right.
		Vector3 dir = Input.GetAxis("Vertical")*directionOfUp+Input.GetAxis("Horizontal")*directionOfRight;
		//Debug.Log (dir.ToString() + Input.GetAxis("Horizontal").ToString() + " " + Input.GetAxis("Vertical").ToString());
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		if(new Vector2(x, y).magnitude < DeadZone){
			return Vector3.zero;
		}

		Vector2 screenCenter = new Vector2(Screen.width, Screen.height) / 2f;
		Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x, screenCenter.y, Camera.main.nearClipPlane));
		Vector3 b = Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x + x * 1, screenCenter.y + y * 1, Camera.main.nearClipPlane));
		//a.z = 0;
		//b.z = 0;

		Plane p = new Plane(Vector3.up, new Vector3(0, -5, 0));
		float distA;
		float distB;

		p.Raycast(new Ray(a, Camera.main.transform.forward), out distA);
		p.Raycast(new Ray(b, Camera.main.transform.forward), out distB);

		Vector3 hA = a + Camera.main.transform.forward * distA;
		Vector3 hB = b + Camera.main.transform.forward * distB;

		dir = hB - hA;
		dir.y = 0;
        //Debug.Log (dir.x + " " + dir.y + " " + dir.z + " " + dir.normalized);
		//dir.y = 0;
		return dir.normalized;
	}

	private static Vector3 getInputDirectionKeyboard(){
		//Find out what "Up" and "Right" really mean.
		Vector3 directionOfUp = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
		Vector3 directionOfRight = new Vector3(Camera.main.transform.forward.z, 0, -Camera.main.transform.forward.x).normalized;
		
		//Figure out where the player is trying to move by multiplying their "up" by up and their "Right" by right.
		Vector3 dir = Input.GetAxis("VerticalKeyboard")*directionOfUp+Input.GetAxis("HorizontalKeyboard")*directionOfRight;
		
		return dir.normalized;
	}

	public static Vector3 getInputDirection(){
		if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f){
			//It's joystick!
			CurrentInputType = InputType.Controller;
			Vector3 input = getInputDirectionJoystick();
			return input;
		}
		else if(Input.GetAxis("HorizontalKeyboard") != 0f || Input.GetAxis("VerticalKeyboard") != 0f){
			CurrentInputType = InputType.Keyboard;
			Vector3 input = getInputDirectionKeyboard();
			return input;
		}
		return new Vector3(0, 0, 0);

	}

	public static Vector3 getMoveValues(){
		if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f){
			//It's joystick!
			CurrentInputType = InputType.Controller;
			Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical"));   //getInputDirectionJoystick();
			return input;
		}
		else if(Input.GetAxis("HorizontalKeyboard") != 0f || Input.GetAxis("VerticalKeyboard") != 0f){
			CurrentInputType = InputType.Keyboard;
			Vector3 input = getInputDirectionKeyboard();
			return input;
		}
		return new Vector3(0, 0, 0);
	}

	public static float getMoveMagnitude(){
		return Mathf.Clamp(getInputDirection().magnitude, 0, 1);
	}
}

