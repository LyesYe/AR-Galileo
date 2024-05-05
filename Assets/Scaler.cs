using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
	public Vector3 originalScale;
	public float scaleSpeed;
   public bool scaling = false;
   void Update()
	{
		if (scaling)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * scaleSpeed);
		}
	}
}
 