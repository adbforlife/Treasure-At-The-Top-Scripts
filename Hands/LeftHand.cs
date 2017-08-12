using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    private GameObject shield;

    void OnEnable()
    {
        Singleton_Service.RegisterSingletonInstance(this);
    }

    void OnDisable()
    {
        Singleton_Service.UnregisterSingletonInstance(this);
    }

    // Use this for initialization
    void Start()
    {
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

    public GameObject SetShield(GameObject obj, Vector3 position, Vector3 rotation)
    {
        Destroy(transform.GetChild(0).gameObject);

        GameObject shieldLeft = Instantiate(obj, transform);
        shieldLeft.transform.localPosition = position;
        shieldLeft.transform.localRotation = Quaternion.Euler(rotation);
        shieldLeft.AddComponent<Rigidbody>();
        shieldLeft.GetComponent<Rigidbody>().useGravity = false;
        shieldLeft.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        shieldLeft.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        shield = shieldLeft;

        return shieldLeft;
    }

    public GameObject GetShield()
    {
        return shield;
    }
}
