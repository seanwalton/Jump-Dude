using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCount : MonoBehaviour
{
    private List<GameObject> Colliders = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Colliders.Contains(collision.gameObject)) Colliders.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Colliders.Contains(collision.gameObject)) Colliders.Remove(collision.gameObject);
    }
}
