using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour {

    private bool isPaused;

	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            transform.RotateAround(this.transform.position, Vector3.up, Time.deltaTime * 50);
        }
    }

    public void PauseSpinning()
    {
        isPaused = true;
    }

    public void ContinueSpinning()
    {
        isPaused = false;
    }
}
