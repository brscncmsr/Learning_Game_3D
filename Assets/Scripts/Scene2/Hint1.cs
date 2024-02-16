using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint1 : MonoBehaviour
{
    public AudioSource h1;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(OpenHints());
    }

    public IEnumerator OpenHints()
    {

        h1.Play();
        yield return new WaitForSeconds(h1.clip.length);
        StartCoroutine(Compare2Controller.Instance.game.hintObject1.OpenHints());

    }

}
