using UnityEngine;
using System.Collections;

public class scrollTextureY : MonoBehaviour {

	public Material material;
	public Vector2 uvAnimationRate = new Vector2( 0.0f, 1.0f ); 
	public string textureName = "_MainTex";
	public Vector2 uvOffset = Vector2.zero;
	
	// Update is called once per frame
	void LateUpdate () {
		uvOffset += ( uvAnimationRate * Time.deltaTime );
		material.SetTextureOffset ("_MainTex", uvOffset);
	}
}
