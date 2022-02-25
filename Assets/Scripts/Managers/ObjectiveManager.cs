using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private Transform[] platforms;
    [SerializeField] private Transform objective;
    [SerializeField] private float heightAbovePlatform;

    int lastLocation = -1;
    int newLocation;
    private Vector3 pos;

    private void Start()
    {
        MoveObjective();
    }

    public void MoveObjective()
    {
        newLocation = Random.Range(0, platforms.Length);
        while (lastLocation == newLocation)
        {
            newLocation = Random.Range(0, platforms.Length);
        }

        pos = platforms[newLocation].position;
        pos.y += heightAbovePlatform;
        objective.position = pos;
    }

}
