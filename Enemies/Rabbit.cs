using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {

    private void Start()
    {
        GetComponent<Enemy_State_Pattern>().isInvincible = true;
    }

    public void BecomeInvulnerable()
    {
        GetComponent<Enemy_State_Pattern>().isInvincible = true;
    }

    public void BecomeVulnerable()
    {
        GetComponent<Enemy_State_Pattern>().isInvincible = false;
    }
}