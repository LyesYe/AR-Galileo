using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class MapboxPositionGetter : MonoBehaviour
{
	public AbstractMap map; // Reference to the Mapbox map

	void Start()
	{
		// Subscribe to the map's events
		map.OnInitialized += OnMapInitialized;
	}

	void OnMapInitialized()
	{
		// Get the center latitude, longitude, and altitude of the map
		Vector2d centerLatLng = map.CenterLatitudeLongitude;
		float altitude = map.Options.locationOptions.zoom;

		// Print the position information
		Debug.Log("Latitude: " + centerLatLng.x);
		Debug.Log("Longitude: " + centerLatLng.y);
		Debug.Log("Altitude: " + altitude);
	}
}
