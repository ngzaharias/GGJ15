using UnityEngine;
using System.Collections;

public class MMenu_Lookat : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void LateUpdate () {
		transform.LookAt (Camera.main.transform.position);
		transform.Rotate (new Vector3 (0, 0, 0));

	}

	// Update is called once per frame
	void Update () {
	
	}
}
