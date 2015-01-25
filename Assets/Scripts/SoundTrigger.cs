using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour
{
	[SerializeField] AudioClip[] sounds;

	void OnTriggerEnter(Collider other)
    {
		AudioSource audioSource = GetComponent<AudioSource>();
		
		if (!audioSource.isPlaying)
		{
			AudioClip ac = sounds[Random.Range(0, sounds.Length)];
			audioSource.clip = ac;
			audioSource.Play();
		}
    }
}
