using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public void Kill()
    {
        gameObject.SetActive(false);
    }
}
