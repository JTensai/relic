using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SwampTileScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Transform parentScale = this.transform.parent;
		float tileX = parentScale.localScale.x / 2.0f;
		float tileY = parentScale.localScale.z / 4.0f;

		renderer.sharedMaterial.SetTextureScale ("_Highlight1", new Vector2 (tileX, tileY));
		renderer.sharedMaterial.SetTextureScale ("_Highlight2", new Vector2 (tileX, tileY));
		renderer.sharedMaterial.SetTextureScale ("_Swamp1", new Vector2 (tileX, tileY));
		renderer.sharedMaterial.SetTextureScale ("_Swamp2", new Vector2 (tileX, tileY));

	}
}
