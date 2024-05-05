using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class SatelliteSpawner : MonoBehaviour
{

	int[] noradIds = new int[]
{
		49810, // GALILEO 28
        49809, // GALILEO 27
        43567, // GALILEO 24
        43566, // GALILEO 23
        43565, // GALILEO 26
        43564, // GALILEO 25
        43058, // GALILEO 22
        43057, // GALILEO 21
        43056, // GALILEO 20
        43055, // GALILEO 19
        41862, // GALILEO 18
        41861, // GALILEO 17
        41860, // GALILEO 16
        41859, // GALILEO 15
        41550, // GALILEO 13
        41549, // GALILEO 14
        41175, // GALILEO 11
        41174, // GALILEO 12
        40890, // GALILEO 10
        40889, // GALILEO 9
        40545, // GSAT0204
        40544, // GSAT0203
        40129, // GALILEO 6
        40128, // GALILEO 5
        38858, // GALILEO-FM4
        38857, // GALILEO-FM3
        37847, // GALILEO-FM2
        37846  // GALILEO-PFM
};
	// Player's position (latitude, longitude, altitude)
	public float playerLatitude;
	public float playerLongitude;
	public float playerAltitude;
	public float spawnDistance;
	public GameObject satellitePrefab;

	// Base URL of the API endpoint
	private string apiUrl = "https://api.n2yo.com/rest/v1/satellite/positions/";

	// API key
	private string apiKey = "XCYZMC-Q46HA7-FE9DJ3-592O"; // Replace with your actual API key

	// Dictionary to store satellite direction vectors
	private Dictionary<int, Vector3> satelliteDirections = new Dictionary<int, Vector3>();

	void Start()
	{
		StartCoroutine(FetchSatelliteDirections());
	}

	IEnumerator FetchSatelliteDirections()
	{
		foreach (int noradId in noradIds)
		{
			string url = apiUrl + noradId + "/" + playerLatitude + "/" + playerLongitude + "/" + playerAltitude + "/1/&apiKey=" + apiKey;

			// Create a request object
			UnityWebRequest request = UnityWebRequest.Get(url);

			// Send the request
			yield return request.SendWebRequest();

			// Check for errors
			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Error fetching satellite position for NORAD ID " + noradId + ": " + request.error);
			}
			else
			{
				// Parse JSON response
				string jsonResponse = request.downloadHandler.text;
				SatellitePositionData data = JsonUtility.FromJson<SatellitePositionData>(jsonResponse);

				// Calculate direction vector
				Vector3 satellitePosition = CalculateSatellitePosition(playerLatitude, playerLongitude, playerAltitude, data.positions[0].satlatitude, data.positions[0].satlongitude, data.positions[0].sataltitude);
				Vector3 direction = (satellitePosition - transform.position).normalized;

				// Store direction vector in dictionary
				satelliteDirections[noradId] = direction;
				print(noradId + "done");
			}
		}

		// After fetching all directions, you can spawn the satellites in AR using the direction vectors
		// Example usage:
		foreach (KeyValuePair<int, Vector3> kvp in satelliteDirections)
		{
			int noradId = kvp.Key;
			Vector3 direction = kvp.Value;

			// Spawn satellite in AR using direction vector
			SpawnSatellite(direction , kvp.Key);
		}
	}

	Vector3 CalculateSatellitePosition(float playerLat, float playerLon, float playerAlt, float satLat, float satLon, float satAlt)
	{
		// Convert latitude and longitude to radians
		float playerLatRad = playerLat * Mathf.Deg2Rad;
		float playerLonRad = playerLon * Mathf.Deg2Rad;
		float satLatRad = satLat * Mathf.Deg2Rad;
		float satLonRad = satLon * Mathf.Deg2Rad;

		// Earth radius in meters
		float earthRadius = 6371000; // Approximate

		// Convert spherical coordinates to Cartesian coordinates
		Vector3 playerPos = new Vector3(
			earthRadius * Mathf.Cos(playerLatRad) * Mathf.Cos(playerLonRad),
			earthRadius * Mathf.Cos(playerLatRad) * Mathf.Sin(playerLonRad),
			earthRadius * Mathf.Sin(playerLatRad)
		);
		Vector3 satPos = new Vector3(
			earthRadius * Mathf.Cos(satLatRad) * Mathf.Cos(satLonRad),
			earthRadius * Mathf.Cos(satLatRad) * Mathf.Sin(satLonRad),
			earthRadius * Mathf.Sin(satLatRad)
		);

		// Adjust for altitude
		playerPos += playerAlt * Vector3.up;
		satPos += satAlt * Vector3.up;

		return satPos - playerPos;
	}

	void SpawnSatellite(Vector3 direction , int noradID)
	{
		// Instantiate satellite prefab and set its position and rotation based on the direction vector
		GameObject satellite = Instantiate(satellitePrefab, Camera.main.transform.position + direction * spawnDistance, Quaternion.LookRotation(direction, Vector3.up));
		satellite.name = noradID.ToString();
	}

	// Data structure for JSON parsing
	[Serializable]
	public class SatellitePositionData
	{
		public Position[] positions;
	}

	[Serializable]
	public class Position
	{
		public float satlatitude;
		public float satlongitude;
		public float sataltitude;
		public float azimuth;
		public float elevation;
		public float ra;
		public float dec;
		public long timestamp;
		public bool eclipsed;
	}
}
