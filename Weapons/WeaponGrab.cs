using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGrab : MonoBehaviour {

    public enum WeaponType { Weapon1, Weapon2, Shield2, Weapon3, Shield3, Weapon4 };
    public WeaponType weaponType;

    public float maxScale;
    public float minScale;
    private Vector3 expandRate;

    private SceneController scene;
    private Player player;
    private RightHand rightHand;
    private LeftHand leftHand;
    private Input_Listeners IPL;

    private int value;

    // Use this for initialization
    void Start() {
        scene = Singleton_Service.GetSingleton<SceneController>();
        player = Singleton_Service.GetSingleton<Player>();
        IPL = Singleton_Service.GetSingleton<Input_Listeners>();

        transform.parent.localScale = new Vector3(minScale, minScale, minScale);
        expandRate = new Vector3((maxScale - minScale) / 10, (maxScale - minScale) / 10, (maxScale - minScale) / 10);

        switch (weaponType)
        {
            case WeaponType.Weapon1:
                break;
            case WeaponType.Weapon2:
                value = 15;
                break;
            case WeaponType.Weapon3:
                value = 30;
                break;
            case WeaponType.Weapon4:
                value = 50;
                break;
            case WeaponType.Shield2:
                value = 15;
                break;
            case WeaponType.Shield3:
                value = 30;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (weaponType)
        {
            case WeaponType.Weapon2:
            case WeaponType.Weapon3:
            case WeaponType.Weapon4:
                if (other.tag == "Sword" || other.tag == "Wand")
                {
                    StopCoroutine("Shrink");
                    StartCoroutine("Expand");
                }
                break;
            case WeaponType.Shield2:
            case WeaponType.Shield3:
                if (other.tag == "Shield")
                {
                    StopCoroutine("Shrink");
                    StartCoroutine("Expand");
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (weaponType)
        {
            case WeaponType.Weapon2:
                if ((other.tag == "Sword" || other.tag == "Wand") && IPL.GetRightTriggerInteracted())
                {
                    player.IncreaseAttack(value);

                    rightHand = Singleton_Service.GetSingleton<RightHand>();
                    rightHand.SetSword(transform.parent.gameObject, new Vector3(0f, 0f, 0.22f), new Vector3(180, 0, 90))/*.transform.parent.localScale = new Vector3(maxScale / minScale, maxScale / minScale, maxScale / minScale)*/;

                    transform.parent.parent.parent.gameObject.GetComponent<ChestTable>().DecrementNumWeapons();

                    Destroy(transform.parent.gameObject);
                }
                break;
            case WeaponType.Weapon3:
                if ((other.tag == "Sword" || other.tag == "Wand") && IPL.GetRightTriggerInteracted())
                {
                    player.IncreaseAttack(value);

                    rightHand = Singleton_Service.GetSingleton<RightHand>();
                    rightHand.SetSword(transform.parent.gameObject, new Vector3(0f, 0f, 0.25f), new Vector3(180, -90, -90))/*.transform.parent.localScale = new Vector3(maxScale / minScale, maxScale / minScale, maxScale / minScale)*/;

                    transform.parent.parent.parent.gameObject.GetComponent<ChestTable>().DecrementNumWeapons();

                    Destroy(transform.parent.gameObject);
                }
                break;
            case WeaponType.Weapon4:
                if ((other.tag == "Sword" || other.tag == "Wand") && IPL.GetRightTriggerInteracted())
                {
                    player.IncreaseAttack(value);

                    rightHand = Singleton_Service.GetSingleton<RightHand>();
                    rightHand.SetSword(transform.parent.gameObject, new Vector3(0f, 0f, 0.15f), new Vector3(0, 90, 90))/*.transform.parent.localScale = new Vector3(maxScale / minScale, maxScale / minScale, maxScale / minScale)*/;

                    transform.parent.parent.parent.gameObject.GetComponent<ChestTable>().DecrementNumWeapons();
                    transform.parent.parent.parent.gameObject.GetComponent<ChestTable>().DecrementNumWeapons();

                    Destroy(transform.parent.gameObject);
                }
                break;
            case WeaponType.Shield2:
                if (other.tag == "Shield" && IPL.GetLeftTriggerInteracted())
                {
                    player.IncreaseDefense(value);

                    leftHand = Singleton_Service.GetSingleton<LeftHand>();
                    leftHand.SetShield(transform.parent.gameObject, new Vector3(0f, 0f, 0f), new Vector3(45, 180, 180))/*.transform.parent.localScale = new Vector3(maxScale / minScale, maxScale / minScale, maxScale / minScale)*/;

                    transform.parent.parent.parent.gameObject.GetComponent<ChestTable>().DecrementNumWeapons();

                    Destroy(transform.parent.gameObject);
                }
                break;
            case WeaponType.Shield3:
                if (other.tag == "Shield" && IPL.GetLeftTriggerInteracted())
                {
                    player.IncreaseDefense(value);

                    leftHand = Singleton_Service.GetSingleton<LeftHand>();
                    leftHand.SetShield(transform.parent.gameObject, new Vector3(0f, 0f, -0.12f), new Vector3(-75, 180, 0))/*.transform.parent.localScale = new Vector3(maxScale / minScale, maxScale / minScale, maxScale / minScale)*/;

                    transform.parent.parent.parent.gameObject.GetComponent<ChestTable>().DecrementNumWeapons();

                    Destroy(transform.parent.gameObject);
                }
                break;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        switch (weaponType)
        {
            case WeaponType.Weapon2:
            case WeaponType.Weapon3:
            case WeaponType.Weapon4:
                if (other.tag == "Sword" || other.tag == "Wand")
                {
                    StopCoroutine("Expand");
                    StartCoroutine("Shrink");
                }
                break;
            case WeaponType.Shield2:
            case WeaponType.Shield3:
                if (other.tag == "Shield")
                {
                    StopCoroutine("Expand");
                    StartCoroutine("Shrink");
                }
                break;
            default:
                break;
        }
    }

    IEnumerator Expand()
    {
        while (transform.parent.localScale.x < maxScale)
        {
            yield return new WaitForSeconds(0.02f);
            transform.parent.localScale += expandRate;
        }
    }

    IEnumerator Shrink()
    {
        while (transform.parent.localScale.x > minScale)
        {
            yield return new WaitForSeconds(0.02f);
            transform.parent.localScale -= expandRate;
        }
    }
}
