using UnityEngine;
using System.Collections;

public class ExplodeTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject _explosionPrefab;

    [SerializeField]
    float _explosionDelay = 1.0f;

    [SerializeField]
    float _deathTimer = 2.0f;

    GameObject _explosion = null;

    void OnCollisionEnter(Collision collision)
    {
        if (IsInvoking("Explode"))
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Explode", _explosionDelay);

            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.Damage(playerHealth.CurrentHealth);
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Invoke("Explode", _explosionDelay);
        }
    }

    void Explode()
    {
        _explosion = Instantiate(_explosionPrefab, transform.position, transform.rotation) as GameObject;

        Destroy(_explosion, _deathTimer);
        Destroy(gameObject);
    }
}
