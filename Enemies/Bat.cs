using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {
    public IEnumerator SpecialAttack()
    {
        for (int i = 0; i < 75; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.localPosition += new Vector3(0f, 0.01f, -0.02f);
        }
        for (int i = 0; i < 75; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.localPosition += new Vector3(0f, -0.01f, -0.02f);
        }
        for (int i = 0; i < 75; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.localPosition += new Vector3(0f, 0f, 0.04f);
        }
        /*for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.localPosition += new Vector3(0f, -0.025f, 0.05f);
        }*/
    }
}
