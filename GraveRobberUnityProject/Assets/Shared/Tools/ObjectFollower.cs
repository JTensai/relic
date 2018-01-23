using UnityEngine;
using System.Collections;

/// <summary>
/// Note for offsets, the directional offset is applied first (in object space of the followed object), then the rotation is applied in local space to the following object
/// </summary>
public class ObjectFollower : MonoBehaviour {
	public Transform ToFollow;
	public Vector3 Offset = Vector3.zero;
	public Vector3 EulerRotationOffset = Vector3.zero;

	private Quaternion _initialRotation;

	public void Start () {
		//Debug.Log("Start ObjectFollower");
		//Debug.Log ("\tObject: " + this.gameObject);

		//TODO: do we actually want to get rid of this?
		_initialRotation = transform.rotation;
	}

	public void OnDestroy() {
	}

	public void Update () {
		if(ToFollow != null){
			Vector3 localOffset = ToFollow.rotation * Offset;
			this.transform.position = ToFollow.transform.position + localOffset;
			this.transform.rotation = ToFollow.rotation * Quaternion.Euler(EulerRotationOffset) * _initialRotation;
		}
	}
}
