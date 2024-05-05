using UnityEngine;

public class GPSLocationGetter : MonoBehaviour
{
	void Start()
	{
		// Check if location services are enabled by the user
		if (!Input.location.isEnabledByUser)
		{
			Debug.LogError("Location services are not enabled.");
			return;
		}

		// Start getting the device's location
		Input.location.Start();

		// Wait until location data is available
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			maxWait--;
			Debug.Log("Waiting for location data...");
			System.Threading.Thread.Sleep(1000); // Wait for 1 second
		}

		// If no location data is available
		if (maxWait <= 0)
		{
			Debug.LogError("Timed out while waiting for location data.");
			return;
		}

		// If location data is available
		if (Input.location.status == LocationServiceStatus.Running)
		{
			// Get the latitude, longitude, and altitude
			float latitude = Input.location.lastData.latitude;
			float longitude = Input.location.lastData.longitude;
			float altitude = Input.location.lastData.altitude;

			// Print the position information
			Debug.Log("Latitude: " + latitude);
			Debug.Log("Longitude: " + longitude);
			Debug.Log("Altitude: " + altitude);
		}
		else
		{
			Debug.LogError("Failed to get location data.");
		}

		// Stop getting the location updates
		Input.location.Stop();
	}
}
