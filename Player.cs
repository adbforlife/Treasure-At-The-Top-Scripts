using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    public Text healthText;
    public Text attackText;
    public Text defenseText;
    public Text numGemsText;
    public Text challengerText;

    int health;
    int attack;
    int defense;
    int numGems;

    SceneController scene;

    private int currentLevel;
    private int numSkulls;
    private int numDeaths;

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

        scene = Singleton_Service.GetSingleton<SceneController>();
        currentLevel = scene.GetCurrentLevel();
        numGems = PlayerPrefs.GetInt("numGems");
        numDeaths = PlayerPrefs.GetInt("NumDeaths");
        UpdatePlayerUI();
    }

    public void Initialize(int hp, int a, int d, int g)    {
        health = hp;
        attack = a;
        defense = d;
        numGems = g;
    }

    public void UpdatePlayerUI()
    {
        attackText.text = attack.ToString();
        defenseText.text = defense.ToString();
        healthText.text = health.ToString();
        numGemsText.text = numGems.ToString();
        numDeaths = PlayerPrefs.GetInt("NumDeaths");
        challengerText.text = "Challenger " + (numDeaths + 1).ToString();
    }

    public void Damaged(int enemyAttack)
    {
        int damage = enemyAttack - defense;
        if (damage < 1) { damage = 1; }
        health -= damage;
        if (health <= 0) {
            health = 0;
            StartCoroutine("Die");
        }
        UpdatePlayerUI();
    }

    IEnumerator Die()
    {
        yield return null;
        numDeaths++;
        PlayerPrefs.SetInt("attack", 10);
        PlayerPrefs.SetInt("defense", 10);
        PlayerPrefs.SetInt("health", 1000);
        PlayerPrefs.SetInt("currentLevel", 1);
        PlayerPrefs.SetInt("NumDeaths", numDeaths);
        SceneManager.LoadScene("Play");
    }

    public void IncreaseSkulls(int value)
    {
        numSkulls += value;
    }

    public void IncreaseAttack(int value)
    {
        attack += value;
        PlayerPrefs.SetInt("attack", attack);
        PlayerPrefs.SetInt("defense", defense);
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetInt("numGems", numGems);
        UpdatePlayerUI();
    }

    public void IncreaseDefense(int value)
    {
        defense += value;
        PlayerPrefs.SetInt("attack", attack);
        PlayerPrefs.SetInt("defense", defense);
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetInt("numGems", numGems);
        UpdatePlayerUI();
    }

    public void IncreaseHealth(int value)
    {
        health += value;
        PlayerPrefs.SetInt("attack", attack);
        PlayerPrefs.SetInt("defense", defense);
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetInt("numGems", numGems);
        UpdatePlayerUI();
    }

    public void IncreaseNumGems(int value)  {
        numGems += value;
        PlayerPrefs.SetInt("numGems", numGems);
        UpdatePlayerUI();
    }

    public int GetAttack()
    {
        return attack;
    }

    public int GetDefense()
    {
        return defense;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetNumGems() {
        return numGems;
    }

    public int GetNumDeaths()
    {
        return numDeaths;
    }
}
