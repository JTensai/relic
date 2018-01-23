using UnityEngine;
using System.Collections;

public class tutoiral : MonoBehaviour {

	public string instruction;
	private bool activated;
	
	private void Start() {
		activated = false;
	}
	
	public void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")){
			if(activated)
				return;
			else if (other.GetComponent<PlayerBase> () != null) {
				
				this.activated = true;
				GameUI.DisplayInstructionTextArea(instruction, 4f);


			}
		}
	}}