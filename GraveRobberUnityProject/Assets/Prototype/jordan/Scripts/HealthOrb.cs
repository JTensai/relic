using UnityEngine;
using System.Collections;

public class HealthOrb : MonoBehaviour {

	public int healAmount = 3;
	public float ActivateDelay = 0.3f;
	public bool IncreaseMaxHealth = false;
	public int IncreaseMaxHealthValue = 3;
	private float Step = 1f;
	private float Offset = 0f;
	private float InitialPosition;

	void Start() {
		Offset = transform.position.y + transform.localScale.y;
		Offset += 0.5f;
	}

	void FixedUpdate() {

	}

	void Update() {
		Step += 0.03f;
		if (Step >= 999) {
			Step = 1;
		}
		Vector3 temp = new Vector3 (transform.position.x, (Mathf.Sin (Step) / 4) + Offset, transform.position.z);
		this.transform.position = temp;

		// make it float up and down
		if (ActivateDelay > 0) {
			ActivateDelay -= Time.deltaTime;
			// Debug.Log ("Delay remaining: " + ActivateDelay);		
		}
	}

	void OnTriggerStay(Collider other){
		if (other.CompareTag("Player")) {
			if (ActivateDelay <= 0) {	
				if (IncreaseMaxHealth) {
					other.GetComponent<HealthComponent>().IncreaseMaxHealth(IncreaseMaxHealthValue);
				}	
				else {
					// Debug.Log("Health Orb obtained!");
					other.GetComponent<HealthComponent> ().Heal (healAmount, true);
				}
				Destroy (this.gameObject);
			}
		}
	}
}