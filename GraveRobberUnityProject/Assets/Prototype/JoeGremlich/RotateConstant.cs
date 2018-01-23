using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RotateConstant : MonoBehaviour {
	public float _speed;
	public enum Axis { 
		X,Y,Z
	}
	public Axis axis;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if(axis == Axis.X){
			this.transform.Rotate(Vector3.left, _speed * Time.deltaTime);
			//float oldX = gameObject.transform.rotation.x;
			//gameObject.transform.Rotate(new Vector3 (oldX + _speed, 0, 0));
		}
		else if(axis == Axis.Y){
			this.transform.Rotate(Vector3.up, _speed * Time.deltaTime);
			//float oldY = gameObject.transform.rotation.y;
			//gameObject.transform.Rotate(new Vector3(0, oldY + _speed, 0));
		}
		else if(axis == Axis.Z){
			this.transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
			//float oldZ = gameObject.transform.rotation.z;
			//gameObject.transform.Rotate(new Vector3 (0, 0, oldZ + _speed));
		}


	}
}
