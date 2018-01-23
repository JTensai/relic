using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHUDManager : MonoBehaviour {

    // depricated
//    public UILabel infoTextTitle;
//    public UILabel infoTextBody;
//    private bool infoPanelIsShowing;
//    private Animator hudAnim;
	public GameObject PlayerPortrait;

	public GameObject InstructionTextArea;
    private UILabel InstructionMessage;
    private Animator InstructionAnimator;
    private bool InstructionTextAreaIsShowing;
    
    public GameObject NotificationTextArea;
	private UILabel NotificationMessage;
	private Animator NotificationAnimator;
	private bool NotificationTextAreaIsShowing;
	
	public GameObject KillingSpreeTextArea;
	private UILabel KillingSpreeMessage;
	private Animator KillingSpreeAnimator;
	private bool KillingSpreeTextAreaIsShowing;


	public GameObject PauseScreen;
	
	public UIProgressBar HealthBar;
	public UIProgressBar HealthBarShadow;
	private float HealthBarInitialWidth;
	private float HealthBarSizeIncrement;
	public UILabel HealthText;
	public float HealthChangeDuration = 2;
	public AnimationCurve HealthChangeMovement;
	private bool finishedShrinking = true;
	private float shadowStart;
	
	// XP BAR STUFF
	public GameObject firstXPBar;
	public GameObject XPBarExpansion;
	private List<XPBarDataContainer> XPContainers = new List<XPBarDataContainer>();
	private float nextXPContainerX;
	private int displayDepth;

	public UILabel PlayerLevel;
	private int knownCurrentXP = 0;
	private float knownMaxHP;
//	private float baseHP;
	// DONE WITH XP BAR STUFF

//	public UITexture DashBar;
//	private float initialWidth;
//	private float elapsedTime = 0f;
//	private float rechargeTime = 1f;
//    private int leftSide;
	private bool paused = false;
	private bool shownMissionObjectiveOnce = false;
	private bool showMissionObjective = true;
	public GameObject MissionObjective;
	public GameObject SoundOptions;
	public UISlider volumeSlider;
	public bool soundEnabled = true;

	public GameObject VictoryScreen;
	public GameObject HighScores;
	// This prefab will be instantiated, it should not already exist in the object hierarchy.
	public GameObject StatCounterPanelPrefab;

	private List<StatCounterPanel> statCounters;

	public StatTracker stats { get; private set; }

	private float UIWidth;
	private float UIHeight;

	private bool missionObjectiveFadingOut;
	private bool victoryScreenFadingIn;
	private GameObject inv;

	public GameObject player;
//	private PlayerController playerController;
	private HealthComponent playerHealth;

	public EffectBase XPGainFX;

    //private int wait = 2; // ignore this, used for testing

	// Use this for initialization
    void Start()
    {


		inv = GameObject.FindGameObjectWithTag ("Inventory");
		stats = inv.GetComponent<StatTracker>();

		if (VictoryScreen == null)
		{
			VictoryScreen = transform.FindChild("VictoryScreen").gameObject;

		}
		//VictoryScreen.SetActive (false);
//		PauseScreen.SetActive (false);
//		SoundOptions.SetActive (false);
		volumeSlider.value = 0.5f;
		if (player == null) {
						player = GameObject.FindGameObjectWithTag ("Player");
				}
//		playerController = player.GetComponent<PlayerController> ();
		playerHealth = player.GetComponent<HealthComponent> ();
		knownMaxHP = playerHealth.MaxHealth;
//		baseHP = playerHealth.StartingHealth;
		HealthBarInitialWidth = HealthBar.gameObject.GetComponent<UITexture> ().width;
		HealthBarSizeIncrement = HealthBarInitialWidth / knownMaxHP;

//		hudAnim = GetComponent<Animator>();
		UIWidth = GetComponent<UIPanel>().GetViewSize().x;
		UIHeight = GetComponent<UIPanel>().GetViewSize().y;

        InstructionMessage = InstructionTextArea.GetComponentInChildren<UILabel>();
        InstructionAnimator = InstructionTextArea.GetComponent<Animator>();

		NotificationMessage = NotificationTextArea.GetComponentInChildren<UILabel>();
		NotificationAnimator = NotificationTextArea.GetComponent<Animator>();

		KillingSpreeMessage = KillingSpreeTextArea.GetComponentInChildren<UILabel> ();
		KillingSpreeAnimator = KillingSpreeTextArea.GetComponent<Animator> ();

		ShowMissionObjective();
		this.statCounters = new List<StatCounterPanel>();
	}
	
	// Update is called once per frame
    void Update()
	{
		if (stats== null) {
			stats = inv.GetComponent<StatTracker>();
			Debug.Log("Setting Stats");
		}
		if (showMissionObjective)
		{
			if(player == null)
			{
				player = GameObject.FindGameObjectWithTag("Player");
			}
			player.GetComponent<MovementComponent>().stun(0.5f); // used to disable the input that causes the objective to hide
			if (Input.anyKeyDown)
			{
				HideMissionObjective();
			}
		}

		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown (KeyCode.JoystickButton7))
		{
			if (!paused){
				PauseGame();
			}
			else{
				ResumeGame();
			}
		}

		if (missionObjectiveFadingOut) {
			MissionObjective.GetComponent<UIWidget> ().alpha  -=.05f;
			//Debug.Log(MissionObjective.GetComponent<UIWidget> ().alpha);
			if(MissionObjective.GetComponent<UIWidget> ().alpha <=0)
			{
				MissionObjective.GetComponent<UIWidget> ().alpha = 0;
//				MissionObjective.SetActive(false);
				missionObjectiveFadingOut = false;
			}
		}

		if (victoryScreenFadingIn) {
			//VictoryScreen.SetActive(true);
			VictoryScreen.GetComponent<UIWidget> ().alpha += .05f;

			//Debug.Log(MissionObjective.GetComponent<UIWidget> ().alpha);
			if(VictoryScreen.GetComponent<UIWidget> ().alpha >1)
			{
				
				VictoryScreen.GetComponent<UIWidget> ().alpha = 1;
				victoryScreenFadingIn = false;
			}
		}
		
		UpdateXPBar ();
		if (knownMaxHP != playerHealth.MaxHealth) {
			StartCoroutine (UpdateHealthBarSize ());
		}
	
		int IntHealth = (int)playerHealth.CurrentHealth ;
		int IntMax = (int)playerHealth.MaxHealth ;
		HealthText.text = IntHealth + " / " + IntMax;
		PlayerLevel.text = stats.CurrentLevel.ToString();

        if (InstructionTextAreaIsShowing)
        {
            float instructionTimeout = InstructionAnimator.GetFloat("InstructionTextAreaTimeout") - Time.deltaTime;
            InstructionAnimator.SetFloat("InstructionTextAreaTimeout", instructionTimeout);
            if (instructionTimeout <= 0)
            {
                InstructionTextAreaIsShowing = false;
            }
		}
		if (NotificationTextAreaIsShowing)
		{
			float notificationTimeout = NotificationAnimator.GetFloat("NotificationTextAreaTimeout") - Time.deltaTime;
			NotificationAnimator.SetFloat("NotificationTextAreaTimeout", notificationTimeout);
			if (notificationTimeout <= 0)
			{
				NotificationTextAreaIsShowing = false;
			}
		}
		if (KillingSpreeTextAreaIsShowing)
		{
			float killingSpreeTimeout = KillingSpreeAnimator.GetFloat("KillingSpreeTextAreaTimeout") - Time.deltaTime;
			KillingSpreeAnimator.SetFloat("KillingSpreeTextAreaTimeout", killingSpreeTimeout);
			if (killingSpreeTimeout <= 0)
			{
				KillingSpreeTextAreaIsShowing = false;
			}
		}



//		if (DashBar.width < initialWidth){
//			elapsedTime += Time.deltaTime;
//            if (elapsedTime < rechargeTime)
//            {
//                DashBar.width = (int)(initialWidth * (elapsedTime / rechargeTime));
//                if (DashBar.width == initialWidth)
//                {
//                    // flash the bar to show that it is ready
//                }
//            }
//		}

	}

	/**
	 * Display the info text box with the supplied text for a limited amount of time.
	 * @param titleText The title to appear on the text box (set as "" for no title)
	 * @param bodyText The message body for the info box
	 * @param duration the number of seconds to display the box
	 */


    // Deprecated for newer UI
//    public void DisplayInfoPanel(string titleText, string bodyText, float duration)
//    {
//
//        hudAnim.SetFloat("InfoPanelTimeout", duration);
//        hudAnim.SetTrigger("ShowInfoPanel");
//        infoPanelIsShowing = true;
//        Debug.Log("HI THERE" + infoTextTitle.fontSize);
//        infoTextTitle.fontSize = 80;
//        infoTextBody.fontSize = 80;
//        infoTextTitle.text = titleText;
//        infoTextBody.text = bodyText;
//        infoTextTitle.color = Color.cyan;
//        infoTextBody.color = Color.cyan;
//        infoTextBody.fontStyle = FontStyle.BoldAndItalic;
//        infoTextTitle.fontStyle = FontStyle.BoldAndItalic;
//
//    }

	private void UpdateXPBar(){
	
		if (XPContainers.Count != stats.XPNeeded) {
			for (int i = XPContainers.Count; i < stats.XPNeeded; i++){
				GameObject xp;
				if (i == 0){
					xp = Instantiate(firstXPBar) as GameObject;
					xp.transform.parent = this.transform;
					xp.transform.localScale = new Vector3(1,1,1);
					xp.transform.localPosition = new Vector3 (-UIWidth/2 + 200, UIHeight/2 - 70, 0);
					displayDepth = xp.GetComponent<UITexture>().depth;
					nextXPContainerX = 200 + 40;
				}
				else{
					xp = Instantiate(XPBarExpansion) as GameObject;
					xp.transform.parent = this.transform;
					xp.transform.localScale = new Vector3(1,1,1);
					xp.transform.localPosition = new Vector3 (-UIWidth/2 + nextXPContainerX, UIHeight/2 - 70, 0);
					displayDepth -= 2;
					xp.GetComponent<UITexture>().depth = displayDepth;
					nextXPContainerX = nextXPContainerX + 50;
				}
				XPContainers.Add(new XPBarDataContainer(xp, xp.transform.FindChild("Filler").gameObject));
			}
		}

		if (knownCurrentXP != stats.CurrentXP){
			if (knownCurrentXP < stats.CurrentXP){
				for (int i = knownCurrentXP; i < stats.CurrentXP; i++){
					knownCurrentXP++;
					XPContainers[i].fillerTexture.depth = XPContainers[i].containerTexture.depth - 1;
					StartCoroutine("FillXPBar",XPContainers[i].fillerTexture);
				}

			}
			else if (knownCurrentXP >= stats.CurrentXP){
				knownCurrentXP = 0;
				StartCoroutine("ClearAllXPBars");
			}
		}
	}

	private IEnumerator FillXPBar(UITexture fillerTexture){
		float waitTime = 0.75f;
		yield return new WaitForSeconds (waitTime);
		if(XPGainFX != null){
			EffectBase newInstance = XPGainFX.GetInstance(XPContainers[knownCurrentXP].container.transform.position);
			Vector3 newPos = new Vector3 (newInstance.transform.position.x-0.05f, newInstance.transform.position.y, newInstance.transform.position.z + 1);
			newInstance.transform.position = newPos;
			newInstance.PlayEffect();
		}
//		Debug.Log ("Filling an XP Bar");
		fillerTexture.enabled = true;
	}

	private IEnumerator ClearAllXPBars()
	{		
//		Debug.Log ("Clearing all XP");
		foreach (XPBarDataContainer xpContainer in XPContainers){
			xpContainer.fillerTexture.enabled = false;
		}
//		Debug.Log ("Cleared All XP Containers");
		yield return null;
	}

	private IEnumerator UpdateHealthBarSize()
	{
		if (knownMaxHP != playerHealth.MaxHealth)
		{
			float difference = playerHealth.MaxHealth - knownMaxHP;
			float currentWidth = HealthBar.gameObject.GetComponent<UITexture> ().width;
			float finalWidth = (int) (currentWidth + (HealthBarSizeIncrement * difference));
			float elapsedTime = 0;
			float delta;
			playerHealth.Heal ((int)difference, true);
			UpdateHealthBar(playerHealth.CurrentHealth, playerHealth.MaxHealth);
			knownMaxHP = playerHealth.MaxHealth;
			while (elapsedTime < HealthChangeDuration)
			{
				elapsedTime += Time.deltaTime;
				delta = HealthChangeMovement.Evaluate(elapsedTime/HealthChangeDuration);
				HealthBar.gameObject.GetComponent<UITexture> ().width = (int)Mathf.Lerp(currentWidth, finalWidth, delta);
				yield return null;
			} 
		}
	}

    public void DisplayInstructionTextArea(string message, float duration)
    {

        InstructionAnimator.SetFloat("InstructionTextAreaTimeout", duration);
    //    InstructionAnimator.SetTrigger("ShowInstructionTextArea");
        InstructionTextAreaIsShowing = true;

        InstructionMessage.fontSize = 80;
        InstructionMessage.text = message;
        //InstructionMessage.color = Color.cyan;
        //InstructionMessage.fontStyle = FontStyle.BoldAndItalic;

    }
	
	public void DisplayNotificationTextArea(string message, float duration)
	{
		NotificationAnimator.SetFloat("NotificationTextAreaTimeout", duration);
	//	NotificationAnimator.SetTrigger("ShowNotificationTextArea");
		NotificationTextAreaIsShowing = true;
		
		NotificationMessage.fontSize = 60;
		NotificationMessage.text = message;
		//DialogueMessage.color = Color.cyan;
		//InstructionMessage.fontStyle = FontStyle.BoldAndItalic;
		
	}

	public void DisplayKillingSpreeTextArea(string message, float duration)
	{
		KillingSpreeAnimator.SetFloat("KillingSpreeTextAreaTimeout", duration);
	//	KillingSpreeAnimator.SetTrigger("ShowKillingSpreeTextArea");
		KillingSpreeTextAreaIsShowing = true;
		
		KillingSpreeMessage.fontSize = 80;
		KillingSpreeMessage.text = message;
	}

//    public void UpdateInfoPanelDuration(float duration)
//    {
//        hudAnim.SetFloat("InfoPanelTimeout", duration);
//    }

    public void UpdateInstructionDuration(float duration)
    {
        InstructionAnimator.SetFloat("InstructionTextAreaTimeout", duration);
	}
	
	public void UpdateNotificationDuration(float duration)
	{
		NotificationAnimator.SetFloat("NotificationTextAreaTimeout", duration);
	}
	
	public void UpdateKillingSpreeDuration(float duration)
	{
		KillingSpreeAnimator.SetFloat("KillingSpreeTextAreaTimeout", duration);
	}


	private IEnumerator decreaseHealth = null;
	public void UpdateHealthBar(float current, float max) {
		if (current > playerHealth.CurrentHealth) {
			// was healed
			StartCoroutine (Heal(current, max));
		}
		else{
			// was damaged
			if(decreaseHealth != null){
				StopCoroutine(decreaseHealth);
			}
			decreaseHealth = DecreaseHealth(current, max);
			StartCoroutine (decreaseHealth);
		}
	}

	private IEnumerator Heal(float current, float max)
	{
//		float elapsedTime = 0;

		HealthBar.value = current / max;
		yield return null;
	}

	private IEnumerator DecreaseHealth(float current, float max)
	{		
//		float changeWidth;
//		float initialLeft = shadowTexture.leftAnchor.absolute;
		// position the shadow appropriately
		if (finishedShrinking) 
		{
			shadowStart = HealthBar.value;
//			shadowTexture.rightAnchor.absolute = healthTexture.rightAnchor.absolute - (int)(healthTexture.width * (1 - HealthBar.value));
		}
		else
		{
			shadowStart = HealthBarShadow.value;
		}

//		shadowTexture.leftAnchor.absolute = healthTexture.rightAnchor.absolute - (int) (healthTexture.width * (1 - current / max));
//		changeWidth = initialLeft - shadowTexture.leftAnchor.absolute;
		
//		yield return null;// makes sure the anchors have moved
//		if (HealthBarShadow.value != 1)
//		{
//
//		}


		float elapsedTime = 0;
		float delta = 0;

		HealthBar.value = current / max;
		yield return null;
		float shadowFinish = HealthBar.value;
		
		finishedShrinking = false;
		while (elapsedTime < HealthChangeDuration)
		{
			elapsedTime += Time.deltaTime;
			delta = HealthChangeMovement.Evaluate (elapsedTime / HealthChangeDuration);
			HealthBarShadow.value = Mathf.Lerp (shadowStart, shadowFinish, delta);
			yield return null;
		}
		finishedShrinking = true;
	}

	public void ShowMissionObjective()
	{
		showMissionObjective = true;
		MissionObjective.SetActive (true);
		MissionObjective.GetComponent<JobDescription> ().ShowRelicExample ();
		MissionObjective.GetComponent<UIWidget> ().alpha = 1;
		PauseScreen.GetComponent<UIWidget> ().alpha = 0;
		Time.timeScale = 0;
	}

	public void HideMissionObjective()
	{
		showMissionObjective = false;

		if (paused)
		{
			Time.timeScale = 0;
			PauseScreen.GetComponent<UIWidget> ().alpha = 1;
		}
		else
		{	
			Time.timeScale = 1;
		}		

		if (!shownMissionObjectiveOnce){
			missionObjectiveFadingOut = true;
			shownMissionObjectiveOnce = true;
		}
		else{
			MissionObjective.GetComponent<UIWidget> ().alpha = 0;
		}
		MissionObjective.GetComponent<JobDescription> ().HideRelicExample ();
	}


	public void HideVictoryScene()
	{
		VictoryScreen.GetComponent<UIWidget> ().alpha = 0;
		if (paused)
		{
			Time.timeScale = 0;
			PauseScreen.GetComponent<UIWidget> ().alpha = 1;
		}
		else
		{	
			Time.timeScale = 1;
		}
	}

	public void PauseGame ()
	{
		paused = true;
		PauseScreen.SetActive (true);
		SoundOptions.SetActive (true);
		MissionObjective.SetActive (true);
		PauseScreen.GetComponent<UIWidget> ().alpha = 1;
		MissionObjective.GetComponent<UIWidget> ().alpha = 0;
		Time.timeScale = 0;
	}

	public void ResumeGame() 
	{
		paused = false;

		PauseScreen.SetActive (false);
		SoundOptions.SetActive (false);
		MissionObjective.SetActive (false);

		PauseScreen.GetComponent<UIWidget> ().alpha = 0;
		SoundOptions.GetComponent<UIWidget> ().alpha = 0;
//		if (showMissionObjective)
//		{
//			Time.timeScale = 0;
//			MissionObjective.SetActive(true);
//			MissionObjective.GetComponent<UIWidget> ().alpha = 1;
//		}
//		else
//		{
			Time.timeScale = 1;
//		}
	}


	public void showShipShop()
	{

		Time.timeScale = 0;
		//VictoryScreen.SetActive (true);
		VictoryScreen.GetComponent<UIWidget> ().alpha = 1;
		VictoryScreen.GetComponent<ShipShopStat> ().BuildStatSheet ();

		}

	public void ShowVictoryScreen()
	{
		VictoryScreen.SetActive (true);
		Time.timeScale = 0;
		victoryScreenFadingIn = true;
		//VictoryScreen.GetComponent<UIWidget> ().alpha = 1;
		VictoryScreen.GetComponent<VictoryScreen> ().BuildStatSheet ();
	}

	public void ShowSoundOptions ()
	{
//		PauseScreen.SetActive (false);
//		SoundOptions.SetActive (true);
		PauseScreen.GetComponent<UIWidget> ().alpha = 0;
		SoundOptions.GetComponent<UIWidget> ().alpha = 1;
	}

	public void HideSoundOptions()
	{
//		SoundOptions.SetActive (false);
		SoundOptions.GetComponent<UIWidget> ().alpha = 0;
	}

	public void ShowHighScores()
	{
		Time.timeScale = 0;
//		HighScores.SetActive (true);
		HighScores.GetComponent<UIWidget> ().alpha = 1;
		Debug.Log ("ShowHighScores2");
		HighScores.GetComponent<HSController> ().GetScore ();
		//HighScores.GetComponent<VictoryScreen> ().BuildStatSheet ();
	}

	public void HideHighScores()
	{
//		HighScores.SetActive (false);
		HighScores.GetComponent<UIWidget> ().alpha =0;
	}

	public void IncrementStatCounter(float currentStatAmount, float incrementAmount, StatTracker.StatTypes statType) {
		GameObject newPanel = Instantiate(StatCounterPanelPrefab) as GameObject;
		newPanel.transform.parent = this.transform;
		newPanel.transform.localPosition = new Vector3 (UIWidth, UIHeight, 0);
		for(int i = statCounters.Count - 1; i >= 0; i--) {
			StatCounterPanel scp = statCounters[i];
			if(scp == null) {
				statCounters.RemoveAt(i);
			} else {
				scp.MoveDown();
			}
		}

		StatCounterPanel panelScript = newPanel.GetComponent<StatCounterPanel>();
		panelScript.Initialize(currentStatAmount, incrementAmount, statType, UIWidth, UIHeight, stats);
		statCounters.Add(panelScript);
	}
}
