using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableBehaviour : MonoBehaviour
{
    [SerializeField] private bool DisableOnCollect;
    public UnityEvent OnCollect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            OnCollect?.Invoke();
            if (DisableOnCollect) gameObject.SetActive(false);
        }
    }
}
