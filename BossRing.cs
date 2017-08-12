using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRing : MonoBehaviour {

    private int health = 1;

    public int GetHealth()
    {
        return health;
    }

    public void DecrementHealth()
    {
        health--;
    }
}
