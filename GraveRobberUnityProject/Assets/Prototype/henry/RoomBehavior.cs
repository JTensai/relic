using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;

public class RoomBehavior : MonoBehaviour {
	public float HeightPadding = 4f;
	public float RoomCameraFOV = 8f;

	public PlayerBase Player;
	public ArtifactBehavior Artifact;
	public bool IsFinalArtifactRoom;
	//public MonsterBase[] Monsters;
	//public EnvironmentBase[] Environments;
	//public DoorBehavior[] Doors;

	//public AstarPath aStar;

	//public MonsterBase[] ReturnMonsters;
	//public EnvironmentBase[] ReturnEnvironments;


	public HashSet<MonsterBase> Monsters;
	public HashSet<EnvironmentBase> Environments;
	public HashSet<DoorBehavior> Doors;
	public HashSet<MonsterBase> ReturnMonsters;
	public HashSet<EnvironmentBase> ReturnEnvironments;
	public RoomBehavior[] AdjacentRooms = new RoomBehavior[8];

	public bool RoomCleared{get;private set;}
	public bool PlayerInRoom{get;private set;}
	public bool PlayerInInteriorRoom{get;private set;}
	public bool ReturnTrip{get;private set;}

	public enum RoomClearType{Monster, Artifact, AlwaysCleared};
	public RoomClearType ClearType = RoomClearType.Monster;

	private Bounds _roomBounds;
	private Bounds _roomInteriorBounds;

	private bool _roomInitialized = false;
	private bool _visited = false;

	private PlayerBase _playerReference;

//	private GameObject _staticLayer, _dynamicLayer, _bakedLayer;

	// Use this for initialization
	void Start () {
		IsFinalArtifactRoom = false;

		Monsters = new HashSet<MonsterBase> ();
		Environments = new HashSet<EnvironmentBase> ();
		Doors = new HashSet<DoorBehavior> ();
		ReturnMonsters = new HashSet<MonsterBase> ();
		ReturnEnvironments = new HashSet<EnvironmentBase> ();
//		_staticLayer = transform.FindChild("Static").gameObject;
//		_dynamicLayer = transform.FindChild("Dynamic").gameObject;
//		_bakedLayer = transform.FindChild("Baked").gameObject;
	}

	public bool ContainsEntity(string name){
		foreach(MonsterBase m in Monsters){
			if (m != null && m.name.Equals(name)){
				return true;
			}
		}
		foreach(EnvironmentBase m in Environments){
			if(m != null && m.name.Equals(name)){
				return true;
			}
		}
		return false;
	}

	private void initializeRoom(){
		GameObject statics = transform.FindChild("Static").gameObject;
		VoxelMap voxel = GetComponent<VoxelMap>();


		Bounds nestedBounds = new Bounds();
		if(voxel != null){
			nestedBounds = getFullBounds(voxel);
		}
		else{
			nestedBounds = getFullBounds(statics);
		}

		BoxCollider bC = gameObject.AddComponent<BoxCollider>();
		bC.isTrigger = true;
		bC.center = nestedBounds.center + (new Vector3(0, HeightPadding, 0) / 2f);
		bC.size = nestedBounds.size + new Vector3(0, HeightPadding, 0);
		_roomBounds = bC.bounds;
		bC.enabled = false;

		BoxCollider internalBounds = gameObject.AddComponent<BoxCollider>();
		internalBounds.isTrigger = true;
		internalBounds.center = nestedBounds.center + (new Vector3(0, HeightPadding, 0) / 2f);
		internalBounds.size = nestedBounds.size + new Vector3(-2, HeightPadding, -2);
		_roomInteriorBounds = internalBounds.bounds;
		internalBounds.enabled = false;

		discoverReturnTripElements();

		//aStar = new AstarPath ();
	}

	private void initializeRoomContents(){
		Transform[] _allObjectsToCheck = GetComponentsInChildren<Transform>(true);
		Player = null;
		//Monsters = null;
		//Environments = null;

		Monsters.Clear ();
		Environments.Clear ();
		Doors.Clear ();
		
		//List<MonsterBase> monsterList = new List<MonsterBase>();
	//	List<EnvironmentBase> environmentList = new List<EnvironmentBase>();
	//	List<DoorBehavior> doorList = new List<DoorBehavior>();
		
		foreach(Transform c in _allObjectsToCheck){
			if(c != null && _roomBounds.Contains(c.transform.position)){
				ArtifactBehavior arti = c.gameObject.GetComponent<ArtifactBehavior>();
				MonsterBase monster = c.gameObject.GetComponent<MonsterBase>();
				EnvironmentBase environment = c.gameObject.GetComponent<EnvironmentBase>();
				DoorBehavior door = c.gameObject.GetComponent<DoorBehavior>();

				if(arti != null){
					Artifact = arti;
					IsFinalArtifactRoom = Artifact.IsFinalArtifact;
				}
				if(monster != null && !Monsters.Contains(monster)){
					Monsters.Add (monster);
				}
				if(environment != null && !Environments.Contains(environment)){
					Environments.Add (environment);
				}
				if(door != null && !Doors.Contains(door)){
					Doors.Add (door);
				}
			}
		}
		
	//	Monsters = monsterList.ToArray();
	//	Environments = environmentList.ToArray();
	//	Doors = doorList.ToArray();

		_playerReference = GameObject.FindObjectOfType<PlayerBase>();

		foreach(DoorBehavior door in Doors){
			door.OpenDoors();
		}		
		foreach(MonsterBase m in ReturnMonsters){
			m.gameObject.SetActive(false);
		}
		foreach(EnvironmentBase e in ReturnEnvironments){
			e.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!_roomInitialized){
			initializeRoom();
			initializeRoomContents();
			_roomInitialized = true;
		}

		updateRoomContents();

//		float distance = Vector3.Distance(transform.position, _playerReference.transform.position);
//		if(distance > Math.Sqrt(Math.Pow(_roomBounds.size.x, 2) + Math.Pow(_roomBounds.size.z, 2))){
//			_staticLayer.gameObject.SetActive(false);
//			_dynamicLayer.gameObject.SetActive(false);
//			_bakedLayer.gameObject.SetActive(false);
//		}
//		else{
//			_staticLayer.gameObject.SetActive(true);
//			_dynamicLayer.gameObject.SetActive(true);
//			_bakedLayer.gameObject.SetActive(true);
//		}

		if(!PlayerInRoom && Player != null && _roomInteriorBounds.Contains(Player.transform.position)){
			playerEntered(true);
		//	Debug.Log (this + " ENTERED");

			CameraManagerScript cms = GameObject.Find("CameraManager").GetComponent<CameraManagerScript>();

			cms.getCurrentCamera();
//			cms.setTargetFOV(RoomCameraFOV);

		}
		if(PlayerInRoom && Player == null){
			playerExited();
		//	Debug.Log (this + " EXITED");
		}

		if(!RoomCleared){
			switch(ClearType){
			case RoomClearType.Monster:
				if(Monsters.Count == 0){
					roomCleared();
					//Debug.Log (this + " CLEARED");
				}
				break;
			case RoomClearType.AlwaysCleared:
				roomCleared();
				//Debug.Log (this + " CLEARED");
				break;
			case RoomClearType.Artifact:
				if(Artifact == null){
					roomCleared();
					//Debug.Log (this + " CLEARED");
					if(IsFinalArtifactRoom){
						if(ReturnTrip == false){
							LevelBehavior.Instance.ArtifactGrabbed();
						}

					}
				}
				break;
			}
		}


		if(Input.GetKeyDown(KeyCode.Alpha0) && Player != null){
			foreach(MonsterBase m in Monsters){
				HealthComponent h = m.GetComponent<HealthComponent>();
				if(h != null){
					h.Kill(AttackEnum.Default);
				}
			}
		}
	}

	public bool PlayerIsInRoom(){
		return PlayerInRoom;
	}

	private void playerEntered(bool playerInRoom){
		PlayerInRoom = playerInRoom;
		if(!RoomCleared){
			foreach(DoorBehavior d in Doors){
				d.CloseDoors();
			}
		}

		//Play entry dialogue on the first time, if it exists
		if (!_visited) {
			RoomEntryDialogue entryDialogue = this.GetComponent<RoomEntryDialogue>();
			if (entryDialogue != null) {
				entryDialogue.Play();
			}
		}

		//switching the astar to this room bounds
		//int min = _roomBounds.min.x -_roomBounds.min.x;
		//AstarPath.active.astarData.gridGraph.bounds.SetMinMax (_roomBounds.min, _roomBounds.max);

		/*if (AstarPath.active != null)
		{
			AstarPath.active.astarData.gridGraph.center = _roomBounds.center;// (or whatever)
			AstarPath.active.astarData.gridGraph.center.y = 0;
			AstarPath.active.astarData.gridGraph.width = (int)((_roomBounds.max.x -_roomBounds.min.x)*1.5);
			AstarPath.active.astarData.gridGraph.depth = (int)((_roomBounds.max.z -_roomBounds.min.z)*1.5);
			AstarPath.active.astarData.gridGraph.UpdateSizeFromWidthDepth ();
			//AstarPath.active.astarData.gridGraph.Scan();

			AstarPath.active.Scan ();
		}*/
		//Debug.Log ("Room " + name);
		if (PlayerInRoom) {
		
			activate ();
			foreach (RoomBehavior rb in AdjacentRooms) {
				//rb.playerEntered (false);
                if (rb != null)
				{Debug.Log("Activated");
                    rb.activate();
                }
			}
		}
		foreach(MonsterBase m in Monsters){
			m.ActivateEntity();
		}
		/*foreach(EnvironmentBase e in Environments){
			e.ActivateEntity();
		}

		if(PlayerInRoom && OnRoomEnter != null){
			OnRoomEnter();
		}
		if(PlayerInRoom)
			_visited = true;*/
	}
	private void activate(){
		foreach(MonsterBase m in Monsters){
			m.ActivateEntity();
		}
		foreach(EnvironmentBase e in Environments){
			e.ActivateEntity();
		}
		
		if(PlayerInRoom && OnRoomEnter != null){
			OnRoomEnter();
		}
		if(PlayerInRoom)
			_visited = true;
	}

	private void playerExited(){
		PlayerInRoom = false;
		bool deactivateThis = true;
		foreach (RoomBehavior rb in AdjacentRooms) {
            if (rb != null)
            {
                bool deactivateChild = true;
                if (!rb.PlayerIsInRoom())
                {
                    foreach (RoomBehavior rb2 in rb.AdjacentRooms)
                    {
                        if (rb2 != null && rb2.PlayerIsInRoom())
                            deactivateChild = false;
                    }
                    if (deactivateChild)
                        rb.deactivate();
                }
                else
                    deactivateThis = false;
            }
		}
		if (deactivateThis)
			deactivate ();
		/*foreach(MonsterBase m in Monsters){
			m.DeactivateEntity();
		}
		foreach(EnvironmentBase e in Environments){
			e.DeactivateEntity();
		}

		if(OnRoomExit != null){
			OnRoomExit();
		}*/
	}
	private void deactivate(){
		foreach(MonsterBase m in Monsters){
			//m.DeactivateEntity();
		}
		foreach(EnvironmentBase e in Environments){
			e.DeactivateEntity();
		}
		
		if(OnRoomExit != null){
			OnRoomExit();
		}
	}

	private void roomCleared(){
		RoomCleared = true;
		foreach(DoorBehavior d in Doors){
			d.OpenDoors(true);
		}

		if(OnRoomCleared != null){
			OnRoomCleared();
		}
	}

	private void discoverReturnTripElements(){
	//	ReturnMonsters = null;
	//	ReturnEnvironments = null;

		ReturnMonsters.Clear ();
		ReturnEnvironments.Clear ();
		
	//	List<MonsterBase> monsterList = new List<MonsterBase>();
	//	List<EnvironmentBase> environmentList = new List<EnvironmentBase>();
		
		Collider[] colliders = Physics.OverlapSphere(transform.position + _roomBounds.center, Mathf.Max (_roomBounds.size.x, _roomBounds.size.z));
		foreach(Collider c in colliders){
			if(_roomBounds.Contains(c.transform.position)){
				MonsterBase monster = c.gameObject.GetComponent<MonsterBase>();
				EnvironmentBase environment = c.gameObject.GetComponent<EnvironmentBase>();

				if(monster != null && !ReturnMonsters.Contains(monster) && monster.ReturnTripOnly){
					ReturnMonsters.Add (monster);
				}
				if(environment != null && !ReturnEnvironments.Contains(environment) && environment.ReturnTripOnly){
					ReturnEnvironments.Add (environment);
				}
			}
		}
		
	//	ReturnMonsters = monsterList.ToArray();
	//	ReturnEnvironments = environmentList.ToArray();

	}

	private void updateRoomContents(){
	//	List<MonsterBase> monsters = Monsters.ToList();
	//	monsters.RemoveAll(m => m == null);
	//	Monsters = monsters.ToArray();
		Monsters.Remove(null);

	//	List<EnvironmentBase> environments = Environments.ToList();
	//	environments.RemoveAll(e => e == null);
	//	Environments = environments.ToArray();
		Environments.Remove(null);

	//	List<DoorBehavior> doors = Doors.ToList();
	//	doors.RemoveAll(d => d == null);
	//	Doors = doors.ToArray();
		Doors.Remove(null);

		if(_playerReference != null && _roomBounds.Contains(_playerReference.transform.position)){
			Player = _playerReference;
		}
		else{
			Player = null;
		}
	}

	private Bounds getFullBounds(VoxelMap target){
		int w = target.Width;
		int h = target.Height;
		int d = target.Depth;

		Vector3 size = new Vector3(w, h, d);
		Vector3 mid = new Vector3(w/2, h/2, d/2);

		return new Bounds(mid, size);
	}

	private Bounds getFullBounds(GameObject target){
		Renderer[] renderers = target.GetComponentsInChildren<Renderer>();
		List<BoxCollider> bColliders = new List<BoxCollider>();
		foreach(Renderer r in renderers){
			BoxCollider b = r.gameObject.AddComponent<BoxCollider>();
			bColliders.Add (b);
			GameObject.Destroy(b);
		}

		Vector3 min = bColliders[0].center - bColliders[0].size / 2f;
		Vector3 max = bColliders[0].center + bColliders[0].size / 2f;

		for(int i = 1; i < bColliders.Count; i++){
			Vector3 curMin = bColliders[1].center - bColliders[1].size / 2f;
			Vector3 curMax = bColliders[1].center + bColliders[1].size / 2f;

			if(curMax.x > max.x){
				max += new Vector3(curMax.x - max.x, 0, 0);
			}
			else if(curMin.x < min.x){
				min += new Vector3(curMin.x - min.x, 0, 0);
			}
			if(curMax.y > max.y){
				max += new Vector3(0, curMax.y - max.y, 0);
			}
			else if(curMin.y < min.y){
				min += new Vector3(0, curMin.y - min.y, 0);
			}
			if(curMax.z > max.z){
				max += new Vector3(0, 0, curMax.z - max.z);
			}
			else if(curMin.z < min.z){
				min += new Vector3(0, 0, curMin.z - min.z);
			}
		}

		Vector3 size = max - min;
		Vector3 mid = Vector3.Lerp(min, max, 0.5f);

		return new Bounds(mid, size);
	}

	public event Action OnRoomEnter;
	public event Action OnRoomExit;
	public event Action OnRoomCleared;

	public void ActivateReturnTrip(){
		RoomCleared = false;
		ReturnTrip = true;
		if(ReturnMonsters != null){
			foreach(MonsterBase m in ReturnMonsters){
				m.gameObject.SetActive(true);
			}
		}
		if(ReturnEnvironments != null){
			foreach(EnvironmentBase e in ReturnEnvironments){
				e.gameObject.SetActive(true);
			}
		}
	}

	public void DeactivateMonsters(){
		foreach(MonsterBase m in Monsters){
			m.DeactivateEntity();
		}
	}

	public void ActivateMonsters(){
		foreach(MonsterBase m in Monsters){
			m.ActivateEntity();
		}
	}
}
