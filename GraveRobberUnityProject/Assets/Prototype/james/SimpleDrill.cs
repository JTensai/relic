using UnityEngine;
using System.Collections;

public class SimpleDrill : MonoBehaviour {

	public VoxelMap voxelMap;

	public Vector3 direction = Vector3.down;
	public string sideTile = "red";
	public string endTile = "blue";

	public void ActivateDrill()
	{
		Voxel startVoxel = voxelMap.GetVoxel(voxelMap.transform.InverseTransformPoint(transform.position));

		if (startVoxel != null)
		{
			voxelMap.DrillHole(startVoxel, direction, 1, sideTile, endTile);
		}

		Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{
		ActivateDrill();
	}
}
