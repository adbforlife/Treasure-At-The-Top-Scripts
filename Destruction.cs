using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour {

    public float destroyTime;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyTime);
	}
}
