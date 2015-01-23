using UnityEngine;
using System.Collections;

public class Buoyancer : MonoBehaviour
{
	[SerializeField] float 			_buoyancy = 100;

	Probe[] _buoyancyProbes;

	float 	_ProbeContribution;

	void Start ()
	{
		_buoyancyProbes = GetComponentsInChildren<Probe>();
		_ProbeContribution = 1.0f / (float)_buoyancyProbes.Length;
	}
	
	void FixedUpdate ()
	{
		for (int i = 0; i < _buoyancyProbes.Length; i++)
		{
			Vector3 probePosition = _buoyancyProbes[i].transform.position;
			float waterLevelAtProbe = BuoyancyManager.Instance.GetWaterLevelAtPosition(probePosition);
			float deltaLevel = waterLevelAtProbe - probePosition.y;
			float force = deltaLevel * Time.deltaTime * _buoyancy * _ProbeContribution;

			rigidbody.AddForceAtPosition(new Vector3(0, Mathf.Clamp(force, -5.0f, 9.8f), 0), probePosition);

			if (BuoyancyManager.PROBE_VISUALIZE)
			{
				_buoyancyProbes[i].SetWaterLevel(waterLevelAtProbe);
			}
		}
	}
}
