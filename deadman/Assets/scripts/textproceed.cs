using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class textproceed : MonoBehaviour {

	public Text text;
	int textnumber =0;
	void progress() {
		textnumber += 1;
	}

	void textchange (){
		if (textnumber == 1){
			text.text = "「あ・‥（なぜ俺はこんな状態になっている）";
		} else if (textnumber == 2){
			text.text = "「あ“ぁ”？（声も出せない）」）";
		}else if (textnumber == 3){
			text.text = "「あぁぁ(なんだ…これは…)";
		}else if (textnumber == 4){
			text.text = "「あぁあ“！？（どうなっているんだ？！）」";
		}else if (textnumber == 5){
			text.text = "「ぐう、ぐうう（怪物たちにも俺を襲ってこない…まさか…俺はこいつらみたいになっているのか？！！！）」";
		}else if (textnumber == 6){
			text.text = "「ああああああ！！！！（しかしこれはこれでチャンスだ！これであいつらに復讐できる！）」）";
		}else if (textnumber == 7){
			text.text = "そして主人公は人間を見つけて";
		}else if (textnumber == 8){
			text.text = "「ああ！！（さっそくこの力を試してやるか！）」";
		}else if (textnumber == 9) {
			SceneManager.LoadScene (2);
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			progress ();
		}
		textchange ();
		
	}
}
