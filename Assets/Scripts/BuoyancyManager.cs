using UnityEngine;
using System.Collections;

public class BuoyancyManager : MonoBehaviour
{
	private static BuoyancyManager instance = null;

	public static BuoyancyManager Instance
	{
		get { return instance; }
	}

	public static bool 		PROBE_VISUALIZE = true;
	public static float 	PROBE_SIZE = 0.1f;

	[SerializeField]Texture2D 	_heightmap;
	
	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}

		DontDestroyOnLoad(this.gameObject);
	}

	/// <summary>Returns the water level at a given position</summary>
	/// <param name="position"></param>
	/// <returns></returns>
	public float GetWaterLevelAtPosition(Vector3 position)
	{
		float timescale = Time.time * -0.05f;
		float u = (position.x * 0.1f) + timescale;
		float v = (position.z * 0.1f);

		return _heightmap.GetPixelBilinear(-u, -v).r;
	}
}
