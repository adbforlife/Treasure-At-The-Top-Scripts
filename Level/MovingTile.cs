using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTile : MonoBehaviour {

    SceneController scene;
    public AudioClip movingStone;
	// Use this for initialization
	void Start () {
        scene = Singleton_Service.GetSingleton<SceneController>();
	}

	IEnumerator MoveDown()
	{
        transform.Find("Audio1").gameObject.GetComponent<AudioSource>().Play();
		for (int i = 0; i < 10; i++)
		{
			yield return new WaitForSeconds(0.05f);
            transform.position -= new Vector3(0, 0.01f, 0);
		}
		yield return new WaitForSeconds(0.5f);
		StartCoroutine("MoveAway");
	}

	IEnumerator MoveAway()	{
        transform.Find("Audio2").gameObject.GetComponent<AudioSource>().Play();
		for (int i = 0; i < 150; i++)
		{
			yield return new WaitForSeconds(0.02f);
			transform.position += new Vector3(0, 0, 0.02f);
		}
        scene.SetCeilingHasMoved(true);
	}

}
