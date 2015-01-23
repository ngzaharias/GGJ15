using UnityEngine;
using System.Collections;

public class EffectsProbe : MonoBehaviour
{
	[SerializeField] GameObject _foamPrefab;

	int 			_simulationCycle = 0;
	float 			_previousLevel;
	ParticleSystem 	_particleSystem;

	void Start()
	{
		_previousLevel = transform.position.y;
		_particleSystem = GetComponent<ParticleSystem>();

		_particleSystem.enableEmission = false;
	}

	void FixedUpdate()
	{
//		return;
		Vector3 position = transform.position;

		float probeWaterLevel = BuoyancyManager.Instance.GetHeight(transform.position);
		float deltaLevel = position.y - probeWaterLevel;
		
		bool enteredWater = _previousLevel > probeWaterLevel && position.y < probeWaterLevel;
		bool exitedWater = _previousLevel < probeWaterLevel && position.y > probeWaterLevel;
		
		if (enteredWater || exitedWater)
		{
			float power = Mathf.Abs(deltaLevel);
//			power = Mathf.Clamp(power, 0.0f, 0.05f);
			_particleSystem.startSpeed = (power * Random.Range(500, 1000)) + 1.0f;
			_particleSystem.Emit((int)(power * 1000) + 1);
			
//			GameObject particle = (GameObject)Instantiate(_foamPrefab, transform.position, Quaternion.identity);
//			float force = Random.Range(-10, 10);
//			particle.rigidbody.AddForce(new Vector3(force, force, force));
		}
//		else if (Mathf.Abs(deltaLevel) < 0.05f)
		else if (deltaLevel > 0 && deltaLevel < 0.1f)
		{
			float power = Mathf.Abs(deltaLevel);
			_particleSystem.startSpeed = 0;
			_particleSystem.Emit(Random.Range(0, 4) == 0 ? 1 : 0);
		}

		_previousLevel = position.y;
	}

	void OnDrawGizmos()
	{
		if (BuoyancyManager.PROBE_VISUALIZE)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(transform.position, BuoyancyManager.PROBE_SIZE * 0.25f);
		}
	}
}
