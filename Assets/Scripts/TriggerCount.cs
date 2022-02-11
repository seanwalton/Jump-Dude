using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCount : MonoBehaviour
{
    private List<GameObject> Colliders = new List<GameObject>();

    [SerializeField] private UnityEvent OnFirstEnterTrigger;
    [SerializeField] private UnityEvent OnLastExitTrigger;

    public int NumberOfObjects => Colliders.Count;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Colliders.Contains(collision.gameObject))
        {
            if (Colliders.Count == 0) OnFirstEnterTrigger?.Invoke();
            Colliders.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Colliders.Contains(collision.gameObject))
        {
            Colliders.Remove(collision.gameObject);
            if (Colliders.Count == 0) OnLastExitTrigger?.Invoke();
        }
    }


}
