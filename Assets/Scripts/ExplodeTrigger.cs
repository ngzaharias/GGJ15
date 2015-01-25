using UnityEngine;
using System.Collections;

public class ExplodeTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject _explosionPrefab;

    [SerializeField]
    string[] _colliderTags = { "Player" };

    [SerializeField]
    float _explosionDelay = 1.0f;

    [SerializeField]
    float _deathTimer = 2.0f;

    GameObject explosion = null;

    void OnCollisionEnter(Collision collision)
    {
        if (IsInvoking("Explode"))
        {
            return;
        }

        foreach (string colliderTag in _colliderTags)
        {
            if (collision.gameObject.CompareTag(colliderTag))
            {
                Invoke("Explode", _explosionDelay);
                return;
            }
        }
    }

    void Explode()
    {
        explosion = Instantiate(_explosionPrefab, transform.position, transform.rotation) as GameObject;

        //explodeParticle.Play();

        Destroy(explosion, _deathTimer);
        Destroy(gameObject);
    }

    void LoadCredits()
    {

    }
}
