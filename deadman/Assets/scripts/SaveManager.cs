using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SaveManager : MonoBehaviour {

	public int currentStage= 0;
	public int clearedStage =0;
	void Start(){
		clearedStage = LoadStage ();
	}

	public void saveClearStage(int Stage)
	{
		PlayerPrefs.SetInt ("stage", Stage);
	}
	public int LoadStage()
	{
		int stage=0;
		if(PlayerPrefs.HasKey("stage"))stage = PlayerPrefs.GetInt("stage");
		return stage;
	}
	public void stage(int stg){
		currentStage = 0;
	}


	public void SaveStatus(PlayerStats.Status stats, PlayerStats.Status addedStats)
	{
		PlayerPrefs.SetInt("currLevel", stats.currLevel);
		PlayerPrefs.SetInt("strength", stats.strength);
		PlayerPrefs.SetFloat("speed", stats.speed);
		PlayerPrefs.SetInt("knowledge", stats.knowledge);
		PlayerPrefs.SetInt("defense", stats.defense);
		PlayerPrefs.SetInt("expToNextLevel", stats.expToNextLevel);

		PlayerPrefs.SetInt("addedStrength", addedStats.strength);
		PlayerPrefs.SetFloat("addedSpeed", addedStats.speed);
		PlayerPrefs.SetInt("addedKnowledge", addedStats.knowledge);
		PlayerPrefs.SetInt("addedDefense", addedStats.defense);
	}

	public PlayerStats.Status LoadCurrStatus()
	{
		PlayerStats.Status temp = new PlayerStats.Status ();

		if (PlayerPrefs.HasKey ("currLevel")) temp.currLevel = PlayerPrefs.GetInt ("currLevel");
		if (PlayerPrefs.HasKey ("strength")) temp.strength = PlayerPrefs.GetInt ("strength");
		if (PlayerPrefs.HasKey ("speed")) temp.speed = PlayerPrefs.GetFloat ("speed");
		if (PlayerPrefs.HasKey ("knowledge")) temp.knowledge = PlayerPrefs.GetInt ("knowledge");
		if (PlayerPrefs.HasKey ("defense")) temp.defense = PlayerPrefs.GetInt ("defense");
		if (PlayerPrefs.HasKey ("expToNextLevel")) temp.expToNextLevel = PlayerPrefs.GetInt ("expToNextLevel");

		return temp;
	}

	public PlayerStats.Status LoadAddedStatus()
	{
		PlayerStats.Status temp = new PlayerStats.Status ();

		if (PlayerPrefs.HasKey ("addedStrength")) temp.strength = PlayerPrefs.GetInt ("addedStrength");
		if (PlayerPrefs.HasKey ("addedSpeed")) temp.speed = PlayerPrefs.GetFloat ("addedSpeed");
		if (PlayerPrefs.HasKey ("addedKnowledge")) temp.knowledge = PlayerPrefs.GetInt ("addedKnowledge");
		if (PlayerPrefs.HasKey ("addedDefense")) temp.defense = PlayerPrefs.GetInt ("addedDefense");

		return temp;
	}
}
