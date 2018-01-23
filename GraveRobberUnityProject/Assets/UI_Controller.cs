using UnityEngine;
using System.Collections;

public class UI_Controller : MonoBehaviour
{

//		private bool menuUp = false;
//		private bool menuBringingUp = false;
//		private bool interactable = true;
//		private bool canceled = false;
//		private UIPanel mainmenu;


		// Use this for initialization
//		void Start ()
//		{
//				if (GameObject.Find ("MainMenu") != null) {
//						mainmenu = GameObject.Find ("MainMenu").GetComponent<UIPanel> ();
//				}
//		}

		// Update is called once per frame
//		void Update ()
//		{
		// The GameObject called "GameHUD" is going to handle the pause screen :) not a seperate scene
//			if(Input.GetKey(KeyCode.Escape))
//			{
//				Application.LoadLevel("MainMenuScene");
//			}

				// if you can push it.
			/*	if (Input.GetKey (KeyCode.Escape) && interactable) {

						StartCoroutine (enableDelay ());
						//Application.LoadLevel("MainMenuScene");
						SparkController temp = GameObject.FindGameObjectWithTag ("Spark").GetComponent<SparkController> ();

						//
						if (menuUp) {
								temp.getStateMachine ().SetNextState ("return");
								mainmenu.alpha = 0f;
								menuUp = false;
								canceled = false;
								menuBringingUp = false;
						} else { // menu not up
								if (menuBringingUp) { // if it is currently bringing up....
										canceled = true;
										menuBringingUp = false;
										temp.getStateMachine ().SetNextState ("return");
										StopCoroutine ("alphaDelay");
								} else {
										canceled = false;
										temp.getStateMachine ().SetNextState ("menu");
										menuBringingUp = true;
										//StartCoroutine (alphaDelay (1f));
										StartCoroutine ("alphaDelay", 1f);
								}
						}
}
		 */


						/*		if(!menuUp){
				temp.getStateMachine().SetNextState("menu");
				menuBringingUp = true;
			//	menuUp = true;
				//a.alpha =1f;
				StartCoroutine (alphaDelay (1f));
				
			}else {
				if(!menuUp){
					canceled = true;
				}
				temp.getStateMachine().SetNextState("return");
				menuUp = false;
				a.alpha =0f;
			}*/

				
//		}

		// Delay esc key 
//		private IEnumerator enableDelay ()
//		{
////				interactable = false;
//				yield return new WaitForSeconds (.3f);	
////				interactable = true;
//		
//		}
//
//		private IEnumerator alphaDelay (float alpha)
//		{
//				yield return new WaitForSeconds (1f);	
//
//				if (!canceled) {
//						menuUp = true;
//						mainmenu.alpha = alpha;
//				}
//		}

}
