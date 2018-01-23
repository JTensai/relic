using UnityEngine;
using System.Collections;

public class SetColor : MonoBehaviour 
{
	public GameObject pillarRoot;

	// Use this for initialization
	void Start () 
	{
		if(pillarRoot.GetComponent<PillarTrigger>().regenerates)
			gameObject.renderer.material.color = new Color(0f, 1f, 0f);
		else
			gameObject.renderer.material.color = new Color(0f, 0.5f, 0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
