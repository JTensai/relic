using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdvancedFallingGroundCoordinator : MonoBehaviour {
	public int TileWidth = 2;
	public int TileHeight = 2;


	public AdvancedFallingGround FallingGround;

	private AdvancedFallingGround[][] _fallingGroundGrid;
	private Dictionary<AdvancedFallingGround, KeyValuePair<int, int>> _positionMap;
	// Use this for initialization
	void Start () {
		_positionMap = new Dictionary<AdvancedFallingGround, KeyValuePair<int, int>>();

		_fallingGroundGrid = new AdvancedFallingGround[TileWidth][];
		for(int i = 0; i < TileWidth; i++){
			_fallingGroundGrid[i] = new AdvancedFallingGround[TileHeight];
		}
		int iCount = 0;
		for(int i = 0; i < TileHeight; i++){
			for(int j = 0; j < TileWidth; j++){
				AdvancedFallingGround toAdd = ((AdvancedFallingGround)GameObject.Instantiate(FallingGround, new Vector3(j, 0, -i), Quaternion.identity));
				_fallingGroundGrid[i][j] = toAdd.GetComponent<AdvancedFallingGround>();
				toAdd.name += iCount;
				iCount++;
				toAdd.gameObject.transform.parent = gameObject.transform;
				_positionMap.Add (toAdd, new KeyValuePair<int, int>(i, j));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PieceCollapsed(AdvancedFallingGround Ground){
//		KeyValuePair<int, int> pos = _positionMap[Ground];


	}

	public bool CheckValid(int x, int y){
		return x >= 0 && x < TileHeight && y >= 0 && y < TileWidth;
	}
}
