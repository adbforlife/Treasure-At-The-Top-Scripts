using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public GameObject riseEffect;

    private Vector3 originalScale;
    private Player player;
    private Animator anim;

    private void Start()
    {
        originalScale = this.transform.localScale;
        player = Singleton_Service.GetSingleton<Player>();
        anim = GetComponent<Animator>();
    }

    public IEnumerator SpecialAttack()
    {
        Instantiate(riseEffect).transform.position = transform.position;
        for (int i = 0; i < 80; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.localPosition += new Vector3(0f, 0.015f, 0f);
            transform.localScale *= 0.99f;
        }

        transform.position = new Vector3(Random.Range(-3f, 3f), player.transform.position.y + 1.3f, Random.Range(0f, 3f));
        transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        transform.LookAt(player.transform.position);

        Instantiate(riseEffect, Vector3.zero, Quaternion.Euler(new Vector3(90, 0, 0))).transform.position = transform.position;
        for (int i = 0; i < 80; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.localPosition -= new Vector3(0f, 0.015f, 0f);
            if (transform.localScale.x < originalScale.x)
            {
                transform.localScale *= 1.01f;
            }
        }
        transform.LookAt(player.transform.position);

        GetComponent<Enemy_State_Pattern>().ResetTempNumDamages();
        GetComponent<Enemy_State_Pattern>().currentState.ToChaseState();
    }
}
