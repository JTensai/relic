using UnityEngine;
using System.Collections;

public class StatDataContainer{

	public float value {get; set;}
	public float scoreValue {get; set;}
	public string displayText { get; set; }

	public StatDataContainer(float value, float scoreValue, string displayText = ""){
		this.value = value;
		this.scoreValue = scoreValue;
		this.displayText = displayText;
	}
}
