using UnityEngine;
using System.Collections;

public class Buoyancer : MonoBehaviour
{
	[SerializeField] float 			_buoyancy = 100;
	//[SerializeField] GameObject		_effectsProbe;

	Probe[] _probes;

	float 	_forceModifier;

	void Start ()
	{
		_probes = GetComponentsInChildren<Probe>();
		_forceModifier = 1.0f / (float)_probes.Length;

		//Transform[] children = GetComponentsInChildren<Transform>();

		//int foamPoints = 0;

		//for (int i = 0; i < children.Length; i++)
		//{
		//	if (children[i].gameObject.name.Contains("FoamPoint"))
		//	{
		//		GameObject go = (GameObject)Instantiate(_effectsProbe, children[i].position, Quaternion.identity);
		//		go.transform.parent = transform;
		//		GameObject.Destroy(children[i].gameObject);
		//		foamPoints++;
		//	}
		//}

		//print ("<" + gameObject.name + "> initialized with <" + foamPoints + "> foam points");
	}
	
	void FixedUpdate ()
	{
		for (int i = 0; i < _probes.Length; i++)
		{
			float probeWaterLevel = BuoyancyManager.Instance.GetHeight(_probes[i].transform.position);
			float deltaLevel = probeWaterLevel - _probes[i].transform.position.y;

			Vector3 targetPosition = _probes[i].transform.position;
			targetPosition.y = probeWaterLevel;
			_probes[i].SetTarget(targetPosition);

			float force = deltaLevel * Time.deltaTime * _buoyancy * _forceModifier;
			rigidbody.AddForceAtPosition(new Vector3(0, Mathf.Clamp(force, -5.0f, 9.8f), 0), _probes[i].transform.position);
		}
	}
}
