using UnityEngine;
using System.Collections;

public class Probe : MonoBehaviour
{
	Vector3 _waterLevelAtProbe;

	/// <summary>Sets the height of the debugger probe target</summary>
	/// <param name="waterLevel"></param>
	public void SetWaterLevel(float waterLevel)
	{
		_waterLevelAtProbe = new Vector3(transform.position.x, waterLevel, transform.position.z);
	}

	void OnDrawGizmos()
	{
		if (BuoyancyManager.PROBE_VISUALIZE)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(transform.position, BuoyancyManager.PROBE_SIZE);
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(_waterLevelAtProbe, BuoyancyManager.PROBE_SIZE);
		}
	}
}
