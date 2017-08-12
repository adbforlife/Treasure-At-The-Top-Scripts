using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Left_Vr_Cont : MonoBehaviour
{

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	Input_Listeners IPL;
	// Use this for initialization
	void Start()
	{
		IPL = Singleton_Service.GetSingleton <Input_Listeners>();
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Update is called once per frame
	void Update()
	{
		if (controller.GetPressDown(triggerButton))
		{ IPL.SetLeftTriggerInteracted(true); /*IPL.Set_Player_Interacting(true);*/ }
		if (controller.GetPressUp(triggerButton))
		{ IPL.SetLeftTriggerInteracted(false); /*IPL.Set_Player_Interacting(false);*/ }
	}

}