using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public GameObject greenSlime;
    public GameObject redSlime;
    public GameObject greenBat;
    public GameObject redBat;
    public GameObject greenRabbit;
    public GameObject redRabbit;
    public GameObject greenGhost;
    public GameObject whiteGhost;
    public GameObject mythDoc;
    public GameObject skeleton;
    public GameObject barbarian;
    public GameObject swordMan;
    public GameObject giant;
    public GameObject mage;

    public GameObject playerScrollPos;
    public GameObject currentPlayerScroll;

    public GameObject movableTile;
    public GameObject level;
    public GameObject highLevel;
    public GameObject topLevel;
    public GameObject level1;
    public Transform levels;

    public float moveUpSpeed;

    public GameObject healthGem;
    public GameObject attackGem;
    public GameObject defenseGem;
    private GameObject tempHGem;
    private GameObject tempAGem;
    private GameObject tempDGem;

    private int maxLevels = 14;
    private int floorHeight = 4;

    private Player player;
    private GameMusic music;

    private bool isTransitioning;
    private bool ceilingHasMoved;
    private int currentLevel;
    private GameObject currentMovableTile;
    private List<GameObject> buildingLevels = new List<GameObject>();
    private int[] emptyLevels = { 6 };




    public GameObject coolEffect;
    public GameObject tempChest;
    private bool opened;
    public AudioClip clip;


    //temporary
    public GameObject treasure;


    void OnEnable()
    {
        Singleton_Service.RegisterSingletonInstance(this);
    }

    void OnDisable()
    {
        Singleton_Service.UnregisterSingletonInstance(this);
    }

    // Use this for initialization
    void Start () {
        
        //temporary testing
        PlayerPrefs.SetInt("currentLevel", 14);


        player = Singleton_Service.GetSingleton<Player>();
        music = Singleton_Service.GetSingleton<GameMusic>();

        buildingLevels.Add(level1);
		for (int i = 1; i < 10; i++)
        {
            buildingLevels.Add(Instantiate(level, new Vector3(0, i * floorHeight, 0), Quaternion.identity, levels));
        }
        for (int i = 11; i <= 13; i++)
        {
            buildingLevels.Add(Instantiate(highLevel, new Vector3(0, (i - 1) * floorHeight, 0), Quaternion.identity, levels));
        }
        buildingLevels.Add(Instantiate(topLevel, new Vector3(0, 52, 0), Quaternion.identity, levels));
        foreach (int i in emptyLevels)
        {
            buildingLevels[i - 1].tag = "EmptyLevel";
            buildingLevels[i - 1].transform.Find("EnemyScrollPos").gameObject.SetActive(false);
            buildingLevels[i - 1].transform.Find("EnemyScroll").gameObject.SetActive(false);
            buildingLevels[i - 2].transform.Find("Ceiling").gameObject.SetActive(false);
        }
        currentMovableTile = Instantiate(movableTile, new Vector3(0, 0, -3f), Quaternion.identity);
        PutCrates();

        music.PlayStartingMusic();
        GetComponent<ScreenFader>().fadeIn = false;
        StartCoroutine(GetComponent<ScreenFader>().DoFade());
        StartCoroutine("DelayFadeIn");
        //currentPlayerScroll = Instantiate(playerScrollPos, Vector3.zero, Quaternion.identity);
    }

    IEnumerator DelayFadeIn()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<ScreenFader>().fadeTime = 2f;
        GetComponent<ScreenFader>().fadeIn = true;
        StartCoroutine(GetComponent<ScreenFader>().DoFade());
    }

    public void PutCrates() {
        for (int i = 1; i <= 15; i++)
        {
            switch (i)
            {
                case 1:
                    buildingLevels[0].GetComponent<Level>().SetNumCrates(1);
                    break;
                case 2:
                    buildingLevels[1].GetComponent<Level>().AddCrate(new Vector3(1f, 0.55f, -1f));
                    break;
                case 3:
                    buildingLevels[2].GetComponent<Level>().AddCrate(new Vector3(0.5f, 0.55f, -1f));
                    buildingLevels[2].GetComponent<Level>().AddCrate(new Vector3(-0.5f, 0.55f, -1f));
                    break;
                case 7:
                    buildingLevels[6].GetComponent<Level>().AddCrate(new Vector3(0.5f, 0.55f, -1f));
                    buildingLevels[6].GetComponent<Level>().AddCrate(new Vector3(-0.5f, 0.55f, -1f));
                    buildingLevels[6].GetComponent<Level>().AddCrate(new Vector3(0f, 1.51f, -1f));
                    break;
                case 8:
                    buildingLevels[7].GetComponent<Level>().AddCrate(new Vector3(0f, 0.55f, -1f));
                    buildingLevels[7].GetComponent<Level>().AddCrate(new Vector3(0f, 1.51f, -1f));
                    buildingLevels[7].GetComponent<Level>().AddCrate(new Vector3(0f, 2.48f, -1f));
                    buildingLevels[7].GetComponent<Level>().AddCrate(new Vector3(0f, 3.46f, -1f));
                    break;
                case 9:
                    buildingLevels[8].GetComponent<Level>().AddCrate(new Vector3(1f, 0.55f, -1f));
                    buildingLevels[8].GetComponent<Level>().AddCrate(new Vector3(1f, 1.51f, -1f));
                    buildingLevels[8].GetComponent<Level>().AddCrate(new Vector3(-1f, 0.55f, -1f));
                    buildingLevels[8].GetComponent<Level>().AddCrate(new Vector3(-1f, 1.51f, -1f));
                    break;
                case 11:
                    buildingLevels[10].GetComponent<Level>().AddCrate(new Vector3(-1f, 0.55f, -1f));
                    buildingLevels[10].GetComponent<Level>().AddCrate(new Vector3(0f, 0.55f, -1f));
                    buildingLevels[10].GetComponent<Level>().AddCrate(new Vector3(1f, 0.55f, -1f));
                    buildingLevels[10].GetComponent<Level>().AddCrate(new Vector3(-1f, 1.51f, -1f));
                    buildingLevels[10].GetComponent<Level>().AddCrate(new Vector3(1f, 1.51f, -1f));
                    break;
                case 12:
                    buildingLevels[11].GetComponent<Level>().AddCrate(new Vector3(-1f, 0.55f, -1f));
                    buildingLevels[11].GetComponent<Level>().AddCrate(new Vector3(0f, 0.55f, -1f));
                    buildingLevels[11].GetComponent<Level>().AddCrate(new Vector3(1f, 0.55f, -1f));
                    buildingLevels[11].GetComponent<Level>().AddCrate(new Vector3(-0.5f, 1.51f, -1f));
                    buildingLevels[11].GetComponent<Level>().AddCrate(new Vector3(0.5f, 1.51f, -1f));
                    buildingLevels[11].GetComponent<Level>().AddCrate(new Vector3(0f, 2.48f, -1f));
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator Transition()
    {
        if (!ceilingHasMoved)
            yield return new WaitUntil(() => ceilingHasMoved);
        while (isTransitioning && ceilingHasMoved)
        {
            yield return new WaitForSeconds(0.01f);
            GoUp();
            if (buildingLevels[currentLevel].tag == "EmptyLevel")
            {
                if (player.transform.position.y >= (currentLevel + 1) * floorHeight)
                {
                    isTransitioning = false;
                    ceilingHasMoved = false;
                    currentLevel += 2;
                    ChangeActiveFloors();
                    SpawnEnemies();
                    buildingLevels[currentLevel - 1].GetComponent<Level>().BeReady();
                    SaveInfo();
                }
            }
            else
            {
                if (player.transform.position.y >= currentLevel * floorHeight)
                {
                    isTransitioning = false;
                    ceilingHasMoved = false;
                    currentLevel++;
                    ChangeActiveFloors();
                    SpawnEnemies();
                    buildingLevels[currentLevel - 1].GetComponent<Level>().BeReady();
                    SaveInfo();
                }
            }
        }
    }

    public void ChangeActiveFloors()
    {
        for (int i = 0; i < maxLevels; i++)
        {
            buildingLevels[i].SetActive(false);
        }
        if (currentLevel == 1)
        {
            buildingLevels[currentLevel - 1].SetActive(true);
            buildingLevels[currentLevel].SetActive(true);
            buildingLevels[currentLevel + 1].SetActive(true);
        }   else if (currentLevel == maxLevels)
        {
            buildingLevels[currentLevel - 2].SetActive(true);
            buildingLevels[currentLevel - 1].SetActive(true);
        }   else if (currentLevel == maxLevels - 1)
        {
            buildingLevels[currentLevel - 2].SetActive(true);
            buildingLevels[currentLevel - 1].SetActive(true);
            buildingLevels[currentLevel].SetActive(true);
        }
        else
        {
            buildingLevels[currentLevel - 2].SetActive(true);
            buildingLevels[currentLevel - 1].SetActive(true);
            buildingLevels[currentLevel].SetActive(true);
            buildingLevels[currentLevel + 1].SetActive(true);
        }
    }

    public void GoUp()
    {
        currentMovableTile.transform.position += new Vector3(0, moveUpSpeed, 0);
        player.transform.position += new Vector3(0, moveUpSpeed, 0);
    }

    public void SpawnGems()
    {
        tempHGem = Instantiate(healthGem, Vector3.zero, Quaternion.identity, buildingLevels[currentLevel - 1].transform);
        tempAGem = Instantiate(attackGem, Vector3.zero, Quaternion.Euler(0, 45, 0), buildingLevels[currentLevel - 1].transform);
        tempDGem = Instantiate(defenseGem, Vector3.zero, Quaternion.Euler(0, -45, 0), buildingLevels[currentLevel - 1].transform);
        tempHGem.transform.localPosition = new Vector3(0f, 0.7f, -1.7f);
        tempAGem.transform.localPosition = new Vector3(0.6f, 0.7f, -2f);
        tempDGem.transform.localPosition = new Vector3(-0.6f, 0.7f, -2f);
    }

    public void DeactivateGems()
    {
        tempHGem.SetActive(false);
        tempAGem.SetActive(false);
        tempDGem.SetActive(false);
    }

    public void SpawnEnemies()
    {
        switch (currentLevel)
        {
            case 1:
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(greenSlime, new Vector3(Random.Range(-3f, 3f), 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().BeReady();
                break;
            case 2:
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(greenBat, new Vector3(Random.Range(-3f, 3f), 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(greenSlime, new Vector3(Random.Range(-3f, 3f), 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().BeReady();
                break;
            case 3:
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(greenGhost, new Vector3(Random.Range(-3f, 3f), 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(greenBat, new Vector3(Random.Range(-3f, 3f), 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(greenSlime, new Vector3(Random.Range(-3f, 3f), 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().BeReady();
                break;
            case 5:
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(mythDoc, new Vector3(Random.Range(-3f, 3f), 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().BeReady();
                music.PlayBossMusic();
                break;
            case 7:
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(greenRabbit, new Vector3(0f, 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(redSlime, new Vector3(-1.5f, 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(redBat, new Vector3(1.5f, 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().BeReady();
                break;
            case 8:
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(skeleton, new Vector3(0f, 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(greenRabbit, new Vector3(-2f, 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(greenRabbit, new Vector3(2f, 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().BeReady();
                break;
            case 9:
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(skeleton, new Vector3(-3f, 0.1f, 2f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(redRabbit, new Vector3(0f, 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(whiteGhost, new Vector3(-1.5f, 0.1f, 2.5f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(redBat, new Vector3(1.5f, 0.1f, 2.5f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(redSlime, new Vector3(3f, 0.1f, 2f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().BeReady();
                break;
            case 11:
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(skeleton, new Vector3(-3f, 0.1f, 8f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(skeleton, new Vector3(-1.5f, 0.1f, 9.5f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(skeleton, new Vector3(0f, 0.1f, 11f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(skeleton, new Vector3(1.5f, 0.1f, 9.5f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(skeleton, new Vector3(3f, 0.1f, 8f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().BeReady();
                music.PlayVeryHighLevelMusic();
                break;
            case 12:
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(barbarian, new Vector3(-1.5f, 0.1f, 8f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(barbarian, new Vector3(1.5f, 0.1f, 8f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().BeReady();
                break;
            case 14:
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(swordMan, new Vector3(-2f, 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(swordMan, new Vector3(2f, 0.1f, 3f));
                buildingLevels[currentLevel - 1].GetComponent<Level>().BeReady();
                break;
            default:
                break;
        }
    }

    public void OpenChest()
    {
        if (!opened)
        {
            tempChest.GetComponent<Animation>().Play();
            PlayAudio(tempChest.GetComponent<AudioSource>(), clip);
            opened = true;
        }
    }

    IEnumerator DelaySpawnEnemy(GameObject enemy, float time)
    {
        yield return new WaitForSeconds(time);
        buildingLevels[currentLevel - 1].GetComponent<Level>().AddEnemy(enemy, new Vector3(Random.Range(-3f, 3f), 0.1f, 3f));
    }

    public void UpdateScrolls()
    {
        if (buildingLevels[currentLevel].tag == "EmptyLevel")
        {
            currentPlayerScroll.transform.position += new Vector3(0, floorHeight * 2, 0);
        }
        else
        {
            currentPlayerScroll.transform.position += new Vector3(0, floorHeight, 0);
        }
        Instantiate(playerScrollPos, Vector3.zero, Quaternion.identity, buildingLevels[currentLevel - 1].transform).transform.localPosition = Vector3.zero;
    }

    public void PlayAudio(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.9f, 1.1f);
        source.Play();
    }

    public void GetSavedInfo()  {
        if (PlayerPrefs.GetInt("hasPlayed") == 0)   {
            PlayerPrefs.SetInt("hasPlayed", 1);

            PlayerPrefs.SetInt("attack", 10);
            PlayerPrefs.SetInt("defense", 10);
            PlayerPrefs.SetInt("health", 1000);
            PlayerPrefs.SetInt("currentLevel", 1);
            PlayerPrefs.SetInt("numGems", 0);
            PlayerPrefs.SetInt("NumDeaths", 0);

            PlayerPrefs.SetInt("upgradeUpgradeHealthCost", 1);
            PlayerPrefs.SetInt("upgradeUpgradeAttackCost", 1);
            PlayerPrefs.SetInt("upgradeUpgradeDefenseCost", 1);

            PlayerPrefs.SetInt("healthUpgrade", 500);
            PlayerPrefs.SetInt("attackUpgrade", 3);
            PlayerPrefs.SetInt("defenseUpgrade", 5);

            player.Initialize(1000, 10, 10, 0);
            currentLevel = 1;
        }   else
        {
            player.Initialize(PlayerPrefs.GetInt("health"), PlayerPrefs.GetInt("attack"), PlayerPrefs.GetInt("defense"), PlayerPrefs.GetInt("numGems"));
            currentLevel = PlayerPrefs.GetInt("currentLevel");
        }
    }

    public void SaveInfo()  {
        PlayerPrefs.SetInt("attack", player.GetAttack());
        PlayerPrefs.SetInt("defense", player.GetDefense());
        PlayerPrefs.SetInt("health", player.GetHealth());
        PlayerPrefs.SetInt("numGems", player.GetNumGems());
        PlayerPrefs.SetInt("currentLevel", currentLevel);
    }

    public GameObject GetCurrentLevelObject()
    {
        return buildingLevels[currentLevel - 1];
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SetIsTransitioning(bool value, bool produceGems)
    {
        isTransitioning = value;
        if (isTransitioning)
        {
            RaycastHit hit;
            if (Physics.Raycast(currentMovableTile.transform.position + new Vector3(0, 3, 0), Vector3.up, out hit))
            {
                if (produceGems)
                {
                    SpawnGems();
                }
                else
                {
                    StartCoroutine("Transition");
                }
				hit.transform.gameObject.GetComponent<MovingTile> ().StartCoroutine ("MoveDown");
            }
        }
    }

    public bool GetIsTransitioning()
    {
        return isTransitioning;
    }

    public void SetCeilingHasMoved(bool value)  {
        ceilingHasMoved = value;
    }

    public void SetCurrentMovableTilePosition(Vector3 pos)
    {
        currentMovableTile.transform.position = pos;
    }
}