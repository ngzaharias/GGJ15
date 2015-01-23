using UnityEngine;
using System.Collections;

public class Probe : MonoBehaviour
{
	Vector3 _targetPosition;

	public void SetTarget(Vector3 position)
	{
		_targetPosition = position;
	}

	void OnDrawGizmos()
	{
		if (BuoyancyManager.PROBE_VISUALIZE)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(transform.position, BuoyancyManager.PROBE_SIZE);
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(_targetPosition, BuoyancyManager.PROBE_SIZE);
		}
	}
}
