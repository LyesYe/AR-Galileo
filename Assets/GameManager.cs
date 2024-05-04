using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject eye;
    public GameObject player;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(SpawnLight());
    }
    IEnumerator SpawnLight()
    {
        yield return new WaitForSeconds(1);
        Instantiate(eye,player.transform.position,Quaternion.identity);
        StartCoroutine(SpawnLight());
    }
}
