using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : MonoBehaviour {

    public GameObject readyEffect;

    public void ReadyAttack()
    {
        /*Instantiate(readyEffect, transform.GetChild(1).Find("Spear")).transform.localPosition = Vector3.zero;
        Instantiate(readyEffect).transform.position = transform.GetChild(1).Find("Spear").position;*/
    }
}
