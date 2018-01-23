using UnityEngine;
using System.Collections;

public class EditorUtils {

	private static bool _isPro;
	private static bool _isProCalculated;
	public static bool IsPro{
		get{
			if(!_isProCalculated){
				_isProCalculated = true;
				_isPro = true;
#if UNITY_EDITOR
				_isPro = UnityEditorInternal.InternalEditorUtility.HasPro();
#endif
			}
			return _isPro;
		}
	}
}
