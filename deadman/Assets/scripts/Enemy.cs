using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHP = 60;
	public int enemyHP;
    public float speed = 1f;
    public int e_damage = 10;
    public Collider2D[] e_attackHitBoxes;
	public GameObject player;
    public Slider healthSlider;
    public bool attackDistance;
	public float damageTime = 2;
    public float deathtime = 0f;
    Animator anim;
	public bool chase;
	public float chaseTimer = 5f;
	public bool idleState= true;
   
    float timer = 0;
    float timeback = 0f;
    bool damaged;

	public GameObject attackTarget;
	public GameObject chaseTarget;
	public bool atkCd =false;
	void Start (){
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag("Player").gameObject;
		healthSlider.maxValue = maxHP;
	}


	public enum State
	{
		Idle = 0,
		Move,     
		Attack,
        Death,
	}
	public State mState = State.Idle;

  

    public void DropItem()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.tag == "Item")
            {
                child.gameObject.SetActive(true);
                child.parent = null;
                child.transform.position = transform.position;
                i -= 1;
            }
        }
    }

    public void GetDamage(int damageDeal)
    {        
		anim.SetTrigger ("TakeDamage");
        damaged = true;
        enemyHP -= damageDeal;
        healthSlider.value = enemyHP;
        if (enemyHP <= 0.0f)
        {
            mState = State.Death;
            anim.SetTrigger("Death");
            DropItem();
            enemyHP = 0;           
        }
    }
    
    
    void idle()
    {
		anim.SetTrigger ("Stop");
    }

	void OnMouseDown(){
		player.GetComponent<player> ().opponent = this.gameObject;
	}
	void OnMouseExit(){
		player.GetComponent<player> ().opponent = null;
	}
		

    private void OnTriggerStay2D(Collider2D target)
    {
		if (mState == State.Death) return;
		if(attackTarget == null)
		{
			if ((target.tag == "Player") || (target.tag == "Partner")  ||(target.tag == "mob")) 
			{ mState = State.Attack; attackTarget = target.gameObject;chase = false;}	
		}
		if (mState == State.Attack)
		{		

			if (!atkCd)
			{	chase = false;
				chaseTimer = 5;
				atkCd = true;
				GetComponent<AudioSource> ().Play();
				Vector3 temp = transform.localScale;
				if(transform.position.x < target.transform.position.x) temp.x = Mathf.Abs(transform.localScale.x);  // Flip left
				else if(transform.position.x > target.transform.position.x) temp.x = -Mathf.Abs(transform.localScale.x);  // Flip right
				transform.localScale = temp;
				timer=0f;
				if (attackTarget != null) {
					attack (attackTarget);
				}

			}
			else
			{	
				timer += Time.deltaTime;
				anim.SetTrigger("Stop");

			}
		} else if(mState == State.Move)
		{
			mState = State.Attack;
		}  
	} 

    private void OnTriggerExit2D(Collider2D other)
	{
		if (mState == State.Death)
			return;
		if (other.gameObject == attackTarget) {
			chaseTarget = other.gameObject;
			attackTarget = null;
			chase = true;
		}
	}
	private void attack(GameObject target){
		anim.SetTrigger("Attack");
		if (target.tag == "Player")
			target.GetComponent<player>().p_getDamage(e_damage); 
		else if (target.tag == "Partner")
			target.GetComponent<partnerAI>().GetDamage(e_damage); 
		else if (target.tag == "mob")
			target.GetComponent<mob>().GetDamage(e_damage);
	}

	public void move(GameObject target)
	{
		if (mState != State.Move|| atkCd)
			return;
		if (target.gameObject != null) {
			Vector3 temp = transform.localScale;
			if (transform.position.x < target.transform.position.x) {
				temp.x = Mathf.Abs (transform.localScale.x); 
			} else {
				temp.x = -Mathf.Abs (transform.localScale.x); 
			}
			transform.localScale = temp;

			transform.position = Vector2.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime);
		} else {
			
			mState = State.Idle;
			idleState = true;			
		}
		anim.SetBool("Walk", true);
	}

    

    private void Update()
    {
		if (timer >= 2f) {
			atkCd = false;
			timer = 0f;
		}
		if (attackTarget == null && chaseTarget == null) {
			mState = State.Idle;
		} else if (chaseTarget != null&&chase) {
			mState = State.Move;
			move (chaseTarget);
			 
		}
		if (enemyHP <= 0) {
			mState = State.Death;
		}
		if (chase) 
		{
			timer += Time.deltaTime;
			chaseTimer -= Time.deltaTime;
			if (chaseTimer <= 0) {
				chase = false;
				chaseTimer = 5f;
				timer = 0;
				mState = State.Idle;
				chaseTarget = null;
				atkCd = false;
				mState = State.Idle;
			}

		}
        if (mState == State.Death)
        {
            deathtime += Time.deltaTime;

            //Debug.Log(deathtime);
            if (deathtime >= 5)
            {
                Destroy(gameObject);
            }
        } 
			if (mState == State.Idle) {
				anim.SetTrigger ("Stop");
				anim.SetBool ("Walk", false);
				idleState = true;
			} else {
				idleState = false;
			}
        timeback += Time.deltaTime;
		
		if (chase && mState == State.Move) {
			move (chaseTarget.gameObject);
        }
        // Debug.Log(enemyHP);     
    }

}




