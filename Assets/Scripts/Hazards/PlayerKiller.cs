using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerKiller : MonoBehaviour
{
    [SerializeField] private bool DisableOnKill;
    public UnityEvent OnKill;
    private PlayerManager pm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pm = collision.gameObject.GetComponent<PlayerManager>();
        if (pm)
        {
            pm.Kill();
            OnKill?.Invoke();
            if (DisableOnKill) gameObject.SetActive(false);
        }
    }
}
