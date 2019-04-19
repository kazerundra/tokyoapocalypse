using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour 
{
    /*character stat*/
    public int playerHp = 100;
    public int maxPlayerHP = 100;
	public float speed = 3.0f;
	public bool alive;

    //limit movement 

    /*character status*/
    public bool rightface;
    public bool moveStatus;
    public bool menuStatus;
    public GameObject DeadScene;

    //android touch system
    bool isTouchAttackMove;
    bool isTouchMoveStatus;
    float pressPeriod = 0.0f;
    public bool isAttackMove = false;
    GameObject enemyTarget;
    Vector3 touchPosition;
    Vector3 screenToWorldPointPosition;
    public Rigidbody2D rigid2d;

    //heart animation
    Animator anim;
    public int heartbeattype;
    public Image healthImage;
    public Image healthBackground;
    float heartBeat;

    //status point system
    int mAddStatPoints = 0, mPointsTemp = 0;
    float mExpMaxTimer = 1.0f;
    bool mIsShowExpPanel = false;
    GameObject mGOLevelUpPanel;

    //damage effect
    GameObject mHitEffect;
    GameObject mGetDamageEffect;
    SpriteRenderer sprite;

    // use item
    PlayerStats mPlayerStats;
    ItemUse itemUse;

	public GameObject opponent;
	public GameObject targetopponent;
	public GameObject holdmenu; 
	//石を装備状態
	private bool waitStone;
	//攻撃できるかどうか
	public bool canAttack;
	public bool atkCd= false;
	private float atkTimer = 3;
	public float atkSpeed;
	public Vector2 curPos;
	public Vector2 afPos;
	public float timer;
	private Vector3 NewMP;



    void Start () 
    {
		alive = true;
        anim = GetComponent<Animator> ();
        heartbeattype = 1;
        healthImage.GetComponent<Tweener>().Play("heartbeat");
        healthBackground.GetComponent<Tweener>().Play("heartbeat");
        mGetDamageEffect = GameObject.FindGameObjectWithTag("GetDamageEffect").gameObject;
        mHitEffect = Resources.Load("Prefabs/HitEffect") as GameObject;
        sprite = GetComponent<SpriteRenderer>();
		rigid2d = GetComponent<Rigidbody2D> ();
        itemUse = GetComponent<ItemUse>();
        mPlayerStats = GetComponent<PlayerStats> ();
		//Physics2D.IgnoreLayerCollision (9, 11);
		//Physics2D.IgnoreLayerCollision (11, 8);
        mGetDamageEffect.SetActive(false);
    }

    public GameObject ClickSelect()
    {
        Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);
		//Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.left, Color.green);
        if (hit)
        {
            Debug.Log(hit.transform.name);
            enemyTarget = hit.transform.gameObject;
            return hit.transform.gameObject;
        }
        else
        {
            return null;
        }
    }

    public void move(Vector3 mousePosition)
    {
		anim.SetTrigger ("Run");
		Vector2 eek;
		Vector2 newPos = Vector2.MoveTowards(gameObject.transform.position, mousePosition,speed* Time.deltaTime);
	
		GetComponent<Rigidbody2D>(). MovePosition (newPos);
		if(transform.position.x == mousePosition.x && transform.position.y == mousePosition.y)
        {
            anim.SetTrigger("Stop");
            anim.ResetTrigger("Run");
        }
    }
	void FixedUpdate(){
		if (moveStatus)
		{
			
			timer += Time.deltaTime;

			if (timer > 1 && timer < 1.5) {
				curPos = transform.position;
			} else if (timer > 1.5 && timer < 2) {
				afPos = transform.position;
			} 
			else if (timer >2 && afPos == curPos) {
				moveStatus = false;
				timer = 0;
			} else if (timer >2) {
				timer = 0;
			}


			isAttackMove = false;
			move(screenToWorldPointPosition);
			Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
			Vector2 WorldPos = new Vector2(screenToWorldPointPosition.x, screenToWorldPointPosition.y);
			if (playerPos == WorldPos) {
				moveStatus = false;
				timer = 0;
				anim.SetTrigger ("Stop");
			}
		
		}else if (isAttackMove) {
			moveStatus = false;
			if(targetopponent != null)
			{			
				Vector2 enemyPosition = targetopponent.transform.position;
				Vector2 temp = enemyPosition;
				Vector2 pPos = rigid2d.position;
				if (enemyPosition.x <= pPos.x) {
					if (targetopponent.tag == "crate") {
						temp.x += 1.5f; 
					}else{
						temp.x += 1.0f; 
					} 
					rightface = true;
				}
				else {
					if (targetopponent.tag == "crate") {
						temp.x -= 1.5f;
					} else {
						temp.x -= 1.0f;	
					}
					 rightface = false; };
				if (GetComponentInChildren<AttackRange> ().StartAttack != true) {
					move(temp);
				}
			
				GetComponentInChildren<AttackRange> ().attackMove (targetopponent);
				if (GetComponentInChildren<AttackRange>().StartAttack ==true && atkCd ==false)
				{	GetComponentInChildren<AttackRange> ().AttackMove = false;
					isAttackMove = false;

					LaunchAttack (mPlayerStats.mStatus.strength + itemUse.atkUP);
					
					Debug.Log ("attack" +mPlayerStats.mStatus.strength + itemUse.atkUP);
					GetComponentInChildren<AttackRange> ().StartAttack = false;
					atkCd = true;
				} 
			}
		}
	}


    void Update() 
    {
		if (atkCd) 
		{
			atkTimer -= Time.deltaTime;
			if (atkTimer <= 0) {
				atkCd = false;
				atkTimer = 3;
			}
		}
		if (alive){
        /*========================== Touch system =============================*/

        if (!EventSystem.current.IsPointerOverGameObject())
        {
					if (!menuStatus && (gameObject.GetComponent<Infection>().isPressed == false)&& (gameObject.GetComponent<Infection>().waitInfection == false)) 
            {
                    if (itemUse.IsStoneCreated && Input.GetMouseButtonDown(0))
                    {
                       
                        itemUse.ThrowStone(Camera.main.ScreenToWorldPoint(Input.mousePosition));
						waitStone = true;
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0))
                        { pressPeriod = 0; }
                        else if (Input.GetMouseButton(0))
                        { pressPeriod += Time.deltaTime; }

						if (Input.GetMouseButtonUp(0) && pressPeriod < 0.5f&& waitStone==false)
                        {
                            if ((holdmenu.GetComponent<holdMenu>().swipe == false))
                            {

                                isAttackMove = false;
                                Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                Vector3 temp = transform.localScale;

                                if (transform.position.x > touchPos.x) temp.x = Mathf.Abs(transform.localScale.x);  // Flip left
                                else if (transform.position.x < touchPos.x) temp.x = -Mathf.Abs(transform.localScale.x);  // Flip right
                                transform.localScale = temp;

                                if (opponent != null)
                                {
                                    targetopponent = opponent;
                                    isAttackMove = true;
                                }
                                else
                                {
															
									moveStatus = true;
									screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                }
                            }
                        }
                    }
            }
        }
			if (Input.GetMouseButtonUp(0)&&waitStone==true)
			{
				GetComponent<ItemUse>().mIsStoneCreated = false;
				waitStone = false;
			}

	

        float moveSpeed = mPlayerStats.mStatus.speed;
		}
    }

    // Healing fucntion.
    public void Heal(int amount)
    {
        heartbeat();
        if (playerHp <= maxPlayerHP)
        {
            playerHp += amount;
            if (playerHp > maxPlayerHP) playerHp = maxPlayerHP;
        }

        healthImage.fillAmount = (float)playerHp / 100 ;
    }

    // Item attackup function アイテムを使って、ダメージをアップします。
    public void AttackUp(int amount)
    { LaunchAttack(mPlayerStats.mStatus.strength + amount);  }

    IEnumerator GetDamageEffect() 
    {
        mGetDamageEffect.SetActive(true);

        Image image = mGetDamageEffect.GetComponent<Image>();
        Color c = image.color;
        c.a = 1.0f;
        while (c.a > 0) 
        {
            c.a = c.a - 0.05f;
            image.color = c;
            yield return null;
        }
		mGetDamageEffect.SetActive(false);
    }

    //attack script 攻撃
    private void LaunchAttack(int damageValue)
    {	
		if (targetopponent.GetComponent<Enemy> () != null) {
			targetopponent.GetComponent<Enemy> ().GetDamage (damageValue);
		} else if (targetopponent.GetComponent<Crate> () != null) {
			targetopponent.GetComponent<Crate> ().Crate_damage(damageValue);
		}

		GetComponent<AudioSource> ().Play();
		anim.SetTrigger("Attack");

		Instantiate (mHitEffect, targetopponent.transform.position, Quaternion.identity);
        //var IgnoreLayer = (1 << 9);
		/*
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.left, Color.green);

        RaycastHit2D hit;
        if( rightface) hit= Physics2D.Raycast((new Vector2(transform.position.x, transform.position.y + 0.5f)), Vector2.left * 2f, 5f);
        else hit = Physics2D.Raycast((new Vector2(transform.position.x, transform.position.y + 0.5f)), Vector2.right *2f, 5f);
        //Debug.Log(hit.collider.name);
        if (hit.collider != null) {
			if (hit.collider.gameObject.tag == "Enemy" ||hit.collider.gameObject.tag == "Boss") {
                //Debug.Log("attacking enemy");
				hit.collider.GetComponent<Enemy> ().GetDamage (damageValue);


			}
			else if (hit.collider.gameObject.tag == "crate") {
                //Debug.Log("crate damaging");
				hit.collider.GetComponent<Crate>().Crate_damage(damageValue);
			}
			GetComponent<AudioSource> ().Play();
            anim.SetTrigger("Attack");
			Vector3 pos = hit.collider.bounds.center;
			if (transform.localScale.x > 0) pos.x -= 0.75f;
			else pos.x += 0.75f;

			Instantiate (mHitEffect, pos, Quaternion.identity);
          

		}
		*/
//   
    }

    public void p_getDamage(int value)
    {
        anim.ResetTrigger("Run");
        anim.SetTrigger ("TakeDamage");
        heartbeat();
        value = value - mPlayerStats.mStatus.defense;
        if (value <= 0) value = 1;


        playerHp -= value;
        if (playerHp <= 0.0f) 
        {
            DeadScene.SetActive(true);
			alive = false;
			anim.SetBool("Death",true);
        }
        StartCoroutine(GetDamageEffect());
        healthImage.fillAmount = (float)playerHp / 100;
    }
    public void heartbeat()
    {
		
        if (playerHp >= 70 && heartbeattype != 1) {
            heartbeattype = 1;
            healthImage.GetComponent<Tweener> ().Play ("heartbeat");
            healthBackground.GetComponent<Tweener> ().Play ("heartbeat");
            healthImage.GetComponent<Tweener> ().Stop ("heartbeat1");
            healthBackground.GetComponent<Tweener> ().Stop ("heartbeat1");
            healthImage.GetComponent<Tweener> ().Stop ("heartbeat2");
            healthBackground.GetComponent<Tweener> ().Stop ("heartbeat2");

        } else if (playerHp >= 40 && playerHp < 70 && heartbeattype != 2) {
            heartbeattype = 2;
            healthImage.GetComponent<Tweener> ().Play ("heartbeat1");
            healthBackground.GetComponent<Tweener> ().Play ("heartbeat1");
            healthImage.GetComponent<Tweener> ().Stop ("heartbeat");
            healthBackground.GetComponent<Tweener> ().Stop ("heartbeat");
            healthImage.GetComponent<Tweener> ().Stop ("heartbeat2");
            healthBackground.GetComponent<Tweener> ().Stop ("heartbeat2");
        } else if (playerHp > 0 && playerHp < 40 && heartbeattype != 3) {
            heartbeattype = 3;
            healthImage.GetComponent<Tweener> ().Play ("heartbeat2");
            healthBackground.GetComponent<Tweener> ().Play ("heartbeat2");
            healthImage.GetComponent<Tweener> ().Stop ("heartbeat1");
            healthBackground.GetComponent<Tweener> ().Stop ("heartbeat1");
            healthImage.GetComponent<Tweener> ().Stop ("heartbeat");
            healthBackground.GetComponent<Tweener> ().Stop ("heartbeat");
        } else {
			healthImage.GetComponent<Tweener> ().Stop ("heartbeat2");
			healthBackground.GetComponent<Tweener> ().Stop ("heartbeat2");
			healthImage.GetComponent<Tweener> ().Stop ("heartbeat1");
			healthBackground.GetComponent<Tweener> ().Stop ("heartbeat1");
			healthImage.GetComponent<Tweener> ().Stop ("heartbeat");
			healthBackground.GetComponent<Tweener> ().Stop ("heartbeat");
			
        }
    }
}
