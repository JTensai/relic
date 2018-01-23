using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	
	public GameObject rollingCredits;
	public UIPanel hiddenButton;
	public UIPanel originalMenuButton;
	public GameObject firstDefaultButton;
	public GameObject secondDefaultButton;
	private bool finished = false;
	public float RollStop = 4600;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		rollingCredits.transform.localPosition = new Vector3 (0, -4400, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (finished){
			if (originalMenuButton.alpha > 0){
				originalMenuButton.alpha -= Time.deltaTime*4;
			}
			if (hiddenButton.alpha < 1){
				hiddenButton.alpha += Time.deltaTime;
			}
			if (UICamera.selectedObject == null) {
				UICamera.selectedObject = secondDefaultButton;
				secondDefaultButton.GetComponent<UIButton>().SendMessage("OnHover", true);
			}
		}
		else{
			if (rollingCredits.transform.localPosition.y < RollStop){
				Debug.Log (rollingCredits.transform.localPosition.y);
				rollingCredits.transform.Translate(0, 0.007f, 0);			
				if (UICamera.selectedObject == null) {
					UICamera.selectedObject = firstDefaultButton;
					firstDefaultButton.GetComponent<UIButton>().SendMessage("OnHover", true);
				}
			}
			else{			
				finished = true;
				UICamera.selectedObject = null;
				Destroy (firstDefaultButton);
			}
		}
	}
}
