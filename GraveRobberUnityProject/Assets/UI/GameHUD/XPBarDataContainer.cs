using UnityEngine;
using System.Collections;
 
public class XPBarDataContainer{

	public GameObject container {get; set;}
	public GameObject filler {get; set;}
	public UITexture containerTexture { get; private set;}
	public UITexture fillerTexture { get; private set;}

	public XPBarDataContainer(GameObject container, GameObject filler){
		this.container = container;
		this.filler = filler;
		containerTexture = container.GetComponent<UITexture> ();
		fillerTexture = filler.GetComponent<UITexture> ();
	}
}
