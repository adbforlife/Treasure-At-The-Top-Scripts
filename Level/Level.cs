using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public GameObject crate;
    public GameObject spawnEffect;

    private SceneController scene;
    private Player player;
    private RightHand rightHand;
    private GameMusic music;

    private int numEnemies;
    [HideInInspector] public bool hasStarted;
    private bool isReady;
    private int numCrates;
    
    [HideInInspector] public List<GameObject> enemies = new List<GameObject>();

    private Vector3 spawnPosition;

	// Use this for initialization
	void Start () {
        scene = Singleton_Service.GetSingleton<SceneController>();
        player = Singleton_Service.GetSingleton<Player>();
        music = Singleton_Service.GetSingleton<GameMusic>();
	}

    public void StartLevel()    {
        hasStarted = true;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))    {
            enemy.GetComponent<Enemy_State_Pattern>().currentState.ToChaseState();
        }
    }

    private void Update()
    {
        if (!hasStarted && isReady && numCrates == 0/*rightHand.gameObject.transform.position.y > player.transform.position.y + 1.8f*/)    {
            hasStarted = true;
            rightHand.StartCoroutine(rightHand.LongVibration(0.5f, 1));
            StartLevel();
        }
    }

    public void AddEnemy(GameObject thing, Vector3 position)
    {
        numEnemies++;
        spawnPosition = position;
        Instantiate(spawnEffect, transform).transform.localPosition = spawnPosition;
        StartCoroutine(SpawnEnemy(thing, spawnPosition));
    }

    public void AddCrate(Vector3 position)
    {
        numCrates++;
        Instantiate(crate, transform).transform.localPosition = position;
    }

    public void BeReady()
    {
        StartCoroutine("GetReady");
    }

    IEnumerator GetReady()  {
        yield return new WaitForSeconds(2f);
        rightHand = Singleton_Service.GetSingleton<RightHand>();
        isReady = true;
    }

    IEnumerator EnemyMoveUp(GameObject enemy)
    {
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.02f);
            enemy.transform.position += new Vector3(0, 0.02f, 0);
        }
    }

    IEnumerator SpawnEnemy(GameObject thing, Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject enemy = Instantiate(thing, Vector3.zero, Quaternion.Euler(0, 180, 0), transform);
        enemy.transform.localPosition = spawnPosition - new Vector3(0, 1, 0);
        enemy.transform.LookAt(new Vector3(player.transform.position.x, enemy.transform.position.y, player.transform.position.z));
        enemies.Add(enemy);
        StartCoroutine(EnemyMoveUp(enemy));

    }

    public void RemoveEnemy(GameObject thing)
    {
        numEnemies--;
        enemies.Remove(thing);
        if (numEnemies == 0)
        {
            if (scene.GetCurrentLevel() == 14)
            {
                transform.Find("FinalWall").gameObject.GetComponent<FinalWall>().StartOpening();
                music.PlayFinalBossMusic();
            }
            else
            {
                scene.SetIsTransitioning(true, true);
            }
        }
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

    public GameObject GetEnemyScroll()  {
        return transform.Find("EnemyScroll").gameObject;
    }

    public void SetNumCrates(int num)  {
        numCrates = num;
    }

    public void DecreaseCrates(int num) {
        numCrates -= num;
    }
}