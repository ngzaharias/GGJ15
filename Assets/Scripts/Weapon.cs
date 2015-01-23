using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	[SerializeField] float		_recoil;
	[SerializeField] Rigidbody	_vesselRigidBody;

	ParticleSystem _particleSystem;

	void Start()
	{
		// Assumes the body root (and its rigidbody) is the parent
		_particleSystem = GetComponent<ParticleSystem>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Fire();
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward));
		Gizmos.DrawSphere(transform.position, 0.1f);
	}

	void Fire()
	{
		GameObject projectile = (GameObject)Instantiate(Resources.Load("Sphere"), transform.position, Quaternion.identity);

		_vesselRigidBody.AddForceAtPosition(GetRecoilDirection() * _recoil, transform.position);
		projectile.GetComponent<Projectile>().Fire(GetFireDirection());
		_particleSystem.Emit(50);
	}

	Vector3 GetFireDirection()
	{
		return transform.TransformDirection(Vector3.forward);
	}

	Vector3 GetRecoilDirection()
	{
		return transform.TransformDirection(Vector3.back);
	}
}
