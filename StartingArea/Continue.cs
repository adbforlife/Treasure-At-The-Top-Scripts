using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Continue : MonoBehaviour {
    
    private FrontDoor door;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("hasPlayed") == 0)   {
            gameObject.GetComponent<Button>().interactable = false;
        }
        door = Singleton_Service.GetSingleton<FrontDoor>();
	}

    public void TransitionToGame()  {
        door.OpenDoor();
    }
}
