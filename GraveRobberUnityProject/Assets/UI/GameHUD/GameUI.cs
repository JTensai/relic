using UnityEngine;
using System.Collections;

public class GameUI {

	private static GameHUDManager gameHUDManagerInstance;

	private static GameHUDManager GetGameHUDInstance() {
        if (gameHUDManagerInstance == null)
        {
            if (GameObject.FindGameObjectWithTag("GameHUD") != null)
            {
                gameHUDManagerInstance = GameObject.FindGameObjectWithTag("GameHUD").GetComponent<GameHUDManager>();
                return gameHUDManagerInstance;
            }
            else
            {
                //Debug.Log("Error: the GameHUD prefab is missing from the scene. Cannot use UI.");
                return null;
            }
        }
        else
        {
            return gameHUDManagerInstance;
        }
	}

//    public static void DisplayInfoPanel(string titleText, string bodyText, float duration)
//    {
//        if (GetGameHUDInstance() != null)
//        {
//            GameUI.GetGameHUDInstance().DisplayInfoPanel(titleText, bodyText, duration);
//        }
//    }

    public static void DisplayInstructionTextArea(string message, float duration)
    {
        if (GetGameHUDInstance() != null)
        {
            GameUI.GetGameHUDInstance().DisplayInstructionTextArea(message, duration);
        }
	}
	
	public static void DisplayNotificationTextArea(string message, float duration)
	{
		if (GetGameHUDInstance() != null)
		{
			GameUI.GetGameHUDInstance().DisplayNotificationTextArea(message, duration);
		}
	}
	
	public static void DisplayKillingSpreeTextArea(string message, float duration)
	{
		if (GetGameHUDInstance() != null)
		{
			GameUI.GetGameHUDInstance().DisplayKillingSpreeTextArea(message, duration);
		}
	}

//    public static void UpdateInfoPanelDuration(float duration)
//    {
//        if (GetGameHUDInstance() != null)
//        {
//            GameUI.GetGameHUDInstance().UpdateInfoPanelDuration(duration);
//        }
//    }

    public static void UpdateInstructionDuration(float duration)
    {
        if (GetGameHUDInstance() != null)
        {
            GameUI.GetGameHUDInstance().UpdateInstructionDuration(duration);
        }
	}
	
	public static void UpdateNotificationDuration(float duration)
	{
		if (GetGameHUDInstance() != null)
		{
			GameUI.GetGameHUDInstance().UpdateNotificationDuration(duration);
		}
	}
	
	public static void UpdateKillingSpreeDuration(float duration)
	{
		if (GetGameHUDInstance() != null)
		{
			GameUI.GetGameHUDInstance().UpdateKillingSpreeDuration(duration);
		}
	}

	public static void UpdateHealthBar(float playerHeath, float maxHealth) {
        if (GetGameHUDInstance() != null)
        {
            GameUI.GetGameHUDInstance().UpdateHealthBar(playerHeath, maxHealth);
        }
	}

//	public static void UpdateDashBar(float time){
//        if (GetGameHUDInstance() != null)
//        {
//		    GameUI.GetGameHUDInstance ().UpdateDashBar (time);
//        }
//	}


	public static void IncrementStatCounter(float currentStatAmount, float incrementAmount, StatTracker.StatTypes statType) {
		if (GetGameHUDInstance() != null)
		{
			GameUI.GetGameHUDInstance().IncrementStatCounter(currentStatAmount, incrementAmount, statType);
		}
	}

	public static Camera GetCamera() {
		if (GetGameHUDInstance() != null)
		{
			return GameUI.GetGameHUDInstance().camera;
		}
		return null;
	}
}
