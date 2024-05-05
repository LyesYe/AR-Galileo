using UnityEngine;

public class ARManager : MonoBehaviour
{
	public GameObject objectToSpawn; // The object to spawn
	public float sphereCastRadius = 0.1f; // Radius of the sphere cast
	public float spawnOffset = 0.5f; // Offset from the hit point to spawn the object
	public string targetTag = "Sat"; // Tag of spawnedObjectthe target object
	public float destroyDelay = 1f; // Delay before destroying the spawned object
	private GameObject spawnedObject;

	private void Update()
	{
		RaycastHit hit;
		if (Physics.SphereCast(Camera.main.transform.position, sphereCastRadius, Camera.main.transform.forward, out hit, Mathf.Infinity))
		{
			if (hit.collider.CompareTag(targetTag))
			{
				if (spawnedObject == null)
				{
					Vector3 spawnPosition = hit.point + hit.normal * spawnOffset;
					spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
				}
			}
			else
			{
				if (spawnedObject != null)
				{
					Destroy(spawnedObject,1);
				}
			}
		}
		else
		{
			if (spawnedObject != null)
			{
				Destroy(spawnedObject,1);
			}
		}
	}
}

	



