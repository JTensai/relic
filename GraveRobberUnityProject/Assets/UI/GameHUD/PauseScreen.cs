using UnityEngine;
using System.Collections;

public class PauseScreen : MonoBehaviour {

	private UIWidget widget = null;
	public GameObject defaultButton;

	// Use this for initialization
	void Start () {
		widget = GetComponent<UIWidget> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (widget.alpha > 0) {
			if (UICamera.selectedObject == null) {
				UICamera.selectedObject = defaultButton;
				defaultButton.GetComponent<UIButton>().SendMessage("OnHover", true);
			}
		}
	}
}
