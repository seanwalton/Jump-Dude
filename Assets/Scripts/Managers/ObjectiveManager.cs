using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private Transform[] platforms;
    [SerializeField] private Transform objective;
    [SerializeField] private float heightAbovePlatform;

    public static event Action<int> OnScoreChange;

    int lastLocation = -1;
    int newLocation;
    private Vector3 pos;
    private int numberOfCollections;

    private void Start()
    {
        numberOfCollections = -1;
        MoveObjective();
    }

    public void MoveObjective()
    {
        newLocation = UnityEngine.Random.Range(0, platforms.Length);
        while (lastLocation == newLocation)
        {
            newLocation = UnityEngine.Random.Range(0, platforms.Length);
        }

        pos = platforms[newLocation].position;
        pos.y += heightAbovePlatform;
        objective.position = pos;
        numberOfCollections++;
        OnScoreChange?.Invoke(numberOfCollections);
    }

}
