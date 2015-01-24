using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	[SerializeField] float		_force = 1000;
	[SerializeField] float		_recoil;
	[SerializeField] Rigidbody	_vesselRigidBody;
	[SerializeField] Transform	_trajectory;
	[SerializeField] Transform	_weaponRoot;
	
	float _trajectoryPatternOffset;
	ParticleSystem _particleSystem;
	Vector3 _weaponRootInitialRotation;

	void Start()
	{
		_particleSystem = GetComponent<ParticleSystem>();
		_weaponRootInitialRotation = _weaponRoot.eulerAngles;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
		{
			Fire();
		}

		_trajectoryPatternOffset -= Time.deltaTime * 3;
		_trajectory.position = transform.position;

		//GetComponent<Trajectory>().setTrajectoryPoints(transform.position, GetFireDirection() * force * 0.01995f);

		float angle = -transform.parent.eulerAngles.x;
		float angle2 = Mathf.Sin(angle * Mathf.Deg2Rad);
		float gravity = Physics.gravity.magnitude;

		float timeOfPeak = (_force * angle2) / gravity;
		float timeOfLand = (2 * _force * angle2) / gravity;
		float peak = _force * timeOfPeak * angle2 - (gravity * timeOfPeak * timeOfPeak / 2.0f);
		float distance = _force * timeOfLand * Mathf.Cos(angle * Mathf.Deg2Rad);
		float scaleCorrection = 0.000395f;
		_trajectory.localScale = new Vector3(1, peak * scaleCorrection, distance * scaleCorrection);

		// Tile and scroll trajectory pattern
		float flightTime = (2 * _force * angle2) / gravity;
		_trajectory.renderer.materials[0].SetTextureOffset("_MainTex", new Vector2(_trajectoryPatternOffset, 0));
		_trajectory.renderer.materials[0].SetTextureScale("_MainTex", new Vector2(flightTime * 0.5f, 1));
	}

	void LateUpdate()
	{
		Vector3 stabilizedRotation = _weaponRootInitialRotation;
		stabilizedRotation.y = _vesselRigidBody.transform.eulerAngles.y + _weaponRootInitialRotation.y;
		_weaponRoot.eulerAngles = stabilizedRotation;
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
		projectile.GetComponent<Projectile>().Fire(GetFireDirection().normalized, _force);
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
