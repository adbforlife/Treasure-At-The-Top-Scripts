using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUpgrade : MonoBehaviour {

    public Text upgradeHealthText;
    public Text upgradeAttackText;
    public Text upgradeDefenseText;
    public Text canvasNumGemsText;

    public enum UpgradeUpgradeType { Health, Attack, Defense}
    public UpgradeUpgradeType upgradeUpgradeType;
    [HideInInspector] public int cost;

    private SceneController scene;

	// Use this for initialization
	void Start () {
        scene = Singleton_Service.GetSingleton<SceneController>();

        if (PlayerPrefs.GetInt("healthUpgrade") == 0)
        {
            upgradeHealthText.text = "+500";
        }
        else
        {
            upgradeHealthText.text = "+" + PlayerPrefs.GetInt("healthUpgrade").ToString();
        }

        if (PlayerPrefs.GetInt("attackUpgrade") == 0)
        {
            upgradeAttackText.text = "+3";
        }
        else
        {
            upgradeAttackText.text = "+" + PlayerPrefs.GetInt("attackUpgrade").ToString();
        }

        if (PlayerPrefs.GetInt("defenseUpgrade") == 0)
        {
            upgradeDefenseText.text = "+5";
        }
        else
        {
            upgradeDefenseText.text = "+" + PlayerPrefs.GetInt("defenseUpgrade").ToString();
        }

        switch (upgradeUpgradeType) {
            case UpgradeUpgradeType.Health:
                if (PlayerPrefs.GetInt("upgradeUpgradeHealthCost") == 0)    {
                    PlayerPrefs.SetInt("upgradeUpgradeHealthCost", 1);
                    cost = 1;
                }   else
                {
                    cost = PlayerPrefs.GetInt("upgradeUpgradeHealthCost");
                }
                break;
            case UpgradeUpgradeType.Attack:
				if (PlayerPrefs.GetInt("upgradeUpgradeAttackCost") == 0)
				{
					PlayerPrefs.SetInt("upgradeUpgradeAttackCost", 1);
					cost = 1;
				}
				else
				{
					cost = PlayerPrefs.GetInt("upgradeUpgradeAttackCost");
				}
				break;
            case UpgradeUpgradeType.Defense:
				if (PlayerPrefs.GetInt("upgradeUpgradeDefenseCost") == 0)
				{
					PlayerPrefs.SetInt("upgradeUpgradeDefenseCost", 1);
					cost = 1;
				}
				else
				{
					cost = PlayerPrefs.GetInt("upgradeUpgradeDefenseCost");
				}
				break;
            default:
                break;
        }
        transform.GetChild(0).GetComponent<Text>().text = cost.ToString();
        if (cost > PlayerPrefs.GetInt("numGems"))
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void UpgradeUpgradeHealth()  {
        Singleton_Service.GetSingleton<Player>().IncreaseNumGems(-cost);
        canvasNumGemsText.text = PlayerPrefs.GetInt("numGems").ToString();

        PlayerPrefs.SetInt("healthUpgrade", PlayerPrefs.GetInt("healthUpgrade") + 100);
        cost++;
        PlayerPrefs.SetInt("upgradeUpgradeHealthCost", cost);
        transform.GetChild(0).GetComponent<Text>().text = cost.ToString();
        upgradeHealthText.text = "+" + PlayerPrefs.GetInt("healthUpgrade").ToString();
        foreach (GameObject thing in GameObject.FindGameObjectsWithTag("UpgradeUpgradeButton"))
        {
            if (thing.GetComponent<UpgradeUpgrade>().cost > PlayerPrefs.GetInt("numGems"))
            {
                thing.GetComponent<Button>().interactable = false;
            }
        }

        scene.PlayAudio(GetComponent<AudioSource>(), GetComponent<UpgradeAudio>().upgradeAudio[Random.Range(0, GetComponent<UpgradeAudio>().upgradeAudio.Length)]);
    }

    public void UpgradeUpgradeAttack() {
        Singleton_Service.GetSingleton<Player>().IncreaseNumGems(-cost);
        canvasNumGemsText.text = PlayerPrefs.GetInt("numGems").ToString();

        PlayerPrefs.SetInt("attackUpgrade", PlayerPrefs.GetInt("attackUpgrade") + 1);
		cost++;
        PlayerPrefs.SetInt("upgradeUpgradeAttackCost", cost);
        transform.GetChild(0).GetComponent<Text>().text = cost.ToString();
        upgradeAttackText.text = "+" + PlayerPrefs.GetInt("attackUpgrade").ToString();
        foreach (GameObject thing in GameObject.FindGameObjectsWithTag("UpgradeUpgradeButton"))
        {
            if (thing.GetComponent<UpgradeUpgrade>().cost > PlayerPrefs.GetInt("numGems"))
            {
                thing.GetComponent<Button>().interactable = false;
            }
        }

        scene.PlayAudio(GetComponent<AudioSource>(), GetComponent<UpgradeAudio>().upgradeAudio[Random.Range(0, GetComponent<UpgradeAudio>().upgradeAudio.Length)]);
    }

    public void UpgradeUpgradeDefense()
	{
        Singleton_Service.GetSingleton<Player>().IncreaseNumGems(-cost);
        canvasNumGemsText.text = PlayerPrefs.GetInt("numGems").ToString();

        PlayerPrefs.SetInt("defenseUpgrade", PlayerPrefs.GetInt("defenseUpgrade") + 2);
		cost++;
		PlayerPrefs.SetInt("upgradeUpgradeDefenseCost", cost);
        transform.GetChild(0).GetComponent<Text>().text = cost.ToString();
        upgradeDefenseText.text = "+" + PlayerPrefs.GetInt("defenseUpgrade").ToString();
        foreach (GameObject thing in GameObject.FindGameObjectsWithTag("UpgradeUpgradeButton"))
        {
            if (thing.GetComponent<UpgradeUpgrade>().cost > PlayerPrefs.GetInt("numGems"))
            {
                thing.GetComponent<Button>().interactable = false;
            }
        }

        scene.PlayAudio(GetComponent<AudioSource>(), GetComponent<UpgradeAudio>().upgradeAudio[Random.Range(0, GetComponent<UpgradeAudio>().upgradeAudio.Length)]);
    }
}