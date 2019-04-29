using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour {
	public Text tutorialText;
	private bool waitNext = false;
	private string nextText;
	public bool t1,t2,t3,t4,t5,t6,stageClear;
	float timer = 0f;
	public GameObject player;
	public GameObject enemy;
	public GameObject crate;
	public GameObject CommandImage;
	public GameObject InfectionTutorial;
	public GameObject StoneTutorial;
	public GameObject target;
	public GameObject SaveManager;


	// Use this for initialization
	void Start () 
	{
		tutorialText = tutorialText.GetComponent<Text> ();
		tutorialText.text = "敵をタップして攻撃出来ます";
		t1 = false;
		t2 = false;
		t3 = false;
		t4 = false;
		t5 = false;
		t6 = false;
		stageClear = false;
	}
	public void onImage(){
		CommandImage.SetActive (true);
	}
	public void offImage(){
		CommandImage.SetActive (false);
	}
	public void attackTutorial(){
		if (t1)
			return;
        tutorialText.text = "しかし武器がないとダメジーは少ないです";
        changeTextAfter("段ボールを破壊したら、武器がもらえます");
        t1 = true;
		waitNext = true;
	}
	public void itemTutorial()
	{
		if (t2)
			return;
        tutorialText.text = "右上のアイテムパネルでバットをタップして";
        changeTextAfter("装備出来ます");
        t2 = true;
		t1 = true;
		waitNext = true;
	}
	public void infectionTutorial()
	{
		if (t3)
			return;
        tutorialText.text = "敵の体力が低い時に左下のボタンを押してください";
        changeTextAfter("次に敵を選択して、その敵がゾンビになる");
        t3 = true;
		waitNext = true;
	}
	public void partnerTutorial()
	{
		if (t5)
			return;
		tutorialText.text ="長押しでパートナーを命令できます";
		t5 = true;
		waitNext = true;
	}
	public void stoneTutorial()
	{
		if (t6)
			return;
        tutorialText.text = "アイテムパネルで石を押した後投げたい所にタップ";
        changeTextAfter("投げた場所に他のゾンビを引き寄せます");
        t6 = true;
		waitNext = true;
	}
	private void changeTextAfter (string text){
		waitNext = true;
		nextText = text;
	}
	void Update(){
		if (!stageClear) {
			if (target.GetComponent<Enemy> ().enemyHP <= 0) {
				stageClear = true;
				tutorialText.text = "ステージクリア";
				SaveManager.GetComponent<SaveManager> ().saveClearStage (1);
			}
		} else {
			timer += Time.deltaTime;
			if (timer >= 5f) {
				timer = 0;
				SceneManager.LoadScene (1);
			}
		}
		if (StoneTutorial.GetComponent<StartTutorialCollision>().startTutorial) {
			stoneTutorial ();
		}
		if (InfectionTutorial.GetComponent<StartTutorialCollision>().startTutorial) {
			infectionTutorial ();
		}
		if (waitNext) {
			
			timer += Time.deltaTime;
			if (timer >= 5f) {
				waitNext = false;
				tutorialText.text = nextText;
				timer = 0;
			}
		}
		if (!t2) {
			if(crate.GetComponent<Crate>().CrateHP <=1)
			{
				itemTutorial ();
				t2 = true;
			}
		}
		if (!t4) {
			if(enemy.GetComponent<Enemy>().enemyHP<=79)
			{
				attackTutorial ();
				t4 = true;
			}
		}
		if (!t5) {
			if(enemy.GetComponent<Enemy>().enemyHP<=0)
			{
				partnerTutorial ();
				onImage ();
				t5 = true;
			}
		}
	}


}
