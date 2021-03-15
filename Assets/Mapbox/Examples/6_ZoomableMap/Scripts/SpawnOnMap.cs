namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
    using Mapbox.Json;
    using Mapbox.Utils.JsonConverters;
	using Mapbox.Json.Linq;
	using System.IO;

	public class SpawnOnMap : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		[Geocode]
		Vector2d[] _locationStrings;

		public Vector2d[] _locations;

		public List<string> conv = new List<string>();

		[SerializeField]
		float _spawnScale = 100f;

		[SerializeField]
		GameObject _markerPrefab;

		List<GameObject> _spawnedObjects;
		[SerializeField] SpawnMarker spawn;

		public int pNumber;
		void Start()
		{

		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.L))
			{
				string filePath = Application.persistentDataPath + "/pins.json";
				print("loading from: " + filePath);

				StreamReader sr = new StreamReader(filePath);
				string jsonString = sr.ReadToEnd();
				sr.Close();

				JObject jObj = JObject.Parse(jsonString);

				pNumber = jObj["features"]["totalPinsNumber"].Value<int>();
				_locations = jObj["features"]["coordinates"].ToObject<Vector2d[]>();
			}
			if (_spawnedObjects != null)
			{
				for (int i = 0; i < _spawnedObjects.Count; i++)
				{
					var spawnedObject = _spawnedObjects[i];
					var location = _locations[i];
					spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
					spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				}
			}
		}

		public void LoadStrings()
		{
			string[] _locatios = spawn.positionsString.ToArray();

	}



		public void LoadPins()
        {

			string filePath = Application.persistentDataPath + "/pins.json";
			print("loading from: " + filePath);

			StreamReader sr = new StreamReader(filePath);
			string jsonString = sr.ReadToEnd();
			sr.Close();

			JObject jObj = JObject.Parse(jsonString);

			pNumber = jObj["features"]["totalPinsNumber"].Value<int>();
			_locations = jObj["features"]["coordinates"].ToObject<Vector2d[]>();

			Invoke("SpawnPins", 3);
		}

		public void SpawnPins()
        {
			_spawnedObjects = new List<GameObject>();
			for (int i = 0; i < _locations.Length; i++)
			{
				
				var instance = Instantiate(_markerPrefab);
				instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
				instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				_spawnedObjects.Add(instance);
			}
			SpawnMarker.Instance.popUp.gameObject.SetActive(true);
			SpawnMarker.Instance.popText.text = "Zapisano!";
		}

	}

	
}