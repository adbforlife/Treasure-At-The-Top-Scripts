using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMaterial : MonoBehaviour {

    private Transform rightHand;
    private Transform leftHand;

    public GameObject swordUpgradeParticles;
    public GameObject shieldUpgradeParticles;

    private UpgradeAudio upgradeAudio;

    public enum UpgradeType { Health, Attack, Defense }
    public UpgradeType upgradeType;

    public float maxScale;
    public float minScale;
    private Vector3 expandRate;

    private Player player;
    private SceneController scene;
    private Input_Listeners IPL;

    private int value;

    private bool isReady;



    // Use this for initialization
    void Start()
    {
        switch(upgradeType)
        {
            case UpgradeType.Health:
                if (PlayerPrefs.GetInt("healthUpgrade") == 0)
                {
                    PlayerPrefs.SetInt("healthUpgrade", 500);
                    value = 500;
                }   else
                {
                    value = PlayerPrefs.GetInt("healthUpgrade");
                }
                break;
            case UpgradeType.Attack:
                if (PlayerPrefs.GetInt("attackUpgrade") == 0)   {
                    PlayerPrefs.SetInt("attackUpgrade", 3);
                    value = 3;
                }   else
                {
                    value = PlayerPrefs.GetInt("attackUpgrade");
                }
                break;
            case UpgradeType.Defense:
                if (PlayerPrefs.GetInt("defenseUpgrade") == 0)
                {
                    PlayerPrefs.SetInt("defenseUpgrade", 5);
                    value = 5;
                }   else
                {
                    value = PlayerPrefs.GetInt("defenseUpgrade");
                }
                break;
            default:
                break;
        }
        player = Singleton_Service.GetSingleton<Player>();
        scene = Singleton_Service.GetSingleton<SceneController>();
        IPL = Singleton_Service.GetSingleton<Input_Listeners>();

        rightHand = GameObject.Find("RightHand").transform;
        leftHand = GameObject.Find("LeftHand").transform;

        upgradeAudio = GetComponent<UpgradeAudio>();

        expandRate = new Vector3((maxScale - minScale) / 10, (maxScale - minScale) / 10, (maxScale - minScale) / 10);

        StartCoroutine("MoveUp");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isReady) {
            if (other.tag == "Sword" || other.tag == "Shield" || other.tag == "Wand")
            {
                transform.GetChild(0).GetComponent<SpinningObject>().PauseSpinning();
                StopCoroutine("Shrink");
                StartCoroutine("Expand");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isReady)
        {
            if (other.tag == "Sword" && IPL.GetRightTriggerInteracted() || other.tag == "Wand" && IPL.GetRightTriggerInteracted() || other.tag == "Shield" && IPL.GetLeftTriggerInteracted())
            {
                switch (upgradeType)
                {
                    case UpgradeType.Health:
                        scene.DeactivateGems();
                        scene.PlayAudio(player.GetComponent<AudioSource>(), upgradeAudio.upgradeAudio[Random.Range(0, upgradeAudio.upgradeAudio.Length)]);
                        player.IncreaseHealth(value);
                        scene.UpdateScrolls();
                        scene.StartCoroutine("Transition");
                        break;
                    case UpgradeType.Attack:
                        scene.DeactivateGems();
                        Instantiate(swordUpgradeParticles, rightHand).transform.localPosition = Vector3.zero;
                        scene.PlayAudio(rightHand.gameObject.GetComponent<AudioSource>(), upgradeAudio.upgradeAudio[Random.Range(0, upgradeAudio.upgradeAudio.Length)]);
                        player.IncreaseAttack(value);
                        scene.UpdateScrolls();
                        scene.StartCoroutine("Transition");
                        break;
                    case UpgradeType.Defense:
                        scene.DeactivateGems();
                        Instantiate(swordUpgradeParticles, leftHand).transform.localPosition = Vector3.zero;
                        scene.PlayAudio(leftHand.gameObject.GetComponent<AudioSource>(), upgradeAudio.upgradeAudio[Random.Range(0, upgradeAudio.upgradeAudio.Length)]);
                        player.IncreaseDefense(value);
                        scene.UpdateScrolls();
                        scene.StartCoroutine("Transition");
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isReady)
        {
            if (other.tag == "Sword" || other.tag == "Shield" || other.tag == "Wand")
            {
                transform.GetChild(0).GetComponent<SpinningObject>().ContinueSpinning();
                StopCoroutine("Expand");
                StartCoroutine("Shrink");
            }
        }
    }

    IEnumerator MoveUp()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.02f);
            transform.position += new Vector3(0, 0.01f, 0);
        }
        isReady = true;
    }

    IEnumerator Expand()
    {
        while (transform.localScale.x < maxScale)
        {
            yield return new WaitForSeconds(0.02f);
            transform.localScale += expandRate;
        }
    }

    IEnumerator Shrink()
    {
        while (transform.localScale.x > minScale)
        {
            yield return new WaitForSeconds(0.02f);
            transform.localScale -= expandRate;
        }
    }
}
