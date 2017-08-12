using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalChest : MonoBehaviour {

    private Animator anim;
    private SceneController scene;
    private Player player;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        scene = Singleton_Service.GetSingleton<SceneController>();
	}
	
    public void StopChestAnimation()    {
        anim.speed = 0;
        GameObject staff = transform.GetChild(3).gameObject;
        staff.transform.parent = Singleton_Service.GetSingleton<RightHand>().transform;
        staff.transform.localPosition = Vector3.zero;
        staff.transform.localRotation = Quaternion.Euler(0, 90, 0);
        StartCoroutine("FadePlayer");
    }

    IEnumerator FadePlayer()
    {
        player = Singleton_Service.GetSingleton<Player>();
        scene.GetComponent<ScreenFader>().fadeIn = false;
        StartCoroutine(scene.GetComponent<ScreenFader>().DoFade());
        yield return new WaitForSeconds(2f);
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 15);
        player.transform.localRotation = Quaternion.Euler(0, 180, 0);
        scene.GetComponent<ScreenFader>().fadeIn = true;
        StartCoroutine(scene.GetComponent<ScreenFader>().DoFade());
        yield return new WaitForSeconds(2f);
        GameObject ring = transform.parent.Find("BossRings").GetChild(0).gameObject;
        ring.SetActive(true);
        ring.transform.localPosition = new Vector3(0, 0, 15);
        for (int i = 0; i < 450; i++)
        {
            yield return new WaitForSeconds(0.01f);
            ring.transform.localPosition += new Vector3(0, 0.005f, 0);
            player.transform.localPosition += new Vector3(0, 0.005f, 0);
        }
        transform.parent.Find("FinalWall").GetComponent<FinalWall>().StartClosing();
        for (int i = 0; i < 450; i++)
        {
            yield return new WaitForSeconds(0.01f);
            ring.transform.localPosition += new Vector3(0, 0.005f, 0);
            player.transform.localPosition += new Vector3(0, 0.005f, 0);
        }
        transform.parent.Find("BossRings").GetChild(1).gameObject.SetActive(true);
        transform.parent.Find("BossRings").GetChild(2).gameObject.SetActive(true);
        transform.parent.Find("BossRings").GetChild(3).gameObject.SetActive(true);
        transform.parent.Find("BossRings").GetChild(4).gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Key") {
            other.transform.parent = transform;
            other.transform.localPosition = new Vector3(-0.05f, 1.58f, 0.9f);
            other.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            StartCoroutine(MoveIn(other.transform));
        }
    }

    IEnumerator MoveIn(Transform thing)
    {
        for (int i = 0; i < 300; i++)
        {
            yield return new WaitForSeconds(0.01f);
            thing.localPosition -= new Vector3(0, 0, 0.001f);
        }
        anim.Play("Open_Close");
    }
}