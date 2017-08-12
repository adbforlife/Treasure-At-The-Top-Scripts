using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandSword : MonoBehaviour {

    public GameObject energyBall;
    private GameObject currentBall;
    private SceneController scene;
    private Input_Listeners IPL;
    private RightHand rightHand;
    private bool charged;

	// Use this for initialization
	void Start () {
        if (transform.parent.gameObject.GetComponent<RightHand>() != null)
        {
            scene = Singleton_Service.GetSingleton<SceneController>();
            IPL = Singleton_Service.GetSingleton<Input_Listeners>();
            currentBall = Instantiate(energyBall, transform);
            currentBall.transform.localPosition = new Vector3(0f, 0.72f, 0f);
            currentBall.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
	}

    private void Update()
    {
        if (charged && IPL.GetRightTriggerInteracted())
        {
            rightHand = Singleton_Service.GetSingleton<RightHand>();
            rightHand.StartCoroutine(rightHand.LongVibration(0.05f, 1f));
            currentBall.GetComponent<EnergyBall>().StartCoroutine("Shoot");
            currentBall = Instantiate(energyBall, transform);
            currentBall.transform.localPosition = new Vector3(0f, 0.72f, 0f);
            currentBall.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            charged = false;
        }
    }

    public void Charged()
    {
        charged = true;
    }
}
