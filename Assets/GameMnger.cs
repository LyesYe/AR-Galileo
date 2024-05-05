using FischlWorks_FogWar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameMnager : MonoBehaviour
{
	public static GameMnager instance;
	public GameObject eye;
	public GameObject player;
	public csFogWar csfow;


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
		var fr = Instantiate(eye, player.transform.position, Quaternion.identity);
		csfow.AddFogRevealer(new csFogWar.FogRevealer(fr.transform, 10, true));
		StartCoroutine(SpawnLight());
	}
	public void ChangeScene()
	{
		SceneManager.LoadScene("AR");
	}
}
