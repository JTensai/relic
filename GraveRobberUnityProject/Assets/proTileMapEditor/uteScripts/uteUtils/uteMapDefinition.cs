using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class uteMapTileDefinition
{
	private string prefabGUID;
	private Vector3 position;
	private Vector3 eulerAngles;

	private bool isStatic;
	private string connectionFamily;

	public uteMapTileDefinition(string prefabGUID, Vector3 position, Vector3 eulerAngles, bool isStatic, string connectionFamily)
	{
		this.prefabGUID = prefabGUID;
		this.position = position;
		this.eulerAngles = eulerAngles;
		
		this.isStatic = isStatic;
		this.connectionFamily = connectionFamily;
	}

	public string PrefabGUID { get { return prefabGUID; } }
	public Vector3 Position { get { return position;} }
	public Vector3 EulerAngles { get { return eulerAngles; } }
	
	public bool IsStatic { get { return isStatic; } } 
	public bool IsTileConnected { get { return connectionFamily != null; } }
	public string ConnectionFamily { get { return connectionFamily; } }
}

public class uteMapDefinition
{
	private List<uteMapTileDefinition> tiles = new List<uteMapTileDefinition>();

	public uteMapDefinition()
	{

	}

	public void AddTile(uteMapTileDefinition definition)
	{
		tiles.Add(definition);
	}

	public int TileCount { get { return tiles.Count; } }

	public uteMapTileDefinition GetTile(int index)
	{
		return tiles[index];
	}
}
