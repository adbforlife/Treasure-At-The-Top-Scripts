using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour {

    public GameObject finalShieldEffect;
    public GameObject finalBubbleEffect;

    public GameObject redSlime;
    public GameObject redBat;
    public GameObject whiteGhost;
    public GameObject skeleton;
    public GameObject swordMan;
    public GameObject giant;

    public GameObject finalChest;
    public GameObject finalKey;

    private List<Transform> teleportPositions = new List<Transform>();

    public GameObject bossShield;
    public GameObject bossPresenceEffect;
    private GameObject tempBossShield;
    private GameObject tempPresenceEffect;
    private GameObject wand;
    GameObject Rings;
    private Animator anim;
    private float waitPeriod = 0.3f;
    private bool initiated;
    private bool dead;
    private bool isInvincible;
    private int tolerance = 3;
    private int tempDamages = 0;
    private int currentIntPos = 0;
    private bool ringDead = false;
    private bool firstTime = true;
    private bool gotKey = false;
    private SceneController scene;
    private Player player;

	// Use this for initialization
	void Start () {
        //finalChest.SetActive(false);
        anim = GetComponent<Animator>();
        Rings = GameObject.FindGameObjectWithTag("Rings");
        for (int i = 0; i < Rings.transform.childCount; i++)
        {
            teleportPositions.Add(Rings.transform.GetChild(i));
        }
        //FOR TESTING
        /*foreach (Transform thing in teleportPositions)
        {
            teleportPositions.Remove(thing);
        }
        teleportPositions.Add(Rings.transform.GetChild(0));*/



        tempBossShield = Instantiate(bossShield, transform);
        tempBossShield.transform.localPosition = new Vector3(0, 0.7f, 0.5f);
        tempBossShield.transform.localRotation = Quaternion.Euler(0, -90, 0);
        scene = Singleton_Service.GetSingleton<SceneController>();
        /*tempPresenceEffect = Instantiate(bossPresenceEffect, transform);
        tempPresenceEffect.transform.localPosition = Vector3.zero;*/
	}

    public void Initiate()
    {
        //finalChest.SetActive(true);
        initiated = true;
        StartTeleporting();
    }

    public void StartTeleporting()
    {
        StopCoroutine("SpawnMore");
        tempBossShield.SetActive(false);
        tempPresenceEffect = Instantiate(bossPresenceEffect, transform);
        tempPresenceEffect.transform.localPosition = Vector3.zero;
        if (firstTime)
        {
            anim.Play("victory");
            firstTime = false;
        }
        else
        {
            anim.Play("GetHit");
        }
    }

    public void Nothing() { }

    public void StartSkill()
    {
        anim.Play("victory");
    }
    
    public void EndTeleporting()
    {
        isInvincible = false;
        int tempIntPos = 0;
        if (teleportPositions.Count != 1)
        {
            if (ringDead)
            {
                Debug.Log(teleportPositions.Count);
                Transform currentRing = teleportPositions[currentIntPos];
                Debug.Log(teleportPositions.Remove(currentRing));
                currentRing.gameObject.SetActive(false);
                ringDead = false;
                tempIntPos = Random.Range(0, teleportPositions.Count);
            }
            else
            {

                while ((tempIntPos = Random.Range(0, teleportPositions.Count)) == currentIntPos)
                {
                    Nothing();
                }
            }
            currentIntPos = tempIntPos;
            Vector3 tempPos = teleportPositions[tempIntPos].position;
            transform.position = tempPos;
            tempBossShield.transform.localPosition = new Vector3(0, 0.7f, 0.5f);
            tempBossShield.transform.localRotation = Quaternion.Euler(0, -90, 0);
            tempBossShield.SetActive(true);
            Destroy(tempPresenceEffect);
            anim.Play("attack02");
        }
        else
        {
            /*foreach (Transform ring in teleportPositions)
            {
                Destroy(ring.gameObject);
            }*/
            dead = true;
            transform.localPosition = new Vector3(0f, 0.11f, 15f);
            anim.Play("idle");
            
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<Enemy_State_Pattern>().currentState.ToDieState();
            }

            wand = Singleton_Service.GetSingleton<RightHand>().GetSword();
            StartCoroutine("GetRidOfWand");
        }
    }

    IEnumerator GetRidOfWand()
    {
        yield return new WaitForSeconds(2f);
        anim.Play("attack01");
        yield return new WaitForSeconds(1f);
        wand.transform.parent = null;
        wand.AddComponent<Rigidbody>();
        Destroy(wand, 2);
        yield return new WaitForSeconds(0.5f);
        player = Singleton_Service.GetSingleton<Player>();
        Singleton_Service.GetSingleton<LeftHand>().GetShield().GetComponent<Collider>().isTrigger = true;
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            player.transform.position += new Vector3(0f, 0f, 0.02f);
        }
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            player.transform.position += new Vector3(0f, 0f, 0.03f);
        }
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            player.transform.position += new Vector3(0f, 0f, 0.04f);
        }
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            player.transform.position += new Vector3(0f, 0f, 0.06f);
        }
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            player.transform.position += new Vector3(0f, 0f, 0.09f);
        }
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            player.transform.position += new Vector3(0f, 0f, 0.05f);
        }
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            player.transform.position += new Vector3(0f, 0f, 0.03f);
        }
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            player.transform.position += new Vector3(0f, 0f, 0.01f);
        }
    }

    public void PauseAnimation()
    {
        anim.speed = 0;
    }

    public void SpawnRandomEnemy()
    {
        int randomInt = Random.Range(0, 100);
        if (randomInt < 8)
        {
            scene.GetCurrentLevelObject().GetComponent<Level>().AddEnemy(redSlime, new Vector3(Random.Range(-3f, 3f), 0.1f, Random.Range(6f, 12f)));
        }   else if (randomInt < 16)
        {
            scene.GetCurrentLevelObject().GetComponent<Level>().AddEnemy(redBat, new Vector3(Random.Range(-3f, 3f), 0.1f, Random.Range(6f, 12f)));
        }   else if (randomInt < 24)
        {
            scene.GetCurrentLevelObject().GetComponent<Level>().AddEnemy(whiteGhost, new Vector3(Random.Range(-3f, 3f), 0.1f, Random.Range(6f, 12f)));
        }   else if (randomInt < 54)
        {
            scene.GetCurrentLevelObject().GetComponent<Level>().AddEnemy(skeleton, new Vector3(Random.Range(-3f, 3f), 0.1f, Random.Range(6f, 12f)));
        }   else if (randomInt < 84)
        {
            scene.GetCurrentLevelObject().GetComponent<Level>().AddEnemy(swordMan, new Vector3(Random.Range(-3f, 3f), 0.1f, Random.Range(6f, 12f)));
        }
        else
        {
            scene.GetCurrentLevelObject().GetComponent<Level>().AddEnemy(giant, new Vector3(Random.Range(-3f, 3f), 0.1f, Random.Range(6f, 12f)));
        }
        scene.GetCurrentLevelObject().GetComponent<Level>().Invoke("StartLevel", 1f);
    }

    public void BecomeIdle()
    {
        StartCoroutine("Chilling");
        StartCoroutine("SpawnMore");
    }

    IEnumerator SpawnMore()
    {
        yield return new WaitForSeconds(10f);
        anim.Play("attack02");
    }

    IEnumerator Chilling()
    {
        anim.Play("idle");
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            if (other.tag == "EnergyBall" && initiated)
            {
                if (tempDamages <= tolerance && !isInvincible)
                {
                    tempDamages++;
                }
                else
                {
                    if (!ringDead)
                    {
                        if (teleportPositions[currentIntPos].GetComponent<BossRing>().GetHealth() == 0)
                        {
                            /*Transform currentRing = teleportPositions[currentIntPos];
                            Debug.Log(teleportPositions.Remove(currentRing));
                            Destroy(currentRing.gameObject);*/
                            ringDead = true;
                        }
                        else
                        {
                            teleportPositions[currentIntPos].GetComponent<BossRing>().DecrementHealth();
                        }
                    }
                    if (!isInvincible)
                    {
                        tempDamages = 0;
                        isInvincible = true;
                        StartTeleporting();
                    }
                    else
                    {
                        Nothing();
                    }
                }
                other.GetComponent<EnergyBall>().Explode();
            }
        }
        else
        {
            Nothing();
        }

        if (!gotKey && other.tag == "Shield")
        {
            anim.speed = 1;
            anim.Play("die");
            Invoke("DropKey", 2f);
            gotKey = true;
        }
    }

    public void BossDie()
    {
        finalBubbleEffect.SetActive(false);
        finalShieldEffect.SetActive(false);
        Singleton_Service.GetSingleton<GameMusic>().PlayCreditsMusic();
        gameObject.SetActive(false);
    }

    public void DropKey()
    {
        GameObject key = Instantiate(finalKey, transform.parent);
        key.transform.position = transform.position;
    }
}