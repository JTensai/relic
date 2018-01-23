using UnityEngine;
using System.Collections;

public class shopStat : MonoBehaviour {
	
	public UILabel textLabel;
	private UILabel valueLabel;


	
	
	// Use this for initialization
	void Start () {

		valueLabel = transform.FindChild ("Value").GetComponent<UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetText(string s)
	{
		if (textLabel != null)
		{
			textLabel.text = s;
		}
	}
	

	
	public void SetValueText(string s)
	{
		if (valueLabel != null)
		{
			valueLabel.text = s;
		}
	}

}
