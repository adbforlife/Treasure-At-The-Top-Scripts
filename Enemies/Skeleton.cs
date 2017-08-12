using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour {

    public GameObject[] waves;
    private bool enraged;
    private SceneController scene;
    private Player player;

	// Use this for initialization
	void Start () {
        scene = Singleton_Service.GetSingleton<SceneController>();
        player = Singleton_Service.GetSingleton<Player>();
	}

    public void ChangePosition()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 3.3f);
        if (scene.GetCurrentLevelObject().GetComponent<Level>().hasStarted == false)    {
            scene.GetCurrentLevelObject().GetComponent<Level>().StartLevel();
        }
        GetComponent<Enemy_State_Pattern>().currentState.ToChaseState();
    }

    public void SendWave()  {
        GameObject tempWave = Instantiate(waves[Random.Range(0, waves.Length)], transform.parent);
        tempWave.transform.position = transform.position;
        tempWave.transform.LookAt(player.transform.position);
    }

    private void Update()
    {
        if (GetComponent<Enemy_State_Pattern>().GetHealth() < 150 && !enraged)
        {
            GetComponent<Animator>().Play("Skill");
            GetComponent<Enemy_State_Pattern>().SetTolerance(0);
            GetComponent<Enemy_State_Pattern>().SetMoveSpeed(1f);
            GetComponent<Enemy_State_Pattern>().SetThreshold(3f);
            enraged = true;
        }
    }
}
