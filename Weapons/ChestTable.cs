using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTable : MonoBehaviour {

    private SceneController scene;
    private int numWeapons;

    private void Start()
    {
        scene = Singleton_Service.GetSingleton<SceneController>();
        numWeapons = 2;         
    }

    public IEnumerator MoveAway()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 150; i++)
        {
            yield return new WaitForSeconds(0.02f);
            transform.position += new Vector3(0, 0, 0.01f);
        }
    }

    public void DecrementNumWeapons()
    {
        numWeapons--;
        if (numWeapons == 0)
        {
            StartCoroutine("MoveAway");
            scene.SetIsTransitioning(true, false);
            scene.UpdateScrolls();
        }
    }
}
