using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour {

    private Player player;

    // Use this for initialization
    void Start () {
        player = Singleton_Service.GetSingleton<Player>();
        this.GetComponent<Text>().text = player.GetHealth().ToString();
    }
}
