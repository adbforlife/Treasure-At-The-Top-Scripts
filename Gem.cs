using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

    private GameObject destination;
    private Vector3 deltaPos;
    private Player player;
    private SceneController scene;

	// Use this for initialization
	void Start () {
        player = Singleton_Service.GetSingleton<Player>();
        scene = Singleton_Service.GetSingleton<SceneController>();
        destination = GameObject.Find("DestinationGem");
        deltaPos = (destination.transform.position - transform.position) / 50f;
        StartMoving();
	}

    public void StartMoving()   {
        StartCoroutine("Travel");
    }

    IEnumerator Travel()    {
        yield return new WaitForSeconds(1f);
        while (Vector3.Distance(transform.position, destination.transform.position) > 0.1f) {
            yield return new WaitForSeconds(0.01f);
            if (transform.localScale.x > 10f)    {
                transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
            }
            transform.position += deltaPos;
        }
        player.IncreaseNumGems(1);
        scene.PlayAudio(player.GetComponent<AudioSource>(), GetComponent<UpgradeAudio>().upgradeAudio[Random.Range(0, GetComponent<UpgradeAudio>().upgradeAudio.Length)]);

        Destroy(gameObject);
    }
}
