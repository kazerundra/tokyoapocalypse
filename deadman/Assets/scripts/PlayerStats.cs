using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour 
{
	public GameObject GOStatPanel;
	public const int maxLevel = 10;

	int mAddStatPoints = 0, mPointsTemp = 0;
	float mExpMaxTimer = 1.0f;
	bool mIsShowExpPanel = false;

	GameObject mGOLevelUpPanel;
	SaveManager mSaveManager;
	public Status mStatus;

	// -----------------------------Status------------------------------------
	[System.Serializable]
	public class Status
	{
		public int currLevel = 1;
		public int strength = 1;
		public float speed = 1f;
		public int knowledge = 1;
		public int defense = 1;
		public int expToNextLevel = 5;

		public Status()
		{
			currLevel = 1;
			strength = 1;
			speed = 1f;
			knowledge = 1;
			defense = 1;
			expToNextLevel = 5;
		}

		public Status(int level, int str, float spd, int knowledge, int def, int expToNextLevel)
		{
			this.currLevel = level;
			this.strength = str;
			this.speed = spd;
			this.knowledge = knowledge;
			this.defense = def;
			this.expToNextLevel = expToNextLevel;
		}

		public bool IsMaxLevel()
		{ return currLevel == PlayerStats.maxLevel; }

		public void Clear()
		{
			this.currLevel = 0;
			this.strength = 0;
			this.speed = 0;
			this.knowledge = 0;
			this.defense = 0;
			this.expToNextLevel = 0;
		}
	}
	void Awake()
	{
		mSaveManager = GameObject.FindGameObjectWithTag ("SaveManager").GetComponent<SaveManager>();
	}

	void Start () 
	{
		mGOLevelUpPanel = GameObject.FindGameObjectWithTag("LevelUpPanel").gameObject;
		mGOLevelUpPanel.SetActive(false);
		mStatus = new Status (1, 1, 1, 1, 1, 5);
	}
}
