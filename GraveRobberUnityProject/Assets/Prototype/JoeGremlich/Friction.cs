using UnityEngine;
using System.Collections;

public class Friction : MonoBehaviour {
	private MovementComponent _playerMove;
	private VisionCube _area;

	public float frictionAmount = 1.0f;

	// Use this for initialization
	void Start () {
		_area = this.GetComponent<VisionCube> ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] players = _area.PlayersInVision ();
		foreach (GameObject p in players) {
			MovementComponent move = p.GetComponent<MovementComponent>();
			move.Move(0, p.GetComponent<Transform>().forward * frictionAmount * Time.deltaTime);
		}
	}
}
