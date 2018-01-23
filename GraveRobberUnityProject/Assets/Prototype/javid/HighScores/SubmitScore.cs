using UnityEngine;
using System.Collections;

public class SubmitScore : MonoBehaviour {

	public HSController hscontroller;
	public UILabel label;
	public UILabel score;
	private static bool submitted = false;

	public void Submit(){
		if (!submitted) {
						Debug.Log ("score.text" + score.text);
						Debug.Log ("int.Parse(score.text)" + int.Parse (score.text));
						hscontroller.PostScore (label.text, int.Parse (score.text));
						StartCoroutine ("a");
						//	hscontroller.GetScore ();
			submitted = true;
				}
	}

	IEnumerator a(){
		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(2f));
		hscontroller.GetScore ();
	}
}
