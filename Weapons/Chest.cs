using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {


    private bool opened;
    private SceneController scene;

	// Use this for initialization
	void Start () {
        scene = Singleton_Service.GetSingleton<SceneController>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword" || other.tag == "Wand")
        {
            OpenChest();
        }
    }

    public void OpenChest()
    {
        if (!opened)
        {
            transform.parent.parent.gameObject.GetComponent<Animation>().Play();
            scene.PlayAudio(GetComponent<AudioSource>(), GetComponent<OpenLockAudio>().openLockAudio[Random.Range(0, GetComponent<OpenLockAudio>().openLockAudio.Length)]);
            opened = true;
        }
    }
}
