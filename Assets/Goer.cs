using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goer : MonoBehaviour
{
    public GameObject thing;
    void Start()
    {
        StartCoroutine(Go());
    }

    // Update is called once per frame
    IEnumerator Go()
    {
        yield return new WaitForSeconds(1.68f); 
        thing.SetActive(true);
        gameObject.SetActive(false);

    }
}
