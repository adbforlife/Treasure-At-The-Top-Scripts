using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    private GameObject sword;

    void OnEnable()
    {
        Singleton_Service.RegisterSingletonInstance(this);
    }

    void OnDisable()
    {
        Singleton_Service.UnregisterSingletonInstance(this);
    }

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    public IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            device.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }

    public GameObject SetSword(GameObject obj, Vector3 position, Vector3 rotation)
    {
        Destroy(transform.GetChild(0).gameObject);

        GameObject swordRight = Instantiate(obj, transform);
        swordRight.transform.localPosition = position;
        swordRight.transform.localRotation = Quaternion.Euler(rotation);
        sword = swordRight;

        return swordRight;
    }

    public GameObject GetSword()
    {
        return sword;
    }
}
