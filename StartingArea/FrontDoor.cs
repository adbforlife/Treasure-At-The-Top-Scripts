using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class FrontDoor : MonoBehaviour {

    public GameObject upgradeCanvas;
    public GameObject exitCanvas;

	public GameObject leftHand;
	public GameObject rightHand;
	public GameObject shield;
    public GameObject shield2;
    public GameObject shield3;
	public GameObject sword;
    public GameObject sword2;
    public GameObject wand;
    public GameObject wandSword;

    public GameObject playerScrollPos;
    
	private Animator anim;
    private GameObject enterCanvas;
    private NewJourney newJourneyButton;

    private SceneController scene;
    private Player player;
    private GameMusic music;

	private Vector3 insidePos = new Vector3(0, 0, -3f);
	private Vector3 swordLocalPos = new Vector3(0, 0, 0.25f);
	private int weaponScale = 20;
    private int lvl;

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
        if (PlayerPrefs.GetInt("hasPlayed") == 0)
        {
            exitCanvas.SetActive(false);
            upgradeCanvas.SetActive(false);
        }
        enterCanvas = Singleton_Service.GetSingleton<EnterCanvas>().gameObject;
        anim = GetComponent<Animator>();
        newJourneyButton = Singleton_Service.GetSingleton<NewJourney>();

        scene = Singleton_Service.GetSingleton<SceneController>();
        player = Singleton_Service.GetSingleton<Player>();
        music = Singleton_Service.GetSingleton<GameMusic>();
	}

    public void OpenDoor()  {
        enterCanvas.SetActive(false);
        upgradeCanvas.SetActive(false);
        exitCanvas.SetActive(false);
        anim.Play("OpenDoor");
    }

    public void FinishOpeningAnimation()    {
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        if (scene.GetCurrentLevel() < 5)
        {
            music.PlayLowLevelMusic();
        }   else if (scene.GetCurrentLevel() == 5)
        {
            music.PlayBossMusic();
        }   else if (scene.GetCurrentLevel() < 11)
        {
            music.PlayHighLevelMusic();
        }
        else
        {
            music.PlayVeryHighLevelMusic();
        }
        scene.GetComponent<ScreenFader>().fadeIn = false;
        StartCoroutine(scene.GetComponent<ScreenFader>().DoFade());
        yield return new WaitForSeconds(2f);
        TeleportInside();
        yield return new WaitForSeconds(0.2f);
        scene.GetComponent<ScreenFader>().fadeIn = true;
        StartCoroutine(scene.GetComponent<ScreenFader>().DoFade());
    }

	public void TeleportInside()
	{
        scene.GetSavedInfo();
        lvl = scene.GetCurrentLevel();

        insidePos = new Vector3(0f, (lvl - 1) * 4f, -3f);
		player.transform.position = insidePos + new Vector3(0, 0, 0.5f);
        scene.SetCurrentMovableTilePosition(insidePos);

        playerScrollPos.transform.position = new Vector3(0f, (scene.GetCurrentLevel() - 1) * 4f, 0f);
        player.UpdatePlayerUI();
		
		scene.ChangeActiveFloors();
		scene.SpawnEnemies();

        ChangeControllerModel();
	}

	public void ChangeControllerModel()
	{
        switch(lvl)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                leftHand.GetComponent<LeftHand>().SetShield(shield, Vector3.zero, new Vector3(45, 0, 0));
                rightHand.GetComponent<RightHand>().SetSword(sword, swordLocalPos, new Vector3(0, 90, 90));
                leftHand.GetComponent<NVRHand>().enabled = false;
                rightHand.GetComponent<NVRHand>().enabled = false;
                break;
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                leftHand.GetComponent<LeftHand>().SetShield(shield2, shield2.GetComponent<Weapon>().pos, shield2.GetComponent<Weapon>().rot);
                rightHand.GetComponent<RightHand>().SetSword(sword2, sword2.GetComponent<Weapon>().pos, sword2.GetComponent<Weapon>().rot);
                leftHand.GetComponent<NVRHand>().enabled = false;
                rightHand.GetComponent<NVRHand>().enabled = false;
                break;
            case 11:
            case 12:
            case 13:
                leftHand.GetComponent<LeftHand>().SetShield(shield3, shield2.GetComponent<Weapon>().pos, shield3.GetComponent<Weapon>().rot);
                rightHand.GetComponent<RightHand>().SetSword(wand, wand.GetComponent<Weapon>().pos, wand.GetComponent<Weapon>().rot);
                leftHand.GetComponent<NVRHand>().enabled = false;
                rightHand.GetComponent<NVRHand>().enabled = false;
                break;
            case 14:
                leftHand.GetComponent<LeftHand>().SetShield(shield3, shield2.GetComponent<Weapon>().pos, shield3.GetComponent<Weapon>().rot);
                rightHand.GetComponent<RightHand>().SetSword(wandSword, wandSword.GetComponent<Weapon>().pos, wandSword.GetComponent<Weapon>().rot);
                leftHand.GetComponent<NVRHand>().enabled = false;
                rightHand.GetComponent<NVRHand>().enabled = false;
                break;
        }
	}
}
