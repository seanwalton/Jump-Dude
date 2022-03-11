using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] guns;
    [SerializeField] private float difficultyIncreaseRate;

    private List<GameObject> inactiveGuns = new List<GameObject>();
    private int gunToActivate;

    private void Awake()
    {
        inactiveGuns.AddRange(guns);
    }

    private void Start()
    {
        InvokeRepeating("IncreaseDifficultyViaTime", difficultyIncreaseRate, 
            difficultyIncreaseRate);
    }

    private void IncreaseDifficultyViaTime()
    {
        Debug.Log("Difficulty Increase " + Time.time);
    }

    public void IncreaseDifficultyViaProgression()
    {
        if (inactiveGuns.Count == 0) return;

        gunToActivate = Random.Range(0, inactiveGuns.Count);

        inactiveGuns[gunToActivate].SetActive(true);

        inactiveGuns.RemoveAt(gunToActivate);
    }
}
