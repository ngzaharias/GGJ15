using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
	[SerializeField] int				_health = 100;
	[SerializeField] Vector3			_healthBarOffset;
	[SerializeField] Transform			_transformToScale;
	[SerializeField] Vector3			_onDeathScale;
	[SerializeField] ParticleSystem		_particlesToEmitDamage;
	[SerializeField] int				_onDamageEmission;
	[SerializeField] ParticleSystem		_particlesToEmitDeath;
	[SerializeField] int				_onDeathEmission;

	GameObject _healthBar;

	void Start()
	{
		_healthBar = (GameObject)Instantiate(Resources.Load("Interface/HealthBar"), transform.position + _healthBarOffset, Quaternion.identity);
		_healthBar.transform.parent = InterfaceManager.Instance.transform.Find("CanvasWorld");
		_healthBar.GetComponent<Slider>().maxValue = _health;
	}

	void LateUpdate()
	{
		if (_healthBar != null)
		{
			_healthBar.GetComponent<Slider>().value = _health;
			_healthBar.transform.position = transform.position + _healthBarOffset;
		}
	}

	public void Damage(int damage)
	{
		_health = Mathf.Clamp(_health - damage, 0, int.MaxValue);

		if (_health == 0)
		{
			GameObject.Destroy(_healthBar);
			if (_transformToScale)
				_transformToScale.localScale = _onDeathScale;
			if (_particlesToEmitDeath)
				_particlesToEmitDeath.Emit(_onDeathEmission);
		}
		else
		{
			if (_particlesToEmitDamage)
				_particlesToEmitDamage.Emit(_onDamageEmission);
		}
	}
}
