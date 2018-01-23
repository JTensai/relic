using UnityEngine;
using System.Collections;

public class UpdateScores : MonoBehaviour {


	public HSController hscontroller;

	void OnTriggerEnter(Collider other) {
		hscontroller.GetScore();
	}
}
