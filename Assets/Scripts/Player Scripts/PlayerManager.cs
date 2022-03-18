using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public UnityEvent OnKill;
    public void Kill()
    {
        OnKill?.Invoke();
        Invoke("DeactiveObject", 0.5f);
    }

    private void DeactiveObject()
    {
        gameObject.SetActive(false);
    }
}
