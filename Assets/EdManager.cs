using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EdManager : MonoBehaviour
{
   public void NextScene()
	{
		SceneManager.LoadScene("Map");
	}
}
