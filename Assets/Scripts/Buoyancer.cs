using UnityEngine;
using System.Collections;

public class Buoyancer : MonoBehaviour
{
	[SerializeField] float 			_buoyancy = 100;

	Probe[] _probes;

	float 	_ProbeContribution;

	void Start ()
	{
		_probes = GetComponentsInChildren<Probe>();
		_ProbeContribution = 1.0f / (float)_probes.Length;
	}
	
	void FixedUpdate ()
	{
		for (int i = 0; i < _probes.Length; i++)
		{
			float waterLevelAtProbe = BuoyancyManager.Instance.GetWaterLevelAtPosition(_probes[i].transform.position);
			float deltaLevel = waterLevelAtProbe - _probes[i].transform.position.y;
			float force = deltaLevel * Time.deltaTime * _buoyancy * _ProbeContribution;
			Vector3 probePosition = _probes[i].transform.position;

			rigidbody.AddForceAtPosition(new Vector3(0, Mathf.Clamp(force, -5.0f, 9.8f), 0), probePosition);

			if (BuoyancyManager.PROBE_VISUALIZE)
			{
				_probes[i].SetWaterLevel(waterLevelAtProbe);
			}
		}
	}
}
