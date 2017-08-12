using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_State_Pattern : MonoBehaviour {

    [SerializeField] private int health;
    [SerializeField] private int attack;
    [SerializeField] private int defense;

    private string monsterName;

    private Animator anim;

    private SceneController scene;
    private Player player;
    private RightHand rightHand;
    private LeftHand leftHand;
    private GameMusic music;
    private int playerAttack;
    private int playerDefense;
    private bool hasInitiated;

    [HideInInspector] public float distanceThreshold;
    public float moveSpeed;
    [HideInInspector] public float waitTime;
    private float distance;
    private float monsterHeight;
    private int tolerance;
    [HideInInspector] public bool canHaveAnimator;

    private int tempNumDamages;
    [HideInInspector] public bool isInvincible;

    private AudioSource swordSource;
    private AudioSource shieldSource;
    private AudioSource playerHitSource;
    private SwordAudio swordAud;
    private ShieldAudio shieldAud;
    private PlayerHitAudio playerHitAud;

    [HideInInspector] public string animIdleString;
    [HideInInspector] public string animMoveString;
    [HideInInspector] public string animAttackString;
    [HideInInspector] public string animHurtString;
    [HideInInspector] public string animDieString;
    [HideInInspector] public string animSpecialString;

    public static Enemy_State_Pattern instance;

    public enum EnemyType {GreenSlime, RedSlime, GreenBat, RedBat, GreenRabbit, RedRabbit, GreenGhost, WhiteGhost, MythDoc, MythMinion, Skeleton, Barbarian, Swordman, Giant, Mage}
    public EnemyType enemyType;

	[HideInInspector] public Enemy_State currentState;
	[HideInInspector] public Idle_State idleState;
	[HideInInspector] public Chase_State chaseState;
    [HideInInspector] public Attack_State attackState;
    [HideInInspector] public Hurt_State hurtState;
    [HideInInspector] public Die_State dieState;
    [HideInInspector] public Special_State specialState;


    //Parameters for specific enemies
    [HideInInspector] public int mythDocMaxMinions;

    void Awake()	{
		instance = this;
		idleState = new Idle_State (this);
		chaseState = new Chase_State (this);
        attackState = new Attack_State(this);
        hurtState = new Hurt_State(this);
        dieState = new Die_State(this);
        specialState = new Special_State(this);
	}

	// Use this for initialization
	void Start () {
        waitTime = 0.02f;

        scene = Singleton_Service.GetSingleton<SceneController>();
        player = Singleton_Service.GetSingleton<Player>();
        rightHand = Singleton_Service.GetSingleton<RightHand>();
        leftHand = Singleton_Service.GetSingleton<LeftHand>();
        music = Singleton_Service.GetSingleton<GameMusic>();

        swordSource = rightHand.GetSword().GetComponent<AudioSource>();
        swordAud = rightHand.GetSword().GetComponent<SwordAudio>();
        shieldSource = leftHand.GetShield().GetComponent<AudioSource>();
        shieldAud = leftHand.GetShield().GetComponent<ShieldAudio>();
        playerHitSource = player.GetComponent<AudioSource>();
        playerHitAud = player.GetComponent<PlayerHitAudio>();


        playerAttack = player.GetAttack();
        playerDefense = player.GetDefense();

        anim = GetComponent<Animator>();



        UpdateEnemyUI();

        EnemyInit();
        StartCoroutine("Updating");
        currentState = idleState;
        currentState.StartState();
    }

    
	// Update is called once per frame
	IEnumerator Updating () {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            //Debug.Log(currentState);
            currentState.UpdateState();
        }
	}

    private void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0f, monsterHeight, 0f), player.transform.position - transform.position, Color.green);
    }


    public void PerformAttack()
    {
        tempNumDamages = 0;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0f, monsterHeight, 0f), player.transform.position - transform.position, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Sword"))
            {
                scene.PlayAudio(playerHitSource, playerHitAud.playerHitAudio[Random.Range(0, playerHitAud.playerHitAudio.Length)]);
            }   else if (hit.transform.gameObject.CompareTag("Shield"))
            {
                leftHand.StartCoroutine(leftHand.LongVibration(0.05f, 1));
                player.Damaged(attack);
                scene.PlayAudio(shieldSource, shieldAud.shieldAudio[Random.Range(0, shieldAud.shieldAudio.Length)]);
            }   else if (hit.transform.gameObject.CompareTag("Player"))
            {
                player.Damaged(attack + player.GetDefense());
                scene.PlayAudio(playerHitSource, playerHitAud.playerHitAudio[Random.Range(0, playerHitAud.playerHitAudio.Length)]);
            }
        }
    }

    public void ContinueAttacking()
    {
        if (health > 0)
        {
            currentState.ToAttackState();
        }
        else
        {
            currentState.ToDieState();
        }
    }

    public void UpdateEnemyUI()
    {
        /*attackText.text = attack.ToString();
        defenseText.text = defense.ToString();
        healthText.text = health.ToString();*/
    }

    public void Damaged()
    {
        tempNumDamages++;
        if (tempNumDamages <= tolerance)
        {
            currentState.ToHurtState();
        }   else
        {
            if (currentState != specialState && animSpecialString != null)
            {
                switch(enemyType)
                {
                    case EnemyType.GreenGhost:
                    case EnemyType.WhiteGhost:
                    case EnemyType.GreenBat:
                    case EnemyType.RedBat:
                        currentState.ToSpecialState();
                        currentState.StartState();
                        break;
                    default:
                        break;
                }
            }
        }
        int damage = playerAttack - defense;
        if (damage < 1) { damage = 1; }
        scene.PlayAudio(swordSource, swordAud.swordAudio[Random.Range(0, swordAud.swordAudio.Length)]);
        health -= damage;
        if (health < 0) {
            health = 0;
        }
    }

    public void WaveDamaged()
    {
        tempNumDamages++;
        if (tempNumDamages <= tolerance)
        {
            currentState.ToHurtState();
        }
        else
        {
            if (currentState != specialState && animSpecialString != null)
            {
                switch (enemyType)
                {
                    case EnemyType.GreenGhost:
                    case EnemyType.WhiteGhost:
                    case EnemyType.GreenBat:
                    case EnemyType.RedBat:
                        currentState.ToSpecialState();
                        currentState.StartState();
                        break;
                    default:
                        break;
                }
            }
        }

        int damage = 100 - defense;
        if (damage < 1) { damage = 1; }
        health -= damage;
        if (health < 0)
        {
            health = 0;
            currentState.ToDieState();
        }
        scene.GetCurrentLevelObject().GetComponent<Level>().GetEnemyScroll().GetComponent<EnemyScroll>().UpdateText(monsterName, health, attack, defense);
    }

    public void WandDamaged()
    {
        if (enemyType == EnemyType.Skeleton)
        {
            animHurtString = "Knockback";
        }
        tempNumDamages++;
        if (tempNumDamages <= tolerance)
        {
            currentState.ToHurtState();
        }

        if (enemyType == EnemyType.Skeleton)
        {
            animHurtString = "Damage";
        }

        int damage = playerAttack - defense;
        if (damage < 1) { damage = 1; }
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!scene.GetIsTransitioning() && currentState != dieState && !isInvincible)
        {
            if (other.tag == "Sword")
            {
                Damaged();
                rightHand.StartCoroutine(rightHand.LongVibration(0.05f, 1));
                scene.GetCurrentLevelObject().GetComponent<Level>().GetEnemyScroll().GetComponent<EnemyScroll>().UpdateText(monsterName, health, attack, defense);
                if (health <= 0)
                {
                    currentState.ToDieState();
                }
            } else if (other.tag == "EnergyBall" && other.GetComponent<EnergyBall>().GetIsShot())
            {
                other.GetComponent<EnergyBall>().Explode();
                WandDamaged();
                scene.GetCurrentLevelObject().GetComponent<Level>().GetEnemyScroll().GetComponent<EnemyScroll>().UpdateText(monsterName, health, attack, defense);
                if (currentState == idleState)
                {
                    currentState.ToChaseState();
                }
                if (health <= 0)
                {
                    currentState.ToDieState();
                }
            }
        }
    }

    public IEnumerator MythDocSkill()
    {
        while (currentState != dieState)
        {
            yield return new WaitForSeconds(5f);
            if (scene.GetCurrentLevelObject().GetComponent<Level>().enemies.Count <= mythDocMaxMinions)
            {
                currentState.ToSpecialState();
                currentState.StartState();
            }
        }
    }

    public void SetHasInitiated(bool value)
    {
        hasInitiated = value;
    }

    public void Dead()
    {
        switch(enemyType)
        {
            case EnemyType.MythDoc:
                music.PlayHighLevelMusic();
                scene.GetCurrentLevelObject().GetComponent<Level>().RemoveEnemy(transform.parent.gameObject);
                break;
            case EnemyType.MythMinion:
            case EnemyType.Skeleton:
                scene.GetCurrentLevelObject().GetComponent<Level>().RemoveEnemy(transform.parent.gameObject);
                break;
            default:
                scene.GetCurrentLevelObject().GetComponent<Level>().RemoveEnemy(gameObject);
                break;
        }
        Destroy(gameObject);
    }

    public int GetTempNumDamages()
    {
        return tempNumDamages;
    }

    public int GetTolerance()
    {
        return tolerance;
    }

    public void SetTolerance(int num)
    {
        tolerance = num;
    }

    public void SetMoveSpeed(float num)
    {
        moveSpeed = num;
    }

    public void SetThreshold(float num)
    {
        distanceThreshold = num;
    }

    public void ResetTempNumDamages()
    {
        tempNumDamages = 0;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetAttack()
    {
        return attack;
    }

    public int GetDefense()
    {
        return defense;
    }

    void EnemyInit()
    {
        switch (enemyType)
        {
            case EnemyType.GreenSlime:
                monsterName = "Green Slime";
                health = 70;
                attack = 18;
                defense = 2;
                distanceThreshold = 1.2f;
                moveSpeed = 0.3f;
                monsterHeight = 1f;
                animIdleString = "slime_idle";
                animMoveString = "slime_move";
                animAttackString = "slime_attack";
                animHurtString = null;
                animDieString = "slime_die";
                animSpecialString = null;
                tolerance = 3;
                canHaveAnimator = true;
                break;
            case EnemyType.RedSlime:
                monsterName = "Red Slime";
                health = 350;
                attack = 50;
                defense = 10;
                distanceThreshold = 1.2f;
                moveSpeed = 0.3f;
                monsterHeight = 1f;
                animIdleString = "slime_idle";
                animMoveString = "slime_move";
                animAttackString = "slime_attack";
                animHurtString = null;
                animDieString = "slime_die";
                animSpecialString = null;
                tolerance = 3;
                canHaveAnimator = true;
                break;
            case EnemyType.GreenBat:
                monsterName = "Green Bat";
                health = 100;
                attack = 25;
                defense = 5;
                distanceThreshold = 1.2f;
                moveSpeed = 0.5f;
                monsterHeight = 1.8f;
                animIdleString = "bat_idle";
                animMoveString = "bat_move";
                animAttackString = "bat_attack";
                animHurtString = "bat_damage";
                animDieString = "bat_die";
                animSpecialString = "bat_special";
                tolerance = 3;
                canHaveAnimator = true;
                break;
            case EnemyType.RedBat:
                monsterName = "Red Bat";
                health = 200;
                attack = 60;
                defense = 15;
                distanceThreshold = 1.2f;
                moveSpeed = 0.5f;
                monsterHeight = 1.8f;
                animIdleString = "bat_idle";
                animMoveString = "bat_move";
                animAttackString = "bat_attack";
                animHurtString = "bat_damage";
                animDieString = "bat_die";
                animSpecialString = "bat_special";
                tolerance = 3;
                canHaveAnimator = true;
                break;
            case EnemyType.GreenGhost:
                monsterName = "Green Ghost";
                health = 170;
                attack = 50;
                defense = 5;
                distanceThreshold = 1.2f;
                moveSpeed = 1.2f;
                monsterHeight = 1f;
                animIdleString = "ghost_idle";
                animMoveString = "ghost_move";
                animAttackString = "ghost_attack";
                animHurtString = "ghost_damage";
                animDieString = "ghost_die";
                animSpecialString = "ghost_special";
                tolerance = 5;
                canHaveAnimator = true;
                break;
            case EnemyType.WhiteGhost:
                monsterName = "White Ghost";
                health = 300;
                attack = 80;
                defense = 15;
                distanceThreshold = 1.2f;
                moveSpeed = 1.2f;
                monsterHeight = 1f;
                animIdleString = "ghost_idle";
                animMoveString = "ghost_move";
                animAttackString = "ghost_attack";
                animHurtString = "ghost_damage";
                animDieString = "ghost_die";
                animSpecialString = "ghost_special";
                tolerance = 5;
                canHaveAnimator = true;
                break;
            case EnemyType.GreenRabbit:
                monsterName = "Green Rabbit";
                health = 200;
                attack = 60;
                defense = 8;
                distanceThreshold = 2.1f;
                moveSpeed = 1f;
                monsterHeight = 2f;
                animIdleString = "rabbit_idle";
                animMoveString = "rabbit_move";
                animAttackString = "rabbit_attack";
                animHurtString = "rabbit_damage";
                animDieString = "rabbit_die";
                animSpecialString = null;
                tolerance = 7;
                canHaveAnimator = true;
                break;
            case EnemyType.RedRabbit:
                monsterName = "Red Rabbit";
                health = 300;
                attack = 100;
                defense = 22;
                distanceThreshold = 2.1f;
                moveSpeed = 1f;
                monsterHeight = 2f;
                animIdleString = "rabbit_idle";
                animMoveString = "rabbit_move";
                animAttackString = "rabbit_attack";
                animHurtString = "rabbit_damage";
                animDieString = "rabbit_die";
                animSpecialString = null;
                tolerance = 7;
                canHaveAnimator = true;
                break;
            case EnemyType.MythDoc:
                monsterName = "Myth Doc";
                health = 1000;
                attack = 90;
                defense = 9;
                distanceThreshold = 2.2f;
                moveSpeed = 0.2f;
                monsterHeight = 2f;
                animIdleString = "mythDoc_idle";
                animMoveString = "mythDoc_move";
                animAttackString = "mythDoc_attack";
                animHurtString = "mythDoc_damage";
                animDieString = "mythDoc_die";
                animSpecialString = "mythDoc_special";
                tolerance = 3;
                canHaveAnimator = false;
                mythDocMaxMinions = 4;
                break;
            case EnemyType.MythMinion:
                monsterName = "Myth Minion";
                health = 100;
                attack = 30;
                defense = 8;
                distanceThreshold = 0.7f;
                moveSpeed = 1f;
                monsterHeight = 1.3f;
                animIdleString = "Idle";
                animMoveString = "Run";
                animAttackString = "Attack";
                animHurtString = "Get Hit";
                animDieString = "Dead";
                animSpecialString = null;
                tolerance = 5;
                canHaveAnimator = true;
                break;
            case EnemyType.Skeleton:
				monsterName = "Skeleton";
				health = 300;
				attack = 100;
				defense = 35;
				distanceThreshold = 9f;
				moveSpeed = 0.5f;
				monsterHeight = 1.4f;
				animIdleString = "Idle";
				animMoveString = "Run";
				animAttackString = "Attack";
				animHurtString = "Damage";
				animDieString = "Death";
				animSpecialString = "Skill";
				tolerance = 5;
				canHaveAnimator = true;
                break;
            case EnemyType.Barbarian:
				monsterName = "Barbarian";
				health = 600;
				attack = 160;
				defense = 40;
				distanceThreshold = 3f;
				moveSpeed = 1f;
				monsterHeight = 1.5f;
				animIdleString = "free";
				animMoveString = "walk";
				animAttackString = "skill";
				animHurtString = "hit";
				animDieString = "death";
				animSpecialString = null;
				tolerance = 5;
				canHaveAnimator = false;
				break;
            case EnemyType.Swordman:
				monsterName = "Swordman";
				health = 600;
				attack = 80;
				defense = 80;
				distanceThreshold = 3f;
				moveSpeed = 1f;
				monsterHeight = 1.2f;
				animIdleString = "Idle";
				animMoveString = "Walk";
				animAttackString = "Attack01";
				animHurtString = "GetHit";
				animDieString = "Death";
				animSpecialString = null;
				tolerance = 5;
				canHaveAnimator = true;
				break;
			case EnemyType.Giant:
				monsterName = "Giant";
				health = 800;
				attack = 200;
				defense = 80;
				distanceThreshold = 3.6f;
				moveSpeed = 0.4f;
				monsterHeight = 2f;
				animIdleString = "Idle";
				animMoveString = "Walk";
				animAttackString = "Attack02";
				animHurtString = "GetHit";
				animDieString = "Die";
				animSpecialString = null;
				tolerance = 5;
				canHaveAnimator = true;
				break;
			case EnemyType.Mage:
				monsterName = "Mage";
				health = 4000;
				attack = 150;
				defense = 0;
				distanceThreshold = 3.5f;
				moveSpeed = 1f;
				monsterHeight = 1f;
				animIdleString = "idle";
				animMoveString = "walk";
				animAttackString = "attack01";
				animHurtString = "GetHit";
				animDieString = "die";
				animSpecialString = "attack02";
				tolerance = 5;
				canHaveAnimator = true;
				break;
            default:
                break;
        }
    }
}