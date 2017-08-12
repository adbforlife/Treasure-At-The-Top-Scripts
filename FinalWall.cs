using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalWall : MonoBehaviour {

    private SceneController scene;
    public GameObject boss;

    private void Start()
    {
        scene = Singleton_Service.GetSingleton<SceneController>();    
    }

    public void StartOpening()
    {
        StartCoroutine("Open");
    }

    public void StartClosing()
    {
        StartCoroutine("Close");
    }

    IEnumerator Open()
    {
        StartCoroutine(MoveAway(transform.GetChild(0).GetChild(3)));
        StartCoroutine(MoveAway(transform.GetChild(0).GetChild(4)));
        StartCoroutine(MoveAway(transform.GetChild(1).GetChild(3)));
        StartCoroutine(MoveAway(transform.GetChild(1).GetChild(4)));
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(MoveAway(transform.GetChild(0).GetChild(2)));
        StartCoroutine(MoveAway(transform.GetChild(0).GetChild(5)));
        StartCoroutine(MoveAway(transform.GetChild(1).GetChild(2)));
        StartCoroutine(MoveAway(transform.GetChild(1).GetChild(5)));
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(MoveAway(transform.GetChild(0).GetChild(1)));
        StartCoroutine(MoveAway(transform.GetChild(0).GetChild(6)));
        StartCoroutine(MoveAway(transform.GetChild(1).GetChild(1)));
        StartCoroutine(MoveAway(transform.GetChild(1).GetChild(6)));
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(MoveAway(transform.GetChild(0).GetChild(0)));
        StartCoroutine(MoveAway(transform.GetChild(0).GetChild(7)));
        StartCoroutine(MoveAway(transform.GetChild(1).GetChild(0)));
        StartCoroutine(MoveAway(transform.GetChild(1).GetChild(7)));
        yield return new WaitForSeconds(1.5f);
        boss.GetComponent<Mage>().Initiate();
    }

    IEnumerator MoveAway(Transform wall)
    {
        if (wall.localPosition.x < 0)
        {
            while (wall.localPosition.x > -8.25f)
            {
                yield return new WaitForSeconds(0.01f);
                wall.localPosition -= new Vector3(0.02f, 0f, 0f);
            }
        }
        else
        {
            while (wall.localPosition.x < 8.25f)
            {
                yield return new WaitForSeconds(0.01f);
                wall.localPosition += new Vector3(0.02f, 0f, 0f);
            }
        }
    }

    IEnumerator Close() {
        StartCoroutine("TextAppear");
		StartCoroutine(MoveIn(transform.GetChild(0).GetChild(7)));
		StartCoroutine(MoveIn(transform.GetChild(1).GetChild(7)));
        yield return new WaitForSeconds(1.5f);
		StartCoroutine(MoveIn(transform.GetChild(0).GetChild(6)));
		StartCoroutine(MoveIn(transform.GetChild(1).GetChild(6)));
		yield return new WaitForSeconds(1.5f);
		StartCoroutine(MoveIn(transform.GetChild(0).GetChild(5)));
		StartCoroutine(MoveIn(transform.GetChild(1).GetChild(5)));
		yield return new WaitForSeconds(1.5f);
		StartCoroutine(MoveIn(transform.GetChild(0).GetChild(4)));
		StartCoroutine(MoveIn(transform.GetChild(1).GetChild(4)));
		yield return new WaitForSeconds(1.5f);
		StartCoroutine(MoveIn(transform.GetChild(0).GetChild(3)));
		StartCoroutine(MoveIn(transform.GetChild(1).GetChild(3)));
		yield return new WaitForSeconds(1.5f);
		StartCoroutine(MoveIn(transform.GetChild(0).GetChild(2)));
		StartCoroutine(MoveIn(transform.GetChild(1).GetChild(2)));
		yield return new WaitForSeconds(1.5f);
		StartCoroutine(MoveIn(transform.GetChild(0).GetChild(1)));
		StartCoroutine(MoveIn(transform.GetChild(1).GetChild(1)));
		yield return new WaitForSeconds(1.5f);
		StartCoroutine(MoveIn(transform.GetChild(0).GetChild(0)));
		StartCoroutine(MoveIn(transform.GetChild(1).GetChild(0)));
    }

    IEnumerator MoveIn(Transform wall)  {
        if (wall.localPosition.x < 0)   {
            while (wall.localPosition.x < -2.75f)   {
                yield return new WaitForSeconds(0.01f);
                wall.localPosition += new Vector3(0.02f, 0f, 0f);
            }
        }   else
        {
            while (wall.localPosition.x > 2.75f)    {
                yield return new WaitForSeconds(0.01f);
                wall.localPosition -= new Vector3(0.02f, 0f, 0f);
            }
        }
    }

    IEnumerator TextAppear()    {
        yield return new WaitForSeconds(4.25f);
        transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).GetChild(4).gameObject.SetActive(true);
		yield return new WaitForSeconds(3f);
		transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
		transform.GetChild(2).GetChild(5).gameObject.SetActive(true);
		yield return new WaitForSeconds(3f);
		transform.GetChild(2).GetChild(3).gameObject.SetActive(true);
		transform.GetChild(2).GetChild(6).gameObject.SetActive(true);
        transform.GetChild(2).GetChild(7).gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        scene.GetComponent<ScreenFader>().fadeIn = false;
        StartCoroutine(scene.GetComponent<ScreenFader>().DoFade());
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Play");
    }
}
