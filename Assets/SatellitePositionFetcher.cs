using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class SatellitePositionFetcher : MonoBehaviour
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

	// Base URL of the API endpoint
	private string apiUrl = "https://api.n2yo.com/rest/v1/satellite/positions/";

	// API key
	private string apiKey = "XCYZMC-Q46HA7-FE9DJ3-592O"; // Replace with your actual API key

	// Dictionary to store satellite position data
	private Dictionary<int, SatellitePositionData> satelliteDataDictionary = new Dictionary<int, SatellitePositionData>();

	void Start()
	{
		StartCoroutine(FetchSatellitePositions());
	}

	IEnumerator FetchSatellitePositions()
	{
		foreach (int noradId in noradIds)
		{
			string url = apiUrl + noradId + "/41.702/-76.014/0/1/&apiKey=" + apiKey;

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

				// Store satellite position data in dictionary
				satelliteDataDictionary[noradId] = data;
			}
			print(noradId + "done");
		}

		// After fetching all positions, you can access them from satelliteDataDictionary
		// Example usage:
		foreach (KeyValuePair<int, SatellitePositionData> kvp in satelliteDataDictionary)
		{
			int noradId = kvp.Key;
			SatellitePositionData data = kvp.Value;

			Debug.Log("Satellite Name for NORAD ID " + noradId + ": " + data.info.satname);
			// Access other data as needed
		}
	}

	// Data structure for JSON parsing
	[Serializable]
	public class SatellitePositionData
	{
		public Info info;
		public Position[] positions;
	}

	[Serializable]
	public class Info
	{
		public string satname;
		public int satid;
		public int transactionscount;
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
