using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrebab;
    [SerializeField] private AudioSource contactAudioSource;
    [SerializeField] private AudioClip contactClip;

    public UnityEvent OnKill;
    public void Kill()
    {
        OnKill?.Invoke();

        Instantiate(explosionPrebab, transform.position, transform.rotation).
            GetComponent<ParticleSystem>().Emit(100);

        DeactiveObject();
    }

    private void DeactiveObject()
    {
        gameObject.SetActive(false);
    }

    public void PlayContactSound(float v)
    {
        if ((contactAudioSource.time > 0f) 
            && ((contactAudioSource.time / contactClip.length) < 0.5f)) return;
        contactAudioSource.PlayOneShot(contactClip, v);
    }
}
