using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class NewJourney : MonoBehaviour {
    
    private SceneController scene;
    private Player player;
    private FrontDoor door;

	void OnEnable()
	{
		Singleton_Service.RegisterSingletonInstance(this);
	}

	void OnDisable()
	{
		Singleton_Service.UnregisterSingletonInstance(this);
	}

    public void Start()
    {
        scene = Singleton_Service.GetSingleton<SceneController>();
        player = Singleton_Service.GetSingleton<Player>();
        door = Singleton_Service.GetSingleton<FrontDoor>();
    }

    public void TransitionToGame()  {
       
        PlayerPrefs.SetInt("hasPlayed", 0);
        door.OpenDoor();
    }
}