using UnityEngine;
using System.Collections;

public class VictoryScreenStat : MonoBehaviour {

	public UILabel textLabel;
	public UILabel valueLabel;
	private float value;
	public UILabel multipliedScoreLabel;
	public float calculatedScore { get; private set; }


	// Use this for initialization
	void Start () {
		textLabel = gameObject.GetComponent<UILabel> ();
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
	
	public void SetValue(float value)
	{
		this.value = value;
	}
	
	public void SetValueText(string s)
	{
		if (valueLabel != null)
		{
			valueLabel.text = s;
		}
	}

	public void SetMultiplier(float value)
	{
		calculatedScore = this.value * value;
		string s = calculatedScore.ToString();
		if (multipliedScoreLabel != null)
		{
			multipliedScoreLabel.text = s;
		}
	}
}
