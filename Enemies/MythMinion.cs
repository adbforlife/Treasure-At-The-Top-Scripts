using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MythMinion : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("ToChaseState");
    }

    IEnumerator ToChaseState()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Enemy_State_Pattern>().currentState.ToChaseState();
    }

    public void ChangePosition()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 1.5f);
    }
}
