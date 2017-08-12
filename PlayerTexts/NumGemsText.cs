using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumGemsText : MonoBehaviour {

    //private Player player;

	// Use this for initialization
	void Start () {
        //player = Singleton_Service.GetSingleton<Player>();
        //GetComponent<Text>().text = player.GetNumGems().ToString();
        GetComponent<Text>().text = PlayerPrefs.GetInt("numGems").ToString();
	}
}
