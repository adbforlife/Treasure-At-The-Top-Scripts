using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCollider : MonoBehaviour {

    public GameObject wave;

    private float speed;
    private bool started;
    public bool isReturnWave;
    private bool hasCausedDamage;
    private SceneController scene;
    private Player player;

    private void Start()
    {
        speed = 13f;
        scene = Singleton_Service.GetSingleton<SceneController>();
        player = Singleton_Service.GetSingleton<Player>();
    }

    // Update is called once per frame
    void Update () {
        transform.localPosition += new Vector3(Time.deltaTime * speed, 0, 0);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!isReturnWave && !hasCausedDamage)
        {
            if (other.tag == "Shield")
            {
                GameObject returnWave = Instantiate(transform.parent.parent.gameObject);
                returnWave.transform.position = new Vector3(other.transform.parent.position.x, transform.parent.position.y, other.transform.parent.position.z);
                returnWave.transform.LookAt(transform.parent);
                returnWave.transform.GetChild(0).GetChild(2).GetComponent<WaveCollider>().isReturnWave = true;

                scene.PlayAudio(other.GetComponent<AudioSource>(), other.GetComponent<ShieldAudio>().shieldAudio[Random.Range(0, other.GetComponent<ShieldAudio>().shieldAudio.Length)]);
                StartCoroutine(other.transform.parent.GetComponent<LeftHand>().LongVibration(0.05f, 1));
                player.Damaged(transform.parent.parent.parent.GetChild(0).GetComponent<Enemy_State_Pattern>().GetAttack());
                hasCausedDamage = true;
            }
            else if (other.tag == "Player")
            {
                player.Damaged(transform.parent.parent.parent.GetChild(0).GetComponent<Enemy_State_Pattern>().GetAttack() + player.GetDefense());
                scene.PlayAudio(player.GetComponent<AudioSource>(), player.GetComponent<PlayerHitAudio>().playerHitAudio[Random.Range(0, player.GetComponent<PlayerHitAudio>().playerHitAudio.Length)]);
                hasCausedDamage = true;
            }
        }   else if (isReturnWave)
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<Enemy_State_Pattern>().WaveDamaged();
            }
        }
    }
}
