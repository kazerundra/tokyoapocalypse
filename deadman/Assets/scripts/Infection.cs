using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Infection : MonoBehaviour
{
    public GameObject AttackArea;
    public Image activeImage;
    public float cooldownTime = 5.0f;
    public bool isPressed = false;
    public GameObject mob;
    float cooldownTimer;
    public bool Activate = false;
    public GameObject PauseMenu;
    
    public void Pressed()
    {
        if (activeImage.fillAmount == 1.0f && PauseMenu.GetComponent<UI>().PauseMenuEnable == false) isPressed = !isPressed;
    }

    private void Start()
    {
        PauseMenu = GameObject.FindGameObjectWithTag("commandMenu").gameObject;
    }

    void Update()
    {
        Vector3 mouseposition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mouseposition);
        if (isPressed)
        {
            //            Debug.Log(mouseposition);
            AttackArea.GetComponent<SelectArea>().ActivateCircle(worldMousePos);
            //            Debug.Log(scanArea.Length);
            isPressed = false;
        }

        
        Collider2D[] scanArea = Physics2D.OverlapCircleAll(worldMousePos, 3.5f);
        if (Activate)
        {
            foreach (Collider2D enemy in scanArea)
            {

                if (enemy.gameObject.tag == "Enemy")
                {
					
					float infectionRate = 0;
					infectionRate = 100 - ((enemy.gameObject.GetComponent<Enemy>().enemyHP* 100) / enemy.gameObject.GetComponent<Enemy>().maxHP );
                    infectionRate = infectionRate - (infectionRate % 10);
					float infectionChance = Random.Range(40, 100);
					Debug.Log (infectionRate + "rate");
					Debug.Log (infectionChance);
					if (infectionChance <= infectionRate) {
						Destroy (enemy.gameObject);
						Instantiate (mob, enemy.gameObject.transform.position, Quaternion.identity);
						Activate = false;
					} else
						Activate = false;
                }
            }

            //activeImage.fillAmount = 0.0f;
            isPressed = false;
        }

        if (activeImage.fillAmount < 1.0f)
        {
            if (cooldownTimer < cooldownTime)
            {
                cooldownTimer += Time.deltaTime;
                activeImage.fillAmount = cooldownTimer / cooldownTime;
                if (activeImage.fillAmount >= 1.0f)
                {
                    activeImage.fillAmount = 1.0f;
                    cooldownTimer = 0.0f;
                }
            }
        }
    }
}
        
    


