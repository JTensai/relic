using UnityEngine;
using System.Collections;

public class AdvancedFallingGround : MonoBehaviour {
	public GameObject NW;
	public GameObject N;
	public GameObject NE;
	public GameObject SW;
	public GameObject S;
	public GameObject SE;
	public GameObject W;
	public GameObject E;

	public AdvancedFallingGroundCoordinator Coordinator;

	private AdjacentState _nwState;
	private AdjacentState _nState;
	private AdjacentState _neState;
	private AdjacentState _swState;
	private AdjacentState _sState;
	private AdjacentState _seState;
	private AdjacentState _wState;
	private AdjacentState _eState;

	public enum AdjacentState {StillThere, Collapsed, Solid};
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void SetToPartialFall(GameObject obj){

	}

	private void SetToFullFall(GameObject obj){

	}
}
