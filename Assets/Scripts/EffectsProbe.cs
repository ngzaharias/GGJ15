using UnityEngine;
using System.Collections;

public class EffectsProbe : MonoBehaviour
{
	[SerializeField] GameObject _foamPrefab;

	float _previousLevel;

	void Start()
	{
		_previousLevel = transform.position.y;
	}

	void FixedUpdate()
	{
		float currentLevel = transform.position.y;
		float waterLevelAtProbe = BuoyancyManager.Instance.GetWaterLevelAtPosition(transform.position);
		float levelDifference = currentLevel - waterLevelAtProbe;
		
		bool enteredWater = _previousLevel > waterLevelAtProbe && currentLevel < waterLevelAtProbe;
		bool exitedWater = _previousLevel < waterLevelAtProbe && currentLevel > waterLevelAtProbe;
		
		if (enteredWater || exitedWater)
		{
			// Probe entered or exited the water
		}

		if (Mathf.Abs(levelDifference) < 0.1f)
		{
			// Probe is close to the water level
		}

		_previousLevel = currentLevel;
	}

	void OnDrawGizmos()
	{
		if (BuoyancyManager.PROBE_VISUALIZE)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(transform.position, BuoyancyManager.PROBE_SIZE * 0.5f);
		}
	}
}
