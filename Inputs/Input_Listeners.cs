using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Listeners : MonoBehaviour {
	public bool leftTriggerInteracted = false;
	public bool rightTriggerInteracted = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool GetLeftTriggerInteracted()	{
		return leftTriggerInteracted;
	}

	public bool GetRightTriggerInteracted()	{
		return rightTriggerInteracted;
	}

	public void SetLeftTriggerInteracted(bool value)	{
		leftTriggerInteracted = value;
	}

	public void SetRightTriggerInteracted(bool value)	{
		rightTriggerInteracted = value;
	}

	void OnEnable()
	{
		Singleton_Service.RegisterSingletonInstance(this);
	}
	void OnDisable()
	{
		Singleton_Service.UnregisterSingletonInstance(this);
	}
}
