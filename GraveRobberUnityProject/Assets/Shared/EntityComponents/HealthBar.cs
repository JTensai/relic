using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	//[SerializeField]
//	private int _top;
//
//	//[SerializeField]
//    //private int _left = 30;
//
//	//[SerializeField]
//	//private int _imageRepeat = 3;
//
//	//[SerializeField]
//	//private int _subdivisions = 3;
//
//	//private float _healthStep;
//	
//	[SerializeField]
//	private Texture2D _portrait;
//	[SerializeField]
//	private Texture2D _healthMarker;
//	[SerializeField]
//	private Texture2D _healthContainer;
//	private int _portraitOffset = 10;
//	[SerializeField]
//	private HealthComponent _healthComponent;
//	private Rect rectPool;
//
//	public void Start()
//	{	
//		rectPool = new Rect(0, 0, 1, 1);
//	}

//	public void OnGUI()
//	{
		//float healthPercentage = _healthComponent.CurrentHealth / _healthComponent.MaxHealth;
		//healthPercentage = Mathf.Round(healthPercentage / _healthStep) * _healthStep;

		//float segments = healthPercentage * _imageRepeat;

		// int wholeSegments = Mathf.FloorToInt(segments);
		// float percentageSegment = _healthComponent.CurrentHealth - wholeSegments * 3;
		// percentageSegment = percentageSegment / 3;
		// float percentageSegment = segments - wholeSegments;


//		float left = _portraitOffset + _portrait.width*2 + 5;
//		float top = _portraitOffset;
//
//		rectPool.Set(_portraitOffset, _portraitOffset, _portrait.width*2, _portrait.height*2);
//		GUI.DrawTexture(rectPool, _portrait);	
//		
//		for (int i = 0; i < (int)_healthComponent.MaxHealth; ++i)
//		{
//			rectPool.Set(left, top, _healthMarker.width/2, _healthMarker.height/2);
//			if (i < (int)_healthComponent.CurrentHealth){
//				GUI.DrawTexture(rectPool, _healthMarker);
//			}
//			else{
//				GUI.DrawTexture(rectPool, _healthContainer);
//			}
//
//			left += _healthContainer.width/2.3f;
//		}
		
//		for (int i = 0; i < (int)_healthComponent.CurrentHealth; ++i)
//		{
//			GUI.DrawTexture(new Rect(left, top, _healthMarker.width/2, _healthMarker.height/2), _healthMarker);
//			left += _healthMarker.width/2.8f;
//		}


		/*if (percentageSegment != 0.0f)
		{
			int partialWidth = Mathf.FloorToInt(percentageSegment * _healthMarker.width);
			GUI.DrawTextureWithTexCoords(new Rect(left, _top, partialWidth, _healthMarker.height), _healthMarker, new Rect(0.0f, 0.0f, percentageSegment, 1.0f));
		}*/
//	}
}
