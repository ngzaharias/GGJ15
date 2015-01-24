﻿using UnityEngine;
using System.Collections;

public class Cthulu : MonoBehaviour
{
	[SerializeField] Transform _target;
	[SerializeField] Vector3 _lookOffset;

	Transform _neck;

	void Start()
	{
		_neck = transform.Find("Root/spine1/neck");
	}

	void Update()
	{
		_neck.transform.LookAt(_target.position, Vector3.left);
		_neck.transform.eulerAngles -= _lookOffset;
	}
}
