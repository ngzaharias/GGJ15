using UnityEngine;
using System.Collections;

public class EffectsProbe : MonoBehaviour
{
	float _previousLevel;
	Vector3 _previousPosition;
	ParticleSystem _particleSystem;

	void Start()
	{
		_previousLevel = transform.position.y;
		_particleSystem = GetComponent<ParticleSystem>();
	}

	void FixedUpdate()
	{
		float currentLevel = transform.position.y;
		float waterLevelAtProbe = BuoyancyManager.Instance.GetWaterLevelAtPosition(transform.position);
		float levelDifference = currentLevel - waterLevelAtProbe;
		
		bool enteredWater = _previousLevel > waterLevelAtProbe && currentLevel < waterLevelAtProbe;
		bool exitedWater = _previousLevel < waterLevelAtProbe && currentLevel > waterLevelAtProbe;

		float deltaPosition = Vector3.Distance(transform.position, _previousPosition);
		
		if (Mathf.Abs(levelDifference) < 0.5f)
		{
			_particleSystem.startSpeed = 2.5f + (deltaPosition * 25);
			_particleSystem.emissionRate = (deltaPosition - 0.05f) * 2500;
		}
		else
		{
			_particleSystem.emissionRate = 0;
		}
		
		if (enteredWater || exitedWater)
		{
			//GetComponent<ParticleSystem>().Emit(50);
			// Probe entered or exited the water
		}

		if (Mathf.Abs(levelDifference) < 0.1f)
		{
			//GetComponent<ParticleSystem>().emissionRate = 10;
			// Probe is close to the water level
		}

		_previousLevel = currentLevel;
		_previousPosition = transform.position;
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
