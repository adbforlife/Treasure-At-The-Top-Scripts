using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MythDoc : MonoBehaviour
{
    public GameObject minion;

    SceneController scene;

    void Start()
    {
        scene = Singleton_Service.GetSingleton<SceneController>();
    }

    public void SpawnMinion()
    {
        scene.GetCurrentLevelObject().GetComponent<Level>().AddEnemy(minion, new Vector3(Random.Range(-3f, 3f), 0.1f, 3f));
    }
}
