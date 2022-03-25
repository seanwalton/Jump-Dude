using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrebab;

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
}
