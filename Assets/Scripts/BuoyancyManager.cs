using UnityEngine;
using System.Collections;

public class BuoyancyManager : MonoBehaviour
{
	private static BuoyancyManager instance = null;

	public static BuoyancyManager Instance
	{
		get
		{
			return instance;
		}
	}

	public static bool 		PROBE_VISUALIZE = false;
	public static float 	PROBE_SIZE = 0.05f;

	public static bool 		TESTASD = false;

	[SerializeField]bool		_visualizeProbes = false;
	[SerializeField]float		_probeSize = 0.05f;
	[SerializeField]float		_waveScale = 1;
	[SerializeField]float		_waveSpeed = 1;
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

	void LateUpdate()
	{
		PROBE_VISUALIZE = _visualizeProbes;
		PROBE_SIZE = _probeSize;
	}

	public float GetHeight(Vector3 position)
	{
		float timescale = Time.time * -0.05f;
		float u = (position.x * 0.1f) + timescale;
		float v = (position.z * 0.1f);

		return _heightmap.GetPixelBilinear(-u, -v).r;
	}
}
