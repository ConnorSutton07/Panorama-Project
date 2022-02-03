namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;

	public class SpawnOnMap : MonoBehaviour
	{
		[SerializeField] AbstractMap _map;

		Vector2d location;
        bool spawned = false;
		[SerializeField] float _spawnScale = 100f;

		[SerializeField] GameObject markerPrefab;
        GameObject markerInstance;

		public void Spawn(Vector2d newLocation)
		{
            Destroy(markerInstance);
            Debug.Log("Spawning");
            location = newLocation;
            markerInstance = Instantiate(markerPrefab);
            spawned = true;
		}

		private void Update()
		{
            if (spawned)
            {
                markerInstance.transform.localPosition = _map.GeoToWorldPosition(location, true);
                markerInstance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
		}
	}
}