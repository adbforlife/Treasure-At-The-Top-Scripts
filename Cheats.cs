using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

    private Player player;

	// Use this for initialization
	void Start () {
        player = Singleton_Service.GetSingleton<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt("hasPlayed", 0);
        }   else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.IncreaseHealth(1000);
            player.UpdatePlayerUI();
        }   else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.IncreaseAttack(5);
            player.UpdatePlayerUI();
        }   else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.IncreaseDefense(5);
            player.UpdatePlayerUI();
        }
	}
}
