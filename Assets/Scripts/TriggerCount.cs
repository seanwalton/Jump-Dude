using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCount : MonoBehaviour
{
    private List<GameObject> Colliders = new List<GameObject>();

    [SerializeField] private LayerMask layersToTrigger;
    [SerializeField] private UnityEvent OnFirstEnterTrigger;
    [SerializeField] private UnityEvent OnLastExitTrigger;
    private Vector2 posVector;

    public int NumberOfObjects => Colliders.Count;
    public List<GameObject> ObjectsInTrigger => Colliders;


    public Vector2? GetDirectionToColliders(Transform tr)
    {
        if (Colliders.Count == 0) return null;

        posVector = new Vector2(0f, 0f);

        for (int i = 0; i < Colliders.Count; i++)
        {
            posVector.x += (Colliders[i].transform.position.x - tr.position.x);
            posVector.y += (Colliders[i].transform.position.y - tr.position.y);
        }

        posVector.Normalize();
        return posVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Colliders.Contains(collision.gameObject))
        {
            if ((layersToTrigger & (1 << collision.gameObject.layer)) != 0)
            {
                if (Colliders.Count == 0) OnFirstEnterTrigger?.Invoke();
                Colliders.Add(collision.gameObject);
            }           
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
