using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class ARManager : MonoBehaviour
{
	public GameObject objectToSpawn; // The object to spawn
	public float sphereCastRadius = 0.1f; // Radius of the sphere cast
	public float spawnOffset = 0.5f; // Offset from the hit point to spawn the object
	public string targetTag = "Sat"; // Tag of spawnedObjectthe target object
	public float destroyDelay = 1f; // Delay before destroying the spawned object
	public float scaleSpeed = 1f;
	public Vector3 maxScale;
	private Vector3 originalScale;
	private GameObject spawnedObject;
	public static ARManager instance;
	private GameObject currentSat;
	public Image descImg;
	public List<Sprite> images = new List<Sprite>();

	
	void Awake() { instance = this; }

	void Start()
	{
		// Store the original scale of the object
		originalScale = transform.localScale;
	}
	private void Update()
	{
		RaycastHit hit;
		if (Physics.SphereCast(Camera.main.transform.position, sphereCastRadius, Camera.main.transform.forward, out hit, Mathf.Infinity))
		{
			if (hit.collider.CompareTag(targetTag))
			{
				print("ihih");
				// Lerp scale up
				hit.collider.gameObject.transform.localScale = Vector3.Lerp(hit.collider.gameObject.transform.localScale, maxScale, Time.deltaTime * scaleSpeed);
				descImg.sprite = images.FirstOrDefault(obj => obj.name == hit.collider.gameObject.name);
			}
			
			currentSat = hit.collider.gameObject;
			if (currentSat != null)
			{
				if (TryGetComponent<Scaler>(out Scaler hinge))
				{
					hinge.scaling = false;
				}

			}
			
		}
		else
		{
			if(currentSat != null) 
			{
				var scaler = currentSat.GetComponent<Scaler>();
				scaler.scaling = true;
				//scaler.originalScale = originalScale;
				scaler.scaleSpeed = scaleSpeed;

			}
	
			
		}
	}
}

	



