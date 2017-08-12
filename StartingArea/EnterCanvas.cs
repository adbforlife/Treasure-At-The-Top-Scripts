using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCanvas : MonoBehaviour {
	void OnEnable()
	{
		Singleton_Service.RegisterSingletonInstance(this);
	}

	void OnDisable()
	{
		Singleton_Service.UnregisterSingletonInstance(this);
	}
}