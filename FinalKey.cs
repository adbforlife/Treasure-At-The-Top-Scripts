using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalKey : MonoBehaviour {

    private bool isReady;
    private RightHand rightHand;
    private SceneController scene;
    private Player player;

	// Use this for initialization
	void Start () {
        StartCoroutine("RaiseUp");
        scene = Singleton_Service.GetSingleton<SceneController>();
        player = Singleton_Service.GetSingleton<Player>();
	}

    IEnumerator RaiseUp()
    {
        for (int i = 0; i < 300; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.position += new Vector3(0f, 0.005f, 0f);
        }
        rightHand = Singleton_Service.GetSingleton<RightHand>();
        transform.parent = rightHand.transform;
        transform.localPosition = new Vector3(0.1f, 0.1f, 0.4f);
        transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        scene.GetComponent<ScreenFader>().fadeIn = false;
        StartCoroutine(scene.GetComponent<ScreenFader>().DoFade());
        yield return new WaitForSeconds(2f);
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 18.5f);
        scene.GetComponent<ScreenFader>().fadeIn = true;
        StartCoroutine(scene.GetComponent<ScreenFader>().DoFade());
    }
}
