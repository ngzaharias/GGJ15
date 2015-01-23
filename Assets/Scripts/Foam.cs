using UnityEngine;
using System.Collections;

public class Foam : MonoBehaviour
{
	void Update()
	{
		float c = renderer.material.GetFloat("_Cutoff");
		c += Time.deltaTime * 0.25f;

		if (c >= 1)
		{
			GameObject.Destroy(gameObject);
		}
		else
		{
			renderer.material.SetFloat("_Cutoff", c);
		}

		rigidbody.AddForce(new Vector3(Time.deltaTime * 15, 0, 0));
	}
}
