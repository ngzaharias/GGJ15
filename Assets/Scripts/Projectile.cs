﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    Vector3 _directon;

    bool _flaggedForKill = false;
    float _lifespan;
    ParticleSystem _particleSystem;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Fire(Vector3 direction, float force)
    {
        rigidbody.AddForce(direction * force);
    }

    void Update()
    {
        _lifespan += Time.deltaTime;
        if (_lifespan > 10 || (_flaggedForKill && _particleSystem.particleCount == 0))
        {
            GameObject.Destroy(this.gameObject);
        }
    }

	void LateUpdate()
	{
		float waterLevel = BuoyancyManager.Instance.GetWaterLevelAtPosition(transform.position);
		if (!_flaggedForKill && waterLevel > transform.position.y)
		{
			Kill();
			transform.Find("EProbe").GetComponent<ParticleSystem>().Emit(50);
		}
	}

    void OnCollisionEnter(Collision collision)
    {
        Kill();

		foreach (ContactPoint contact in collision.contacts)
		{
			if (contact.otherCollider.rigidbody)
			{
				contact.otherCollider.rigidbody.AddExplosionForce(1000, transform.position, 5.0f);
			}
			
			if (contact.otherCollider.GetComponent<Health>())
			{
				contact.otherCollider.GetComponent<Health>().Damage(10);
			}
        }
    }

	void Kill()
	{
		collider.enabled = false;
        _particleSystem.Emit(50);
        _flaggedForKill = true;
        renderer.enabled = false;
		rigidbody.isKinematic = true;
	}
}
