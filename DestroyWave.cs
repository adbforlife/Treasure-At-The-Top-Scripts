using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWave : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 5f);	
	}
}
