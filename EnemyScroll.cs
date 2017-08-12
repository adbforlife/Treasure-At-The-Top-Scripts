using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScroll : MonoBehaviour {
    
    private Text monsterText;
    private Text healthText;
    private Text attackText;
    private Text defenseText;

    private Transform quad;

    private bool isActive;

	// Use this for initialization
	void Start () {
        quad = transform.Find("Quad");
        monsterText = quad.GetChild(0).Find("MonsterText").gameObject.GetComponent<Text>();
        healthText = quad.GetChild(0).Find("HealthText").gameObject.GetComponent<Text>();
        attackText = quad.GetChild(0).Find("AttackText").gameObject.GetComponent<Text>();
        defenseText = quad.GetChild(0).Find("DefenseText").gameObject.GetComponent<Text>();

        foreach(Transform child in quad)
        {
            child.gameObject.SetActive(false);
        }
	}

    public void UpdateText(string monsterName, int health, int attack, int defense) {
        if (!isActive)
        {
            foreach (Transform child in quad)
            {
                child.gameObject.SetActive(true);
                isActive = true;
            }
        }
        monsterText.text = monsterName;
        healthText.text = health.ToString();
        attackText.text = attack.ToString();
        defenseText.text = defense.ToString();
    }
}
