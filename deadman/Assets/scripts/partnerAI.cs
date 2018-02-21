using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class partnerAI : MonoBehaviour {

	Animator anim;
	public GameObject player;
    public bool attackDistance;
    public float partnerHP;
    public float maxHp = 150;
    public float moveSpeed = 10f;
    public float speed = 2f;
    public float timer = 0;
    public float atkSpd = 2;
    public float damage = 10;
    public Slider hpSlider;
	Vector2 protectPosition; 
    public Collider2D[] e_attackHitBoxes;
    // mode 0 = idle
    // mode 1 = chase enemy
    // mode 2 = move to position
    // mode 3 = follow player
	// mode 4 = protect
    public int mode;
    public Vector3 mousePosition;
    private int nowDir = 1;
    private int prevDir = 1;
    //attack or not while following command
    public bool command;
    //protect left or right
    //1 = left 2 = right
    public int direction;


    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
        partnerHP = maxHp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = hpSlider.maxValue;
	}
    public void GetDamage(int damageDeal)
    {
		anim.SetTrigger ("TakeDamage");
        partnerHP -= damageDeal;
        hpSlider.value = partnerHP;
        if (partnerHP < 1f)
        {
            partnerHP = 0;
            Destroy(gameObject, 1f);
        }
    }

    public void idle()
    {
        //not moving animation 
        anim.SetBool("Move", false);
        anim.SetTrigger("Stop");
    }
	public void moveto()
	{
		mode = 4;
	}
	public void movetoProtect(){
        anim.SetBool("Move", true);
        Vector3 nakama_position = transform.position;
		float step = speed * Time.deltaTime;
        float pos;
        if (direction == 2) { pos = 3; nowDir = 1; }
        else { pos = -3; nowDir = -1; }
		Vector2 protectPosition = new Vector3 (player.transform.position.x-pos,player.transform.position.y,player.transform.position.z);
		transform.position = Vector3.MoveTowards(nakama_position, protectPosition, step);
	}
    public void move()
    {
        command = true;
        anim.SetBool("Move", true);
        prevDir = nowDir;
        //Debug.Log(mousePosition);
        Vector3 nakama_position = transform.position;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(nakama_position, mousePosition, step);
        if (mousePosition.x == nakama_position.x)
        {
            command = false;
            idle();
        }
        if (Mathf.Abs(mousePosition.x - nakama_position.x) > 0.1f)
        {
            if (mousePosition.x > nakama_position.x)
            {
                nowDir = -1;
                //Debug.Log("b");
            }
            else
            {
                nowDir = 1;
                //Debug.Log("c");
            }
        }

    }
    void follow()
    {
        anim.SetBool("Move", true);
        command = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        Vector3 nakama_position = transform.position;
        Vector3 p_position = player.transform.position;
        float step = speed * Time.deltaTime;
        float dist = Vector3.Distance(p_position, nakama_position);
        if (nakama_position.x < p_position.x)
        {
            nowDir = -1;
        } else
        {
            nowDir = 1;
        }
       

        //Debug.Log(dist);
        if (dist >= 2.5f)
        {
            transform.position = Vector3.MoveTowards(nakama_position, p_position, step);
        }
        else if (dist < 2.5f)
        {
            attackDistance = !attackDistance;
            //Debug.Log("stop");
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        
    }
	void Chase ()
	{
        anim.SetBool("Move", true);

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
		if (enemy) {
			Vector3 e_position = enemy.transform.position;
			Vector3 partner_position = transform.position;
			float step = speed * Time.deltaTime;
			float dist = Vector3.Distance(e_position, partner_position);
			if (partner_position.x < e_position.x)
			{
				nowDir = -1;
			}
			else
			{
				nowDir = 1;
			}
			if (dist <= 3.0f && dist >= 2.1f)
			{
				transform.position = Vector3.MoveTowards(partner_position, e_position, step);
			}
			else if (dist < 2.1f)
			{
				//attackDistance = !attackDistance;
				//Debug.Log("stop");
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);   
			}
		} else { mode = 3;
		}
	


    }
    private void OnTriggerStay2D(Collider2D target)
    {
		if (target.tag == "Enemy"|| target.tag == "Boss" )
        {
			float enemyHP = target.gameObject.GetComponent<Enemy>().enemyHP;
            if (!command)mode = 1;
            if (enemyHP > 0)
            {                
                if (timer >= atkSpd)
                {
                    anim.SetTrigger("Attack");
                    timer -= atkSpd;
                    target.gameObject.GetComponent<Enemy>().GetDamage(10);
                    //Debug.Log(enemyHP);
                }
                else {
                    anim.SetTrigger("Stop");
                    timer += Time.deltaTime;
                }
                    
            }
            else mode = 0;
              
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            timer = 0;
        }
    }
   // Update is called once per frame
    void Update () {
        if (mode == 0) {
            idle();
        }
        else if (mode == 1) {
            Chase();
        } else if (mode == 2)
        {
            move();
        }
        else if (mode == 3)
        {
            follow();
        }
		else if (mode == 4)
		{
			movetoProtect();
		}
        //change character facing position
        if (nowDir != prevDir)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        prevDir = nowDir;

    }
}
