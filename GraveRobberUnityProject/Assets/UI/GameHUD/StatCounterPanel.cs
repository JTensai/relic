using UnityEngine;
using System.Collections;

public class StatCounterPanel : MonoBehaviour {
	public Texture shamblerDeathTexture;
	public Texture chargerDeathTexture;

	public UILabel counterLabel;
	public UITexture statTypeIcon;
	public Animator animator;

	private float incrementAmount;
	private float startingAmount;
	private float lifetime;

	private Vector3 START_POS = new Vector3(1460f, 440f, 0f);
//	private const float END_X = 940f;
	private float END_X = 940f;
	private const float MAX_MOVE_DOWN = -100f;
	private const float MAX_LIFETIME = 3f;

	private float transitionSmoothing = 5f;
	private bool movingDown;
	private float targetY;
	private bool shouldDisappear;

	private float UIWidth;
	private float UIHeight;

	// Use this for initialization
	void Start () {
		//animator.SetFloat("StatCounterDuration", lifetime);

	}

	public void Initialize(float currentStatAmount, float incrementAmount, StatTracker.StatTypes statType, float width, float height, StatTracker stats) {
		UIWidth = width / 2;
		UIHeight = height / 2;
		lifetime = MAX_LIFETIME;
		switch(statType) {
		case StatTracker.StatTypes.ShamblerDeath:
			if (stats != null)
				counterLabel.text = currentStatAmount + " / " + stats.getTotalShamblers();
			statTypeIcon.mainTexture = shamblerDeathTexture;
			break;
		case StatTracker.StatTypes.ChargerDeath:
			counterLabel.text = currentStatAmount + " / " + stats.getTotalChargers();
			statTypeIcon.mainTexture = chargerDeathTexture;
			break;
		case  StatTracker.StatTypes.money:
			Debug.Log("Loading money");
			counterLabel.text = GameObject.Find ("Inventory").GetComponent<StatTracker>().getBank() + "Moneys";
			lifetime = 2000;
			break;
		default:
			break;
		}

		movingDown = false;
		shouldDisappear = false;

//		gameObject.transform.position = GameUI.GetCamera().ScreenToWorldPoint(START_POS);
		gameObject.transform.localPosition = new Vector3 (UIWidth + 400, UIHeight, 0);
		gameObject.transform.localScale = new Vector3(1f,1f,1f);
		targetY = gameObject.transform.localPosition.y;
		END_X = UIWidth;

	}

//	public void Increment() {
//		counterLabel.text = (startingAmount + incrementAmount).ToString();
//	}
	
	// Update is called once per frame
	void Update () {
//		Vector3 newPos = GameUI.GetCamera().WorldToScreenPoint(gameObject.transform.position);
		Vector3 newPos = gameObject.transform.localPosition;
		if(shouldDisappear) {
			newPos = HidePanelUpdate(newPos);
		} else {
			newPos = ShowPanelUpdate(newPos);
		}

		if(movingDown) {
			newPos.y = Mathf.Lerp(newPos.y, targetY, transitionSmoothing * Time.deltaTime);
			if(newPos.y <= targetY) {
				movingDown = false;
				newPos.y = targetY;
			}
		}
//		gameObject.transform.position = GameUI.GetCamera().ScreenToWorldPoint(newPos);
		gameObject.transform.localPosition = newPos;

		lifetime -= Time.deltaTime;
		if(lifetime <= 0f) {
			shouldDisappear = true;
		}
		//animator.SetFloat("StatCounterDuration", lifetime);
	}

	private Vector3 ShowPanelUpdate(Vector3 newPos) {
		if(newPos.x > END_X) {
			newPos.x = Mathf.Lerp(newPos.x, END_X, transitionSmoothing * Time.deltaTime);
		} else {
			newPos.x = END_X;
		}
		return newPos;
	}

	private Vector3 HidePanelUpdate(Vector3 newPos) {
		if(newPos.x < START_POS.x) {
			newPos.x = Mathf.Lerp(newPos.x, START_POS.x + 0.1f, transitionSmoothing * Time.deltaTime);
		} else {
			DestroyStatCounter();
		}
		return newPos;
	}

	public void MoveDown() {
		movingDown = true;
		targetY += MAX_MOVE_DOWN;
	}

	private void DestroyStatCounter() {
		Destroy (gameObject);
	}

	public bool ShouldDisappear() {
		return shouldDisappear;
	}

}
