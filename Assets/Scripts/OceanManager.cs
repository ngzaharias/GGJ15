using UnityEngine;
using System.Collections;

public class OceanManager : MonoBehaviour
{
	[SerializeField] float foamDecay = 0.05f;

	SphereCollider[] 	_spheres;
	Renderer[]			_tiles;

//	int[] test = new int[] { -128, 128, -1, 1 };
	int[] test = new int[] { -128, 128 };

	void Start()
	{
		_spheres = FindObjectsOfType(typeof(SphereCollider)) as SphereCollider[];
		_tiles = GetComponentsInChildren<Renderer>();

		foreach(Renderer r in _tiles)
		{
			Texture2D tex = (Texture2D)Instantiate(r.material.GetTexture("_MainTex3"));
			r.material.SetTexture("_MainTex3", tex);

//			hitObject.renderer.material.mainTexture = texture
//			Texture2D tex = r.material.GetTexture("_MainTex3") as Texture2D;
//			Color[] colors = tex.GetPixels();
//			Color c = Color.black;
//
//			for (int i = 0; i < colors.Length; i++)
//			{
//				c.a = Random.Range(0.0f, 1.0f);
//				colors[i] = c;
//			}
//
//			tex.SetPixels(colors);
//			tex.Apply();
//			r.material.SetTexture("_MainTex3", tex as Texture);
		}

//		Color test = new Color(2, 5, 7);
//		print (test);
	}

	void Update()
	{
		float size = 128.0f;

		for (int i = 0; i < _spheres.Length; i++)
		{
			Vector2 spherePos = new Vector2(_spheres[i].transform.position.x, _spheres[i].transform.position.z);

			foreach(Renderer r in _tiles)
			{
				Texture2D tex = r.material.GetTexture("_MainTex3") as Texture2D;
				Color[] colors = tex.GetPixels();

				for (int j = 0; j < colors.Length; j++)
				{
					Color c = colors[j];

					float x = (((float)j % size) / (size / 10.0f));
					float y = (((float)j / size) / (size / 10.0f));
					Vector2 pos = new Vector2(-x + r.transform.position.x + 5,
					                          -y + r.transform.position.z + 5);

					float d = Vector2.Distance(spherePos, pos);
					if (d < _spheres[i].radius / 4)
					{
//						Random.seed = (int)pos.x + (int)pos.y;
//						c.r += Random.Range(0.75f, 1.0f);
//						c.r += Random.Range(0.1f, 0.2f);
						c.r = 1;
					}

					if (c.r > 0.1f)
					{
						float bleed = c.r * 0.04f;

						for (int k = 0; k < test.Length; k++)
						{
							if (j + test[k] < colors.Length && j + test[k] > 0)
							{
//								float neighbor = colors[j + test[k]].r;
//								if (colors[j + test[k]].r < 0.1f)
//								{
//								float bleed = c.r * 0.25f;
//								colors[j + test[k]].r += bleed;
								colors[j + test[k]].r = Mathf.Clamp01(colors[j + test[k]].r + bleed);
//								c.r -= bleed;
								c.r = Mathf.Clamp01(c.r - (bleed * 50));
//								}
							}
						}
					}

//					if (c.r > 0.1f)
////					if (c.r < 0.95f)
//					{
//						float bleed = (1.0f - c.r) * 0.9f;
////						float bleed = c.r * 0.75f;
//
////						if (Random.Range(0, 250) == 0)
////							print (c.r + " / " + bleed);
//						
//						for (int k = 0; k < test.Length; k++)
//						{
//							if (j + test[k] < colors.Length && j + test[k] > 0)
//							{
////								colors[j + test[k]].r = Mathf.Min(colors[j + test[k]].r, bleed);
//								float neighbor = colors[j + test[k]].r;
////								colors[j + test[k]].r = Mathf.Min(asd, bleed);
////								colors[j + test[k]].r = Mathf.Clamp(bleed, 0, c.r);
////								if (neighbor == 0)
//								colors[j + test[k]].r = Mathf.Clamp(c.r * bleed, bleed, c.r);
//							}
//						}
//						
//						//							if (colors[j + 64] == null) print ("asd");
//						//
//						//							if (j + 64 < colors.Length)
//						//								colors[j + 64].r = Mathf.Max(colors[j + 64].r, bleed);
//						//							if (j - 64 > 0)
//						//								colors[j - 64].r = Mathf.Max(colors[j - 64].r, bleed);
//					}

					float decay = 10 * Time.deltaTime;
					c.r = Mathf.Clamp01(c.r - 0.002f);
					c.r--;
//					c.r = Mathf.Clamp01(c.r - decay);
				}
				
				tex.SetPixels(colors);
				tex.Apply();
				r.material.SetTexture("_MainTex3", tex as Texture);
			}
		}
	}
}