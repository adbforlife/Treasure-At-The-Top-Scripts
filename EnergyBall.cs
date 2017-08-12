using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour {
    public GameObject[] explodeEffects;


    private float maxScale;
    private float minScale;
    private bool isShot;
    private RightHand rightHand;
    private SceneController scene;
    private Player player;

	// Use this for initialization
	void Start () {
        maxScale = 0.15f;
        minScale = 0.11f;
        scene = Singleton_Service.GetSingleton<SceneController>();
        player = Singleton_Service.GetSingleton<Player>();
        StartCoroutine("Charge");
	}

    public IEnumerator Charge()
    {
        while (transform.localScale.x < maxScale)
        {
            transform.localScale += new Vector3(0.003f, 0.003f, 0.003f);
            yield return new WaitForSeconds(0.02f);
        }
        if (transform.parent.gameObject.GetComponent<Wand>() != null)
        {
            transform.parent.gameObject.GetComponent<Wand>().Charged();
        }
        else
        {
            transform.parent.gameObject.GetComponent<WandSword>().Charged();
        }
    }

    public IEnumerator Shoot()
    {
        rightHand = Singleton_Service.GetSingleton<RightHand>();
        Vector3 direction = (transform.position - rightHand.gameObject.transform.position) / 2.75f;
        isShot = true;
        scene.PlayAudio(player. GetComponent<AudioSource>(), GetComponent<EnergyBallAudio>().energyBallAudios[Random.Range(0, GetComponent<EnergyBallAudio>().energyBallAudios.Length)]);
        StartCoroutine("SelfDestruction");
        transform.parent = null;
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            transform.position += direction;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isShot && other.tag == "Infrastructure")
        {
            Explode();
        }
    }

    IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(3f);
        Explode();
    }

    public void Explode()
    {
        Instantiate(explodeEffects[Random.Range(0, explodeEffects.Length)]).transform.position = transform.position;
        Destroy(gameObject);
    }

    public bool GetIsShot()
    {
        return isShot;
    }
}
