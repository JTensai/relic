using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatTracker : MonoBehaviour {
	public List<relicInfo> collectedMiniRelics = new List<relicInfo>();
	private List<GameObject> miniRelics = new List<GameObject>();
	private GameHUDManager GameHUD;

	public float pillarKill = 0;
	public float doubleKill = 0;
	public float tripleKill = 0;
	public float megaKill = 0;
	public float poisonWater = 0;
	private float chargerKills = 0;
	public float totalKills = 0;

	private List<float> keys;
	public float bank;

	
	private int totalShamblers = 0;
	private int shamblerDeaths = 0;
	private int totalChargers = 0;
	private int chargerDeaths = 0;

	public float deathNumber = 0;

	private float timer;
	private bool timerOn = false;
	private float killingSpree;

    public int MaxNormalPillarHelp = 2;
    public int MaxBridgePillarHelp = 1;
    public int MaxWedgePillarHelp = 1;
    public int MaxBreakablesHelp = 1;
    public int MaxThrowablesHelp = 2;


    private int NormalPillarsHit = 0;
    private int BridgePillarsHit = 0;
    private int WedgePillarsHit = 0;
    private int BreakablesHit = 0;
    private int DebrisThrownCount = 0;

	[ReadOnlyAttribute]
	public int XPNeeded = 3;
	[ReadOnlyAttribute]
	public int CurrentXP = 0;
	[ReadOnlyAttribute]
	public int CurrentLevel = 1;

	private int originalXPNeeded;

	public EffectBase DoubleKillFX;
	public EffectBase TripleKillFX;
	public EffectBase MegaKillFX;

	private GameObject player;
	private PlayerController playerController;
	private float playerStartSpeed;
	public float PowerUpSpeedBoost = 0.1f;
	public float PowerUpHPGain = 2;

//	private Vector3 LastDeath;

	// TODO: add more types here so that the stat counter stack knows what to display.
	public enum StatTypes { ShamblerDeath, ChargerDeath, money };
    		
	void Start () {


		keys = new List<float>();
		originalXPNeeded = XPNeeded;
		player = GameObject.Find ("Tesla");//GameObject.FindGameObjectWithTag ("Player");
		playerController = player.GetComponent<PlayerController> ();
		playerStartSpeed = playerController.WalkSpeed;

		GameHUD = GetComponentInParent<GameHUDManager> ();

		foreach (Collect c in GameObject.FindObjectsOfType<Collect>())
		{
			miniRelics.Add(c.gameObject);
		}


		DontDestroyOnLoad(this);		
	}


	public void showMoney(){
			GameUI.IncrementStatCounter(0, 1, StatTypes.money);
			
	}

	 void Update()
	{		
		if (CurrentXP == XPNeeded) {
			LevelUp();
		}
		if (timerOn) {
			timer += Time.deltaTime;

			if(timer > 1)
			{
				timerOn = false;
				if(killingSpree == 2)
				{
					//GameUI.DisplayInstructionTextArea("DOUBLE KILL!", 5f);
//					if(DoubleKillFX != null && LastDeath != null){
//						EffectBase newInstance = DoubleKillFX.GetInstance(LastDeath);
//						newInstance.PlayEffect();
//					}
					GameHUD.DisplayKillingSpreeTextArea("Double Kill", 1.5f);
					doubleKill ++;					
//					CurrentXP++;
				}
				else if(killingSpree == 3)
				{
					//GameUI.DisplayInstructionTextArea("TRIPLE KILL!", 5f);
//					if(TripleKillFX != null && LastDeath != null){
//						EffectBase newInstance = TripleKillFX.GetInstance(LastDeath);
//						newInstance.PlayEffect();
//					}
//					killCount = 2; // In case they happen to get more than a triple kill we should reset it back to 2, so basically a quadra kill == 2 triples lol
					GameHUD.DisplayKillingSpreeTextArea("Triple Kill!", 1.5f);
					tripleKill ++;
					// Upping the current XP by 2 here causes a bug in the xp bar, maybe doing it one at a time will result in no bug
//					CurrentXP++;
//					CurrentXP++;
				}
				else if (killingSpree >= 4)
				{
//					if(MegaKillFX != null && LastDeath != null){
//						EffectBase newInstance = MegaKillFX.GetInstance(LastDeath);
//						newInstance.PlayEffect();
//					}
					GameHUD.DisplayKillingSpreeTextArea("Mega Kill!!!", 2f);
					megaKill++;
				}
				
//				Debug.Log("GOT" + killCount+ "kills");
				killingSpree =0;
				timer = 0;
			}
		}		
	}

	public void addToBank (float input)
	{bank += input;}

	public float getBank()
	{return bank;
		}

     public void upKill(AttackEnum input, GameObject thingKilled)
     {
//		Vector3 temp = thingKilled.transform.position;
//		LastDeath = new Vector3 (temp.x, temp.y, temp.z);
		switch (input) {
		case AttackEnum.Charger:
			chargerKills++; 
			break;
		case AttackEnum.Pillar:
			pillarKill++;
			break;
		case AttackEnum.PoisonWater:
			poisonWater++;
			break;		
		}

		switch (thingKilled.tag) {
		case "Shambler":
			shamblerDeaths++;
			CurrentXP++;
			GameUI.IncrementStatCounter(shamblerDeaths, 1, StatTypes.ShamblerDeath);
			break;
		case "Charger":
			chargerDeaths++;
//			CurrentXP += 2;
			CurrentXP++;
			GameUI.IncrementStatCounter(chargerDeaths, 1, StatTypes.ChargerDeath);
			break;
		}


		totalKills++;
//		if (totalKills % 5 == 0) {
//			speedBoost();			
//		}

		killingSpree++;
		timerOn = true;
		timer = 0;
     }

	public void upDeath()
	{
		XPNeeded = originalXPNeeded;
		CurrentXP = 0;
//		player.GetComponent<HealthComponent> ().MaxHealth = player.GetComponent<HealthComponent> ().StartingHealth;
		deathNumber++;
	}

	private void LevelUp(){
		CurrentLevel++;
		CurrentXP = 0;
		XPNeeded++;
		XPNeeded++;
		playerController.PlayLevelUpFX ();
		HealthUp ();
		GameUI.DisplayInstructionTextArea("Powered Up! +" + PowerUpHPGain + " Max Health!", 5f);
	}
	
	public void MiniArtifactGrabed(relicInfo miniRelic)
	{
		collectedMiniRelics.Add(miniRelic);
		speedBoost ();
		GameUI.DisplayInstructionTextArea("You found a " + miniRelic.name + "! +" + PowerUpSpeedBoost*100 + "% Speed!", 5f);
	}

	private void HealthUp()
	{
		player.GetComponent<HealthComponent>().IncreaseMaxHealth((int)PowerUpHPGain);
	}

	public void speedBoost()
	{		
		playerController.WalkSpeed += (playerStartSpeed * PowerUpSpeedBoost);
	}



    public void InteractableHit(GameObject source)
    {
        if (source.GetComponent<Pillar>() != null){ 
            switch (source.name)
            {
                case "PillarNormal":
                    NormalPillarsHit++;
                    break;
                case "PillarBridgeWedge":
                    WedgePillarsHit++;
                    break;
                case "PillarBridge":
                    BridgePillarsHit++;
                    break;
            }
        }
        else if (source.GetComponent<ThrowablePrefab>() != null)
        {
            DebrisThrownCount++;
        }
        else if (source.GetComponent<BreakingPot>() != null)
        {
            BreakablesHit++;
        }
    }

    public bool shouldShowHelperText(GameObject source)
    {
        bool result = true;
        if (source.GetComponent<Pillar>() != null)
        {
            switch (source.name)
            {
                case "PillarNormal":
                    if (NormalPillarsHit >= MaxNormalPillarHelp)
                    {
                        result = false;
                    }
                    break;
                case "PillarBridgeWedge":
                    if (WedgePillarsHit >= MaxWedgePillarHelp)
                    {
                        result = false;
                    }
                    break;
                case "PillarBridge":
                    if (BridgePillarsHit >= MaxBridgePillarHelp)
                    {
                        result = false;
                    }
                    break;
            }
        }
        else if (source.GetComponent<ThrowablePrefab>() != null)
        {
            if (DebrisThrownCount >= MaxThrowablesHelp)
            {
                result = false;
            }
        }
        else if (source.GetComponent<BreakingPot>() != null)
        {
            if (BreakablesHit >= MaxBreakablesHelp)
            {
                result = false;
            }
        }
        return result;
    }

	public float getCharger()
	{return chargerKills;}

	public float getPoison()
	{return poisonWater;}

	public float getKills()
	{return totalKills;}

//	public float getRelics()
	//	{return collectedMiniRelics.Count;}

	public float getDouble()
	{return doubleKill;}

	public float getTriple()
	{return tripleKill;}

	public float getPillar()
	{return pillarKill;}
	
	public void incrementTotalShamblers(){
		totalShamblers++;
	}
	public void incrementTotalChargers(){
		totalChargers++;
	}

	public List<relicInfo> getCollectedMiniRelics()
	{
		return collectedMiniRelics;
	}
	public List<GameObject> getMiniRelics()
	{
		return miniRelics;
	}

	public int getTotalShamblers(){
		return totalShamblers;
	}

	public int getTotalChargers(){
		return totalChargers;
	}

	public void addKey(float input)
	{GameUI.DisplayInstructionTextArea("You found a Key", 5f);
		keys.Add (input);
		}

	public bool getKey(float input)
		{if (keys.Contains(input)) {
						return true;
				} else {
			return false;
		}

	}

	public Dictionary<string, StatDataContainer> BuildFinalStats(){
		Dictionary<string, StatDataContainer> FinalStats = new Dictionary<string, StatDataContainer>();

		FinalStats ["Total Kills"] = new StatDataContainer(totalKills, 100f, totalKills + " / " + (totalChargers + totalShamblers).ToString());
		FinalStats ["Pillar Kills"] = new StatDataContainer(pillarKill, 300f, pillarKill.ToString());
		FinalStats ["Double Kills"] = new StatDataContainer(doubleKill, 500f, doubleKill.ToString());
		FinalStats ["Triple Kills"] = new StatDataContainer(tripleKill, 1000f, tripleKill.ToString());
		FinalStats ["Mega Kills"] = new StatDataContainer(megaKill, 2000f, megaKill.ToString());
//		FinalStats ["Charger Homicides"] = new StatDataContainer(chargerKills, 200f, chargerKills.ToString());
		FinalStats ["Deaths"] = new StatDataContainer(deathNumber, -500f, deathNumber.ToString());

		return FinalStats;
	}

	public void ResetStats()
	{
		pillarKill = 0;
		doubleKill = 0;
		tripleKill = 0;
		megaKill = 0;
		poisonWater = 0;
		chargerKills = 0;
		totalKills = 0;
		
		totalShamblers = 0;
		shamblerDeaths = 0;
		totalChargers = 0;
		chargerDeaths = 0;
		
		deathNumber = 0;

		XPNeeded = 3;
		CurrentXP = 0;
	}
}

